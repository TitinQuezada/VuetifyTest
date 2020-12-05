import axios from 'axios';
import currentUserService from './services/authenticate-service';
import router from './router/Router';

const httpClient = axios.create({
  baseURL: 'https://localhost:44336/',
  headers: {
    'Content-type': 'application/json',
  },
});

httpClient.interceptors.request.use(
  (configuration) => {
    const user = currentUserService.currentUser;

    if (user) {
      configuration.headers.common['Authorization'] = `Bearer ${user.token}`;
    }

    return configuration;
  },
  (error) => {
    return Promise.reject(error);
  }
);

httpClient.interceptors.response.use(
  (response) => {
    if (response.status === 200 || response.status === 201) {
      return Promise.resolve(response);
    } else {
      return Promise.reject(response);
    }
  },
  ({ response }) => {
    if (response.status === 401) {
      router.replace('/');
    }

    return Promise.reject(response);
  }
);

export default httpClient;
