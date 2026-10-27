import Vue from 'vue'
import App from './App.vue'
import router from './router.js'
import VueRouter from 'vue-router'
import vuetify from './plugins/vuetify'
import store from './store/index.js'
import BaseSpinner from './components/UI/BaseSpinner.vue'
Vue.config.productionTip = false
Vue.use(VueRouter);
Vue.component('base-spinner',BaseSpinner)

new Vue({
  store,
  vuetify,
  router,
  render: h => h(App)
}).$mount('#app')
