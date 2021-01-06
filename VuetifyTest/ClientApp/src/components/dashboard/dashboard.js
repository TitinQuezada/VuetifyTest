import DashBoardService from '../../services/dashboard-service';

export default {
  mounted() {
    this.getDashBoardData();
  },
  data() {
    return {
      registeredUsersQuantity: 0,
      activeUsersQuantity: 0
    };
  },
  methods: {
    async getDashBoardData() {
      const { data } = await DashBoardService.getDashBoardData();
      this.registeredUsersQuantity = data.registeredUsersQuantity;
      this.activeUsersQuantity = data.activeUsersQuantity;
    }
  }
};
