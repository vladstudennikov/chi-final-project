<script setup>
import { onMounted } from 'vue';
import { useAuthStore } from "../stores/authStore";

const auth = useAuthStore();
onMounted(() => {
  auth.loginFromStorage();
});

</script>

<template>
  <div v-if="!auth.loggedIn" class="flex-col-centerv h-screen mt-33">
    <h1 class="mb-10">Welcome</h1>
    <p>Create an account or log in to start using service</p>
  </div>

  <div v-else class="flex-col-centerv h-screen mt-33">
    <h1 class="mb-10">You're logged in</h1>
  </div>
</template>

<style scoped>
h1 {
  color: var(--primary-color);
}
</style>