<template>
  <v-row class="ma-3">
    <v-col cols="6">
      <v-card elevation="2" class="pa-3 white--text" color="success">
        <h3>Usuarios registrados:</h3>
        <span>{{ registeredUsersQuantity }}</span>
      </v-card>
    </v-col>

    <v-col cols="6">
      <v-card elevation="2" class="pa-3 white--text" color="primary">
        <h3>Usuarios activos:</h3>
        <span>{{ activeUsersQuantity }}</span>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
import Dashboard from './dashboard';

export default Dashboard;
</script>
