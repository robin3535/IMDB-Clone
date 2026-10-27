import VueRouter from "vue-router";
import MovieList from "./pages/Movies/MoviesList.vue"
import AddNewMovie from "./pages/Movies/AddNewMovie.vue"
const routes = [
    {path:"/",redirect:"/movies"},
    {path:"/movies",component:MovieList},
    {path:"/movies/add",component:AddNewMovie},
    {path:"/movies/edit/:id",component:AddNewMovie,props:true}
];

const router = new VueRouter({
    mode: "history",
    routes: routes,
  });
  
export default router;