import Vue from 'vue';
import VueRouter from 'vue-router';
import Login from '../views/LoginPage/Login.vue';
import Register from '../views/RegisterPage/Register.vue';

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Login,
  },
  {
    path: '/register',
    name: 'Register',
    component: Register,
  },
];

const router = new VueRouter({
  routes,
});

export default router;
