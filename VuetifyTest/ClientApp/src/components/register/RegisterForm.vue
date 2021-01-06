<template>
  <v-container fill-height>
    <v-row justify="center">
      <v-form ref="form" class="text-center form pa-3">
        <v-row>
          <v-col>
            <img
              src="../../assets/images/user3.png"
              alt="user image"
              width="100rem"
            />
          </v-col>
        </v-row>

        <v-row>
          <v-col>
            <h3 class="text-center primary--text">Registro</h3>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="user.username"
              label="Nombre de usuario"
              :rules="usernameRules"
            ></v-text-field>
          </v-col>

          <v-col cols="12" md="6">
            <v-text-field
              v-model="user.name"
              label="Nombre"
              :rules="nameRules"
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="user.lastname"
              label="Apellido"
              :rules="lastnameRules"
            ></v-text-field>
          </v-col>

          <v-col cols="12" md="6">
            <v-text-field
              v-model="user.email"
              type="email"
              label="Correo electronico"
              :rules="emailRules"
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="user.password"
              type="password"
              label="Contraseña"
              :rules="passwordRules"
            ></v-text-field>
          </v-col>

          <v-col cols="12" md="6">
            <v-text-field
              type="password"
              label="Repetir contraseña"
              :rules="repeatPasswordRules"
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12">
            <v-btn block color="success" @click="register"> Registrarse </v-btn>
          </v-col>

          <v-col cols="12">
            <router-link to="/" tag="div">
              <v-btn block color="primary" outlined> Volver al login </v-btn>
            </router-link>
          </v-col>
        </v-row>
      </v-form>
    </v-row>
  </v-container>
</template>

<script>
import RegisterForm from './RegisterForm';

export default RegisterForm;
</script>

<style scoped>
form {
  width: 40rem;
}
</style>
