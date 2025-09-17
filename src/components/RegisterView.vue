<!-- src/views/Register.vue -->
<template>
  <v-container class="d-flex justify-center align-center" style="height:100vh;">
    <v-card class="pa-6" max-width="400">
      <v-card-title>Créer un compte</v-card-title>
      <v-card-text>
        <v-text-field v-model="username" label="Nom d'utilisateur" outlined dense />
        <v-text-field v-model="email" label="Email" outlined dense />
        <v-text-field v-model="password" label="Mot de passe" type="password" outlined dense />
        
      </v-card-text>
      <v-card-actions class="d-flex flex-column">
        <v-btn color="primary" block @click="register">S'inscrire</v-btn>
        <v-btn text block @click="goLogin">Déjà un compte ? Se connecter</v-btn>
      </v-card-actions>
    </v-card>
  </v-container>
</template>

<script setup>
import { useRouter } from 'vue-router'

const router = useRouter()
const username = ref('')
const email = ref('')
const password = ref('')

function register() {
  // ⚡ Ici tu appelleras ton API backend
  if (username.value && email.value && password.value) {
    // Simuler une inscription réussie
    localStorage.setItem('authToken', 'fake-token')
    router.push('/home')
  } else {
    alert('Veuillez remplir tous les champs')
  }
}

function goLogin() {
  router.push('/login')
}
</script>