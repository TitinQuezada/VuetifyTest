import httpClient from '../http-client';

class AuthenticateService {
  authenticate(authenticateModel) {
    return httpClient.post('api/Authentications', authenticateModel);
  }

  get currentUser() {
    return JSON.parse(localStorage.getItem('currentUser'));
  }
}

export default new AuthenticateService();
