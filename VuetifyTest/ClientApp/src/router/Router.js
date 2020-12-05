import Vue from 'vue';
import VueRouter from 'vue-router';
import Login from '../views/LoginPage/Login.vue';
import Register from '../views/RegisterPage/Register.vue';
import HomePage from '../views/HomePage/HomePage.vue';
import AuthenticateService from '../services/authenticate-service';

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Login,
    meta: {
      allowAnonymous: true,
    },
  },
  {
    path: '/register',
    name: 'Register',
    component: Register,
    meta: {
      allowAnonymous: true,
    },
  },
  {
    path: '/home',
    name: 'Home',
    component: HomePage,
    meta: {
      allowAnonymous: false,
    },
  },
];

const router = new VueRouter({
  routes,
});

router.beforeEach((to, from, next) => {
  if (!to.meta.allowAnonymous && !AuthenticateService.currentUser) {
    next({
      path: '/',
      query: { redirect: to.fullPath },
    });
  } else if (to.meta.allowAnonymous && AuthenticateService.currentUser) {
    next({
      path: 'home',
      query: { redirect: to.fullPath },
    });
  } else {
    next();
  }
});

export default router;
