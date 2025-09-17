import axios from 'axios'

const api = axios.create({
    baseURL: 'http://localhost:5289/api', // ⚡ ton backend API
    headers: {
        'Content-Type': 'application/json'
    }
})

// Intercepteur pour ajouter automatiquement le JWT
api.interceptors.request.use(config => {
    const token = localStorage.getItem('authToken')
    if (token) {
        config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

export default api
