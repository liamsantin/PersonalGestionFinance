<script lang="ts" setup>
  import { ref } from 'vue'
  import { useRoute } from 'vue-router'


  const drawer = ref(true)
  const rail = ref(true)

  const route = useRoute()
  const showDrawer = computed(() => route.name !== 'Login')

  console.log(route.name)
</script>
<template>

    
    <v-layout>
      <v-navigation-drawer
        v-model="drawer"
        :rail="rail"
        permanent
        @click="rail = false"
        v-if="showDrawer"
      >
        <v-list>
          <v-list-item
            prepend-avatar="https://randomuser.me/api/portraits/men/85.jpg"
            title="John Leider"
          >
            <template v-slot:append>
              <v-btn
                icon="mdi-chevron-left"
                variant="text"
                @click.stop="rail = !rail"
              ></v-btn>
            </template>
          </v-list-item>
        </v-list>

        <v-divider></v-divider>

        <v-list density="compact" nav>
          <v-list-item
            prepend-icon="mdi-home-city"
            title="Home"
            value="home"
            to="/home"
          ></v-list-item>
          <v-list-item
            prepend-icon="mdi-account"
            title="My Account"
            value="account"
            to="/user"
          ></v-list-item>
          <v-list-item
            prepend-icon="mdi-account-group-outline"
            title="Users"
            value="users"
          ></v-list-item>
        </v-list>
      </v-navigation-drawer>
      
      <v-main>
        <RouterView />
      </v-main>

    </v-layout>
   

  <AppFooter />
</template>


