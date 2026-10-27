<template>
  <div>
    <the-header></the-header>
    <router-view></router-view>
  </div>
</template>

<script>
import TheHeader from './components/TheHeader.vue';

export default {
  name: 'App',

  components: {
    TheHeader,
  },

  data: () => ({
    //
  }),
};
</script>

<style scoped>
@import url("https://fonts.googleapis.com/css2?family=Ubuntu&display=swap");
@import url("https://fonts.googleapis.com/css2?family=Ubuntu&family=Varela+Round&display=swap");

* {
  font-family: "Ubuntu", sans-serif;
}
.example::-webkit-scrollbar {
  display: none;
}
</style>
