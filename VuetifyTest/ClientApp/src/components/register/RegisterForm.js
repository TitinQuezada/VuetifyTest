import UserService from '../../services/user-service';

export default {
  data() {
    return {
      user: {
        username: '',
        name: '',
        lastname: '',
        email: '',
        password: '',
      },
    };
  },

  methods: {
    validateForm() {
      return this.$refs.form.validate();
    },

    register() {
      if (this.validateForm()) {
        console.log('creando...');
        UserService.createUser(this.user);
      }
    },
  },

  computed: {
    usernameRules() {
      return [
        (value) => (!value ? 'El nomre de usuario es requerido' : true),
        (value) =>
          value?.length < 8
            ? 'El nomre de usuario debe contener 8 caracteres como minimo'
            : true,
        (value) =>
          value?.includes(' ')
            ? 'El nomre de usuario no puede contener espacios'
            : true,
      ];
    },

    nameRules() {
      return [
        (value) => (!value ? 'El nomre es requerido' : true),
        (value) =>
          value?.length < 3
            ? 'El nomre debe contener 3 caracteres como minimo'
            : true,
      ];
    },

    lastnameRules() {
      return [
        (value) => (!value ? 'El apellido es requerido' : true),
        (value) =>
          value?.length < 3
            ? 'El apellido debe contener 3 caracteres como minimo'
            : true,
      ];
    },

    emailRules() {
      return [
        (value) => (!value ? 'El correo electronico es requerido' : true),
        (value) => /.+@.+\..+/.test(value) || 'Correo electronico invalido',
      ];
    },

    passwordRules() {
      return [
        (value) => (!value ? 'La contraseña es requerida' : true),
        (value) =>
          !value?.match(/[a-z]/g)
            ? 'La contraseña debe contener por lo menos una minuscula'
            : true,

        (value) =>
          !value?.match(/[A-Z]/g)
            ? 'La contraseña debe contener por lo menos una mayuscula'
            : true,

        (value) =>
          !value?.match(/[0-9]/g)
            ? 'La contraseña debe contener por lo menos un numero'
            : true,

        (value) =>
          !value?.match(/^.{8,}$/g)
            ? 'La contraseña debe contener por lo menos un 8 caracteres'
            : true,
      ];
    },

    repeatPasswordRules() {
      return [
        (value) => (!value ? 'La contraseña es requerida' : true),
        (value) =>
          value != this.user.password ? 'Las contraseñas no coinciden' : true,
      ];
    },
  },
};
