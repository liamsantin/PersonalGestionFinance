import { defineStore } from 'pinia'
import api from '@/plugins/axios'

interface AuthState {
    token: string | null
}

export const useAuthStore = defineStore('auth', {
    state: (): AuthState => ({
        token: localStorage.getItem('authToken') || null
    }),

    getters: {
        isAuthenticated: (state) => !!state.token
    },

    actions: {
        async login(username: string, password: string) {
            const response = await api.post('/auth/login', { username, password })
            const token = response.data.token

            if (!token) {
                throw new Error('Token manquant')
            }

            this.token = token
            localStorage.setItem('authToken', token)
        },

        logout() {
            this.token = null
            localStorage.removeItem('authToken')
        }
    }
})
