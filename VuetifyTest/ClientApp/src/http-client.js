import axios from 'axios';
import currentUserService from './services/authenticate-service';

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

export default httpClient;
