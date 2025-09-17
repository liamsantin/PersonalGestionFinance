<script setup>
import { useRouter } from 'vue-router'
import axios from 'axios'
import api from '@/plugins/axios'
import { useAuthStore } from '@/stores/auth'

const router = useRouter()
const auth = useAuthStore()
const username = ref('')
const password = ref('')

async function Log() {
  try {
    await auth.login(username.value, password.value)
    router.push('/home') // ⚡ la redirection se fait ici
  } catch (error) {
    alert('Identifiants invalides')
  }
}

</script>
<template>
  <v-container class="d-flex justify-center align-center" style="height:100vh;">
    <v-card class="pa-6" max-width="400" width="400">
      <v-card-title class="title">Connexion</v-card-title>
      <v-card-text>
        <v-text-field v-model="username" label="Nom d'utilisateur" outlined dense />
        <v-text-field v-model="password" label="Mot de passe" type="password" outlined dense />
      </v-card-text>
      <v-btn color="primary" block @click="Log">Se connecter</v-btn>
      <v-card-actions>
        <v-btn color="primary" block to="/register">Créer un compte</v-btn>
      </v-card-actions>
    </v-card>
  </v-container>
</template>
<style scoped>

  .title {
    text-align: center;
    font-size: 32px;
  }

</style>
