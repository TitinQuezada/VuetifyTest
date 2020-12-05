import httpClient from '../http-client';

class UserService {
  createUser(user) {
    return httpClient.post('api/SystemUsers', user);
  }
}

export default new UserService();
