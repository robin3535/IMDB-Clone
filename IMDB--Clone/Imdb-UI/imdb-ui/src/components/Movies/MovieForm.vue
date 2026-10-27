<template>
    <v-container>
        <v-dialog v-model="addCastDialog" persistent max-width="40vw">
            <cast-form :title="addCastTitle" @add-cast="addCast" @close-dialog="addCastDialog = false"></cast-form>
        </v-dialog>
        <v-card>
            <v-card-title>
                <h2>{{ formTitle }}</h2>
            </v-card-title>
            <v-card-text>
                <v-form id="add-movie-form" ref="form" autofocus v-model="isValidForm" lazy-validation @submit.prevent>
                    <v-row>
                        <v-col>
                            <v-text-field label="Movie Name" v-model="name" solo type="string" required
                                :rules="[v => !!v || 'Name is required']">
                            </v-text-field>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col>
                            <v-text-field label="Year of release" v-model.number="yor" solo type="number" required
                                :rules="[v => !!v || 'Year of release is required']">
                            </v-text-field>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col cols="6">
                            <v-text-field label="Language" v-model="language" solo type="string" required
                                :rules="[v => !!v || 'Language is required']">
                            </v-text-field>
                        </v-col>
                        <v-col cols="6">
                            <v-text-field label="Profit" v-model.number="profit" solo type="number" required
                                :rules="[v => !!v || 'Profit is required']">
                            </v-text-field>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col>
                            <v-select label="Actors" :items="actors" item-text="name" item-value="id" multiple
                                v-model="selectedActors" solo required :rules="[v => !!v || 'Actors is required']"
                                @focus="getActors"></v-select>
                        </v-col>
                        <v-col>
                            <v-btn color="rgb(250,250,250)" class="mx-5" secondary id="add-actor-btn"
                                @click="openCastDialog('Actor')">Add Actor</v-btn>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col>
                            <v-select label="Producer" :items="producers" item-text="name" item-value="id"
                                v-model="selectedProducer" solo required :rules="[v => !!v || 'Producer is required']"
                                @focus="getProducers"></v-select>
                        </v-col>
                        <v-col>
                            <v-btn color="rgb(250,250,250) " class="mx-5" secondary id="add-producer-btn"
                                @click="openCastDialog('Producer')">Add
                                Producer</v-btn>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col cols="12">
                            <v-select label="Genres" :items="genres" item-text="name" item-value="id" multiple
                                v-model="selectedGenres" solo required :rules="[v => !!v || 'Genre is required']"
                                @focus="getGenres"></v-select>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col cols="12">
                            <v-textarea v-model="plot" label="Plot" solo type="string" required
                                :rules="[v => !!v || 'Plot is required']">Plot</v-textarea>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col>
                            <v-file-input :rules="fileRules" accept="image/png, image/jpeg, image/bmp" v-model="coverImage"
                                prepend-icon="mdi-camera" label="Cover image" solo></v-file-input>
                        </v-col>
                    </v-row>
                    <v-btn color="rgb(0,0,245)" width="120px" primary id="add-movie-btn" @click="submitForm">{{ action
                    }}</v-btn>
                </v-form>
            </v-card-text>
        </v-card>
    </v-container>
</template>
<script>
import CastForm from '../Casts/CastForm.vue';
import { mapGetters } from 'vuex';
// import { storage } from '../../firebase.js'
// import { ref,getBlob } from "firebase/storage";
export default {
    emits: ['save-data'],
    props: ['id'],
    components: {
        CastForm
    },
    data() {
        return {
            isValidForm: true,
            fileRules: [
                value => !value || value.size < 2000000 || 'Cover size should be less than 2 MB!',
            ],
            action: '',
            formTitle: '',
            castType: '',
            addCastTitle: '',
            addCastDialog: false,
            name: '',
            yor: null,
            plot: '',
            language: '',
            profit: null,
            selectedActors: null,
            selectedProducer: null,
            selectedGenres: null,
            coverImage: {},
        }
    },
    computed: {
        ...mapGetters({
            actors: 'actors/actors',
            producers: 'producers/producers',
            genres: 'genres/genres',
            movies: 'movies/movies',
            coverImageEdit: 'movies/coverImageEdit'
        })

    },
    methods: {
        submitForm() {
            console.log(this.selectedActors)
            var formData = {
                action: this.action,
                name: this.name,
                yor: this.yor,
                id: this.id,
                language: this.language,
                profit: this.profit,
                actors: this.selectedActors,
                producer: this.selectedProducer,
                genres: this.selectedGenres,
                plot: this.plot,
                coverImage: this.coverImage.name,
                file: this.coverImage
            };
            if (this.$refs.form.validate()) {
                this.$emit('save-data', formData);
                this.$refs.form.reset();
                this.$router.replace('/movies');
            }

        },
        openCastDialog(cast) {
            if (cast === "Actor") {
                this.addCastTitle = "Enter Actor Details";
                this.addCastDialog = true;
                this.castType = "Actor"
            }
            if (cast === "Producer") {
                this.addCastTitle = "Enter Producer Details";
                this.addCastDialog = true;
                this.castType = "Producer";
            }

        },
        async addCast(data) {
            try {
                if (this.castType === 'Actor') {
                    const id = await this.$store.dispatch('actors/addActor', data);
                    this.selectedActors.push(id);
                    this.addCastDialog = false;
                }
                if (this.castType === 'Producer') {
                    const id = await this.$store.dispatch('producers/addProducer', data);
                    this.selectedProducer = id;
                    this.addCastDialog = false;
                }

            } catch (e) {
                this.error = e.message || 'something went wrong';
            }
        },
        async setFormContent() {
            if (this.$route.path === "/movies/add") {
                this.formTitle = "Enter Movie Details";
                this.action = "Add";
            } else {
                var movie = this.movies.find(m => m.id == this.id);
                this.formTitle = "Edit Movie Details";
                this.action = "Edit";
                // const httpsReference = ref(storage, movie.coverImage);
                // console.log(httpsReference);
                // const blob = await getBlob(httpsReference,5000);
                // console.log(blob)
                this.name = movie.name;
                this.plot = movie.plot;
                this.yor = movie.yor;
                this.language = movie.language;
                this.profit = movie.profit;
                this.coverImage = new File(["foo"], movie.name + ".jpg", { type: "text/plain", })
                this.selectedActors = movie.actors.map((m) => m.id);
                this.selectedProducer = movie.producer.id;
                this.selectedGenres = movie.genres.map((g) => g.id);
            }
        },
        async getActors() {
            try {
                await this.$store.dispatch('actors/loadActors')
            } catch (e) {
                this.error = e.message || 'something went wrong';
            }
        },
        async getGenres() {
            try {
                await this.$store.dispatch('genres/loadGenres')
            } catch (e) {
                this.error = e.message || 'something went wrong';
            }
        },
        async getProducers() {
            try {
                await this.$store.dispatch('producers/loadProducers')
            } catch (e) {
                this.error = e.message || 'something went wrong';
            }
        },
    },
    created() {
        this.setFormContent();
    }

}
</script>

<style scoped>
#add-movie-form {
    padding: 40px;
}

#add-movie-btn {
    color: azure;
}

h2 {
    color: rgb(26, 98, 157);
    padding: 15px 0px 5px 34px;
}
</style>