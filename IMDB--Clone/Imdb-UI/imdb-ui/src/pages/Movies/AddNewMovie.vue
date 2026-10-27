<template>
    <div data-app>
        <movie-form 
            :id="id"
            @save-data="saveData">
        </movie-form>
    </div>
</template>

<script>
import MovieForm from "../../components/Movies/MovieForm.vue"
import { mapGetters } from "vuex";
export default {
    props:['id'],
    data() {
        return {
            addCast: false,
        }
    },
    components: {
        MovieForm
    },
    computed: {
        ...mapGetters({
            movies:'movies/movies',
            actors: 'actors/actors',
            genres: 'genres/genres',
            producers: 'producers/producers'
        }),
    
    },
    methods: {
        async saveData(data) {
            console.log(data)
            try {
                if(data.action === 'Add'){
                    await this.$store.dispatch('movies/addMovie', data);
                }else{
                    await this.$store.dispatch('movies/updateMovie',data);
                }
                
            } catch (e) {
                this.error = e.message || 'something went wrong';
            }
            
        }
    }
}
</script>

<style scoped>
h2 {
    margin-top: 40px;
}
</style>