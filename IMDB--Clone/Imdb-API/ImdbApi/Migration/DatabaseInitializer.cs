using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ImdbApi.Migration;

public sealed class DatabaseInitializer
{
    private static readonly string[] MigrationResourceNames =
    {
        "ImdbApi.Migration.001_CreateCoreTables.sql",
        "ImdbApi.Migration.002_CreateStoredProcedures.sql"
    };
    private readonly string _connectionString;

    public DatabaseInitializer(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionString:IMDBDB"]
            ?? throw new InvalidOperationException(
                "The connection string 'ConnectionString:IMDBDB' is not configured.");
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        var targetBuilder = new SqlConnectionStringBuilder(_connectionString);
        if (string.IsNullOrWhiteSpace(targetBuilder.InitialCatalog))
        {
            throw new InvalidOperationException(
                "The connection string must specify a database using Initial Catalog or Database.");
        }

        var databaseName = targetBuilder.InitialCatalog;
        var masterBuilder = new SqlConnectionStringBuilder(_connectionString)
        {
            InitialCatalog = "master"
        };

        await using (var masterConnection = new SqlConnection(masterBuilder.ConnectionString))
        {
            await masterConnection.OpenAsync(cancellationToken);

            await using var createDatabase = masterConnection.CreateCommand();
            createDatabase.CommandText = """
                IF DB_ID(@databaseName) IS NULL
                BEGIN
                    DECLARE @createDatabaseSql nvarchar(max) =
                        N'CREATE DATABASE ' + QUOTENAME(@databaseName);
                    EXEC sys.sp_executesql @createDatabaseSql;
                END;
                """;
            createDatabase.Parameters.Add("@databaseName", SqlDbType.NVarChar, 128).Value = databaseName;
            try
            {
                await createDatabase.ExecuteNonQueryAsync(cancellationToken);
            }
            catch (SqlException exception) when (exception.Number == 1801)
            {
                await using var verifyDatabase = masterConnection.CreateCommand();
                verifyDatabase.CommandText = "SELECT DB_ID(@databaseName);";
                verifyDatabase.Parameters.Add("@databaseName", SqlDbType.NVarChar, 128).Value = databaseName;
                var databaseId = await verifyDatabase.ExecuteScalarAsync(cancellationToken);
                if (databaseId is null or DBNull)
                    throw;
            }
        }

        await using var databaseConnection = new SqlConnection(targetBuilder.ConnectionString);
        await databaseConnection.OpenAsync(cancellationToken);

        foreach (var migrationResourceName in MigrationResourceNames)
        {
            var migrationSql = ReadMigrationScript(migrationResourceName);
            var batches = Regex.Split(migrationSql, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            foreach (var batch in batches)
            {
                if (string.IsNullOrWhiteSpace(batch))
                    continue;

                await using var applyMigration = databaseConnection.CreateCommand();
                applyMigration.CommandText = batch;
                applyMigration.CommandTimeout = 120;
                await applyMigration.ExecuteNonQueryAsync(cancellationToken);
            }
        }
    }

    private static string ReadMigrationScript(string resourceName)
    {
        using var stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException(
                $"Embedded database migration '{resourceName}' was not found.");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}
