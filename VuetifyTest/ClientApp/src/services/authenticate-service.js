import httpClient from '../http-client';

class AuthenticateService {
  authenticate(authenticateModel) {
    return httpClient.post('api/Authentications', authenticateModel);
  }

  get currentUser() {
    const currentUser = localStorage.getItem('currentUser');
    return JSON.parse(currentUser);
  }
}

export default new AuthenticateService();
