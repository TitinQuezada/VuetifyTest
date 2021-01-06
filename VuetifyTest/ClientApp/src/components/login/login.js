import AuthenticateService from '../../services/authenticate-service';

export default {
  data() {
    return {
      username: '',
      password: ''
    };
  },

  methods: {
    validateForm() {
      return this.$refs.form.validate();
    },
    async login() {
      try {
        if (this.validateForm()) {
          const authenticateModel = {
            username: this.username,
            password: this.password
          };

          const { data } = await AuthenticateService.authenticate(
            authenticateModel
          );

          localStorage.setItem('currentUser', JSON.stringify(data));

          this.$router.push('home');
        }
      } catch ({ data }) {
        this.$toastr.error(data, 'Ha ocurrido un error inesperado');
      }
    }
  },
  computed: {
    userRules() {
      return [value => !!value || 'El usuario es requerido'];
    },
    passwordRules() {
      return [value => !!value || 'La contraseña es requerida'];
    }
  }
};
