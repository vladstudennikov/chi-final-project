<template>
  <div class="page-wrapper d-flex">
    <Sidebar />
    <div class="content-area flex-grow-1 p-4">
      <h2 class="mb-3">Spendings</h2>
      <button class="btn custom-btn mb-3" @click="addNewSpending">+ Add</button>

      <SpendingTable :spendings="spendings" @edit="editSpending" @delete="confirmDelete" />
      <SpendingModal 
        v-model:show="showModal"
        :spending="selectedSpending"
        :categories="categories"
        @saved="loadSpendings"
      />

      <!-- Delete Confirmation Modal -->
      <div v-if="deleteId !== null" class="modal fade show d-block" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
          <div class="modal-content">
            <div class="modal-header" style="background-color: #696CFF; color: white;">
              <h5 class="modal-title">Confirm Delete</h5>
              <button type="button" class="btn-close" @click="deleteId = null"></button>
            </div>
            <div class="modal-body">Are you sure you want to delete this spending?</div>
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
import SpendingTable from '../components/SpendingTable.vue'
import SpendingModal from '../components/SpendingModal.vue'
import Sidebar from '../components/Sidebar.vue'
import { getSpendings, deleteSpending, getCategories } from '../api/financeApi'

const spendings = ref([])
const categories = ref([])
const showModal = ref(false)
const selectedSpending = ref(null)
const deleteId = ref(null)

const loadSpendings = async () => {
  const res = await getSpendings()
  spendings.value = res.data
}

const addNewSpending = async () => {
  selectedSpending.value = null
  showModal.value = true
}

const loadCategories = async () => {
  const res = await getCategories()
  categories.value = res.data
}

const editSpending = (spending) => {
  selectedSpending.value = { ...spending }
  showModal.value = true
}

const confirmDelete = (id) => {
  deleteId.value = id
}

const deleteConfirmed = async () => {
  await deleteSpending(deleteId.value)
  deleteId.value = null
  loadSpendings()
}

onMounted(() => {
  loadSpendings()
  loadCategories()
})
</script>

<style lang="css" scoped>
  .custom-btn {
    background-color: #696CFF;
    color: white;
    border: none;
  }

.custom-btn:hover {
  background-color: #5a5ed8;
}

.page-wrapper {
  min-height: 100vh;
  width: 100%;
}

.content-area {
  background-color: #f8f9fa;
}
</style>