import Vuex from 'vuex'
import Vue from 'vue'
import movies from './modules/movies/index.js'
import actors from './modules/actors/index.js'
import genres from './modules/genres/index.js'
import producers from './modules/producers/index.js'
Vue.use(Vuex);
const store = new Vuex.Store({
    modules:{
        movies,
        actors,
        genres,
        producers
    }
})
export default store;