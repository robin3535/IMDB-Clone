<template>
    <div class="app-bar-container">
                <v-app-bar dense dark fixed>
                    <img src="../assets/logo.jpg" height="47px">

                    <v-toolbar-title>Imdb</v-toolbar-title>

                    <v-spacer></v-spacer>
                    
                        <router-link to="/movies">Movies</router-link>
                        <router-link to="/movies">Actors</router-link>
                        <router-link to="/movies">Producers</router-link>
                    
                   
                </v-app-bar>
    </div>
</template>
<style scoped>
@import url("https://fonts.googleapis.com/css2?family=Ubuntu&display=swap");
@import url("https://fonts.googleapis.com/css2?family=Ubuntu&family=Varela+Round&display=swap");

* {
    margin: 0px;
    padding: 0px;
    font-family: "Ubuntu", sans-serif;
}
.app-bar-container{
    min-width: 100vw;
    margin-bottom: 80px;
}
a{
    text-decoration: none;
    color: white;
    padding: 20px;
}
a:hover{
    color: blue;
}



</style>