Feature: Genre Resource
Genre feature will testify the get genre,get genre by Id,create genre,delete and update genre.
@GetAllGenres
Scenario: Get All Genres
	Given I am a client
	When I make GET Request '/genres'
	Then response code must be '200'
	And response data must look like from file 'C:\Users\Robin\Desktop\Genre.txt'

@GetGenreById
Scenario Outline: Get Genre by Id 
	Given I am a client
	When I make GET Request '/genres' with parameter '<genreId>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples:
	| genreId | statusCode | responseData              |
	| 1       | 200        | {"id":1,"name":"Drama"}   |
	| 5       | 404        | No Genre found with id= 5 |

@AddGenre
Scenario Outline: Add a Genre
	Given I am a client
	When I make POST Request '/genres' with body '<genreBody>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples: 
	| genreBody         | statusCode | responseData          |
	| {"name":"Horror"} | 201        | {"id":1}              |
	| {"name":""}       | 400        | Name cannot be empty. |
	


@UpdateGenre
Scenario Outline: Update a Genre
	Given I am a client
	When I make PUT Request '/genres' with parameter '<genreId>' and body '<genreBody>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples: 
	| genreId | genreBody         | statusCode | responseData              |
	| 1       | {"name":"Action"} | 200        | Updated Succesfully.      |
	| 1       | {"name":""}       | 400        | Name cannot be empty.     |
	| 0       | {"name":"Action"} | 404        | No Genre found with id= 0 |


@DeleteGenre
Scenario Outline: Delete a genre
	Given I am a client
	When I make DELETE Request '/genres' with parameter '<genreId>'
	Then response code must be '<statusCode>'
	And response data must look like '<responseData>'
	Examples: 
	| genreId | statusCode | responseData              |
	| 1       | 200        | Deleted Successfully.     |
	| 0       | 404        | No Genre found with id= 0 |