<template>
  <div v-if="show" class="modal fade show d-block" tabindex="-1" role="dialog" style="background: rgba(0,0,0,0.5);">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header modal-top-section text-white">
          <h5 class="modal-title">{{ form.id ? 'Edit' : 'New' }} Category</h5>
          <button type="button" class="btn-close" @click="closeModal"></button>
        </div>
        <div class="modal-body">
          <div class="mb-3">
            <label class="form-label">Name</label>
            <input v-model="form.name" type="text" class="form-control" maxlength="100" required />
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
import { ref, watchEffect } from 'vue'
import { updateCategory, createCategory } from '../api/financeApi'

const props = defineProps({
  show: Boolean,
  category: Object
})

const emit = defineEmits(['update:show', 'saved'])

const form = ref({ name: '' })

watchEffect(() => {
  if (props.show) {
    form.value = props.category && props.category.id
      ? { ...props.category }
      : { name: '' }
  }
})

const clear = () => {
  form.value = { name: '' }
}

const submit = async () => {
  if (form.value.name.trim() === '') return

  try {
    if (form.value.id) {
      await updateCategory(form.value.id, form.value)
    } else {
      const payload = { name: form.value.name }
      await createCategory(payload)
    }
    emit('saved')
    closeModal()
  } catch (error) {
    if (error.response) {
      console.error('API error:', error.response.data)
      alert('Error: ' + JSON.stringify(error.response.data))
    } else {
      console.error(error)
    }
  }
}

const closeModal = () => emit('update:show', false)
</script>

<style lang="css" scoped>
  .modal-top-section, .submit-button {
    background-color: #696CFF;
    color: white;
  }

  .submit-button:hover {
    background-color: #5e60db;
    color: white;
  }
</style>