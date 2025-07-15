<template>
  <div v-if="show" class="modal fade show d-block" tabindex="-1" role="dialog" style="background: rgba(0,0,0,0.5);">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header modal-top-section text-white">
          <h5 class="modal-title">{{ form.id ? 'Edit' : 'New' }} Spending</h5>
          <button type="button" class="btn-close" @click="closeModal"></button>
        </div>
        <div class="modal-body">
          <div class="mb-3">
            <label class="form-label">Comment</label>
            <input v-model="form.comment" type="text" class="form-control" maxlength="150" />
          </div>
          <div class="mb-3">
            <label class="form-label">Created At</label>
            <input v-model="form.createdAt" type="datetime-local" class="form-control" />
          </div>
          <div class="mb-3">
            <label class="form-label">Category</label>
            <select v-model="form.costCategoryId" class="form-select">
              <option v-for="cat in categories" :key="cat.id" :value="cat.id">{{ cat.name }}</option>
            </select>
          </div>
        </div>
        <div class="modal-footer">
          <button class="btn btn-secondary" @click="clear">Clear</button>
          <button class="btn submit-button" @click="submit">Submit</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, watch } from 'vue'
import { createSpending, updateSpending } from '../api/financeApi'

const props = defineProps({
  show: Boolean,
  spending: Object,
  categories: Array
})

const emit = defineEmits(['update:show', 'saved'])

const form = ref({
  comment: '',
  createdAt: new Date().toISOString().slice(0, 16),
  costCategoryId: null
})

watch(() => props.spending, (newVal) => {
  form.value = newVal
    ? {
        ...newVal,
        createdAt: new Date(newVal.createdAt).toISOString().slice(0, 16)
      }
    : {
        comment: '',
        createdAt: new Date().toISOString().slice(0, 16),
        costCategoryId: props.categories[0]?.id || null
      }
})

const clear = () => {
  form.value = {
    comment: '',
    createdAt: new Date().toISOString().slice(0, 16),
    costCategoryId: props.categories[0]?.id || null
  }
}

const submit = async () => {
  const payload = {
    comment: form.value.comment,
    createdAt: new Date(form.value.createdAt).toISOString(),
    costCategoryId: form.value.costCategoryId
  }

  console.log('Sending payload:', {
    id: form.value.id,
    comment: form.value.comment,
    createdAt: new Date(form.value.createdAt).toISOString(),
    costCategoryId: form.value.costCategoryId
  })

  try {
    if (form.value.id) {
      await updateSpending(form.value.id, payload)
    } else {
      await createSpending(payload)
    }
  } catch (error) {
    if (error.response) {
      console.error('API error:', error.response.data)
      alert('Error: ' + JSON.stringify(error.response.data))
    } else {
      console.error(error)
    }
  }

  emit('saved')
  closeModal()
}

const closeModal = () => emit('update:show', false)
</script>

<style lang="css" scoped>
  .modal-top-section, .submit-button {
    background-color: #696CFF;
  }
</style>