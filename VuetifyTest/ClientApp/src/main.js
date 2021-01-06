import Vue from 'vue';
import App from './App.vue';
import router from './router/Router';
import vuetify from './plugins/vuetify';
import VueToastr2 from 'vue-toastr-2';
import 'vue-toastr-2/dist/vue-toastr-2.min.css';

window.toastr = require('toastr');

Vue.config.productionTip = false;
Vue.use(VueToastr2);

new Vue({
  router,
  vuetify,
  render: h => h(App)
}).$mount('#app');
