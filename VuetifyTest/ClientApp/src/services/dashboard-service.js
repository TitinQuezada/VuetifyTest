import httpClient from '../http-client';

class DashBoardService {
  getDashBoardData() {
    return httpClient.get('api/DashBoard');
  }
}

export default new DashBoardService();
