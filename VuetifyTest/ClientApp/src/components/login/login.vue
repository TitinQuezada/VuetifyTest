<template>
  <v-container fill-height>
    <v-row justify="center">
      <v-form
        @submit.prevent="login"
        ref="form"
        lazy-validation
        class="text-center form pa-3"
      >
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
            <h3 class="text-center primary--text">Inicio de sessión</h3>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12">
            <v-text-field
              v-model="username"
              label="Usuario"
              :rules="userRules"
            ></v-text-field>
          </v-col>

          <v-col cols="12">
            <v-text-field
              v-model="password"
              type="password"
              label="Contraseña"
              :rules="passwordRules"
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12">
            <v-btn type="submit" block color="success">
              Iniciar sessión
            </v-btn>
          </v-col>

          <v-col cols="12">
            <router-link to="/register" tag="div">
              <v-btn block color="primary" outlined> Registrarse </v-btn>
            </router-link>
          </v-col>

          <v-col cols="12">
            <router-link to="/">Olvide mi contraseña</router-link>
          </v-col>
        </v-row>
      </v-form>
    </v-row>
  </v-container>
</template>

<script>
import login from './login';

export default login;
</script>
