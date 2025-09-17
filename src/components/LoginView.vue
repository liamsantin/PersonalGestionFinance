<!-- src/views/Login.vue -->
<template>
  <v-container class="d-flex justify-center align-center" style="height:100vh;">
    <v-card class="pa-6" max-width="400">
      <v-card-title>Connexion</v-card-title>
      <v-card-text>
        <v-text-field v-model="username" label="Nom d'utilisateur" outlined dense />
        <v-text-field v-model="password" label="Mot de passe" type="password" outlined dense />
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" block @click="login">Se connecter</v-btn>
      </v-card-actions>
    </v-card>
  </v-container>
</template>

<script setup>
import { useRouter } from 'vue-router'

const router = useRouter()
const username = ref('')
const password = ref('')

function login() {
  if (username.value === 'a' && password.value === 'a') {
    localStorage.setItem('authToken', 'fake-token') // ⚡ remplacer plus tard par un vrai JWT
    router.push('/home')
  } else {
    alert('Identifiants invalides')
  }
}
</script>