import { defineStore } from "pinia";
import api from '@/plugins/axios';

export const useUserStore = defineStore('user', {
    actions: {
        async GetInformation() {
            try {
                const response = await api.post('/user/login',)
                return response.data.token
            } catch () {

            }
        }
    }
})