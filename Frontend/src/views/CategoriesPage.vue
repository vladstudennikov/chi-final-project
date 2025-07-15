<template>
  <div class="page-wrapper d-flex">
    <Sidebar />

    <div class="content-area flex-grow-1 p-4">
      <h2 class="mb-3">Cost Categories</h2>
      <button class="btn custom-btn mb-3" @click="addNewCategory">+ Add</button>

      <CategoryTable :categories="categories" @edit="editCategory" @delete="confirmDelete" />
      <CategoryModal 
        v-model:show="showModal"
        :category="selectedCategory"
        @saved="loadCategories"
      />

      <!-- Delete Confirmation Modal -->
      <div v-if="deleteId !== null" class="modal fade show d-block" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
          <div class="modal-content">
            <div class="modal-header" style="background-color: #696CFF; color: white;">
              <h5 class="modal-title">Confirm Delete</h5>
              <button type="button" class="btn-close" @click="deleteId = null"></button>
            </div>
            <div class="modal-body">Are you sure you want to delete this category?</div>
            <div class="modal-footer">
              <button class="btn btn-secondary" @click="deleteId = null">Cancel</button>
              <button class="btn" style="background-color: #696CFF; color: white;" @click="deleteConfirmed">Delete</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import Sidebar from '../components/Sidebar.vue'
import CategoryTable from '../components/CategoryTable.vue'
import CategoryModal from '../components/CategoryModal.vue'
import { getCategories, deleteCategory } from '../api/financeApi'

const categories = ref([])
const showModal = ref(false)
const selectedCategory = ref(null)
const deleteId = ref(null)

const loadCategories = async () => {
  const res = await getCategories()
  categories.value = res.data
}

const editCategory = (category) => {
  selectedCategory.value = { ...category }
  showModal.value = true
}

const confirmDelete = (id) => {
  deleteId.value = id
}

const addNewCategory = () => {
  selectedCategory.value = null
  showModal.value = true
}

const deleteConfirmed = async () => {
  await deleteCategory(deleteId.value)
  deleteId.value = null
  loadCategories()
}

onMounted(loadCategories)
</script>

<style scoped>
.custom-btn {
  background-color: #696CFF;
  color: white;
  border: none;
}

.custom-btn:hover {
  background-color: #5e60db;
}

.page-wrapper {
  min-height: 100vh;
  width: 100%;
}

.content-area {
  background-color: #f8f9fa;
}
</style>