<template>
  <div class="sidebar d-flex flex-column">
    <router-link class="nav-link text-white mb-3" to="/categories">
      <span><img :src="categoriesLogo" alt="Categories" class="sidebar-logo"></span>
      Categories
    </router-link>
    <router-link class="nav-link text-white" to="/spendings">
      <span><img :src="spendingsLogo" alt="Spendings" class="sidebar-logo"></span>
      Spendings
    </router-link>
  </div>
</template>

<script setup>
  import categoriesLogo from "../assets/categories-logo.svg"
  import spendingsLogo from "../assets/money-logo.svg"
</script>

<style scoped>
.sidebar {
  background-color: #212529;
  width: 220px;
  min-height: 100vh;
  flex-shrink: 0;
  padding: 1rem;
  color: white;
}

.nav-link {
  padding-top: 10px;
  padding-bottom: 10px;
  display: flex;
  align-items: center;
}

.nav-link:hover {
  background-color: #5a5ed8;
  border-radius: 4px;
  padding-left: 8px;
}

.sidebar-logo {
  width: 20px;
  height: 20px;
  margin-right: 10px;
}
</style>