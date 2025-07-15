<template>
  <table class="table table-bordered table-hover bg-white">
    <thead class="table-light">
      <tr>
        <th>ID</th>
        <th>Comment</th>
        <th>Created At</th>
        <th>Category ID</th>
        <th>Actions</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="spending in spendings" :key="spending.id">
        <td>{{ spending.id }}</td>
        <td>{{ spending.comment }}</td>
        <td>{{ new Date(spending.createdAt).toLocaleString() }}</td>
        <td>{{ spending.costCategoryId }}</td>
        <td class="d-flex justify-content-center gap-2">
          <button
            class="btn edit-button p-0 border-0 d-flex align-items-center justify-content-center"
            @click="$emit('edit', spending)"
          >
            <img :src="editButton" alt="Edit" style="width: 50%; height: 50%;" />
          </button>

          <button
            class="btn delete-button p-0 border-0 d-flex align-items-center justify-content-center"
            @click="$emit('delete', spending.id)"
          >
            <img :src="deleteButton" alt="Edit" style="width: 50%; height: 50%;" />
          </button>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script setup>
import editButton from "../assets/edit-button.svg"
import deleteButton from "../assets/delete-button.svg"

defineProps({
  spendings: Array
})

defineEmits(['edit', 'delete'])
</script>

<style lang="css" scoped>
  .edit-button {
    width: 40px; 
    height: 40px; 
    min-width: 20px; 
    background-color: #696CFF; 
    border-radius: 6px;
  }

  .edit-button:hover {
    background-color: #5e60db; 
  }

  .delete-button {
    width: 40px; 
    height: 40px; 
    min-width: 20px; 
    background-color: #696CFF; 
    border-radius: 6px;
  }

  .delete-button:hover {
    background-color: #5e60db; 
  }
</style>