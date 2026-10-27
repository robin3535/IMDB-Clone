<template>
    <div data-app>
        <v-row justify="center">
            <v-dialog v-model="deleteDialog" persistent max-width="320">
                <v-card>
                    <v-card-title>
                        Do you really want to delete this movie?
                    </v-card-title>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="rgb(255,0,0)" text @click="deleteDialog = false">
                            Disagree
                        </v-btn>
                        <v-btn color="rgb(0,255,0)" text @click="confirmDelete(deletedMovieId)">
                            Agree
                        </v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-row>
        <v-dialog v-model="movieDialog" persistent max-width="40vw">
            <movie-details :title="name" 
            :plot="plot" 
            :yor="yor" 
            :actors="actors" 
            :producer="producer" 
            :genres="genres"
            :coverImage="coverImage" 
            @close-dialog="movieDialog = false">
            </movie-details>
        </v-dialog>
        <v-btn elevation="2" raised id="addBtn" @click="addMovie">Add Movie</v-btn>
        <v-container>
            <div v-if="this.isLoading">
                <base-spinner></base-spinner>
            </div>
            <v-row v-else-if="!this.isLoading && this.hasMovies">
                <v-col v-for="movie in movies" :key="movie.id" cols="12" sm="4" md="4" xs="1" class="ml-md-auto">
                    <movie-item 
                    :id="movie.id" 
                    :name="movie.name" 
                    :plot="movie.plot" 
                    :coverImage="movie.coverImage"
                    @explore="openMovieDialog" 
                    @edit="editMovie" 
                    @delete="deleteMovie">
                    </movie-item>
                </v-col>
            </v-row>
            <v-card v-else>
                <v-card-text>
                    <h3>No Movies Found</h3>
                </v-card-text>
            </v-card>

        </v-container>
    </div>
</template>
<script>
import { mapGetters } from 'vuex';
import MovieItem from '@/components/Movies/MovieItem.vue';
import MovieDetails from '@/components/Movies/MovieDetails.vue';
export default {
    components: {
        MovieItem,
        MovieDetails
    },
    data() {
        return {
            deletedMovieId: null,
            deleteDialog: false,
            isLoading: false,
            movieDialog: false,
            yor: '',
            actors: [],
            producer: '',
            genres: [],
            name: '',
            plot: '',
            coverImage: '',
        }
    },
    watch: {
        isDelete(value) {
            this.isDelete = value;
        }
    },
    computed: {
        ...mapGetters({
            movies: 'movies/movies',
            hasMovies: 'movies/hasMovies'
        })

    },
    methods: {
        addMovie() {
            this.$router.push("/movies/add");
        },
        async loadMovies() {
            this.isLoading = true;
            try {
                await this.$store.dispatch('movies/loadMovies')
            } catch (e) {
                this.error = e.message || 'something went wrong';
            }
            this.isLoading = false;
        },
        openMovieDialog(id) {
            console.log(id);
            var movie = this.movies.find((m) => m.id === id);
            this.name = movie.name;
            this.yor = movie.yor;
            this.coverImage = movie.coverImage;
            this.plot = movie.plot;
            this.actors = movie.actors.map((a) => a.name);
            this.producer = movie.producer.name;
            this.genres = movie.genres.map((g) => g.name);
            this.movieDialog = true;
        },
        editMovie(id) {
            this.$router.push('/movies/edit/' + id);
        },
        async deleteMovie(id) {
            this.deletedMovieId = id;
            this.deleteDialog = true;
        },
        async confirmDelete(id) {
            try {
                await this.$store.dispatch('movies/deleteMovie', id);
                this.deleteDialog = false;
            } catch (e) {
                this.error = e.message || 'something went wrong';
                this.deleteDialog = false;
            }
        }
    },
    created() {
        this.loadMovies();
    }
}
</script>

<style scoped>
@import url("https://fonts.googleapis.com/css2?family=Ubuntu&display=swap");
@import url("https://fonts.googleapis.com/css2?family=Ubuntu&family=Varela+Round&display=swap");

* {
    font-family: "Ubuntu", sans-serif;
}

#addBtn {
    margin-left: 40px;
    margin-bottom: 20px;

}
</style>