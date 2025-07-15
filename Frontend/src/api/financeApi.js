import axios from 'axios'

const API_URL = 'https://localhost:7061/api'

const getToken = () => localStorage.getItem('authToken')

const axiosInstance = axios.create({
  baseURL: API_URL,
  headers: {
    Authorization: `Bearer ${getToken()}`
  }
})

export const getCategories = () => axiosInstance.get(`/CostCategories`)
export const getSpendings = () => axiosInstance.get(`/Spendings`)

export const deleteCategory = id => axiosInstance.delete(`/CostCategories/${id}`)
export const deleteSpending = id => axiosInstance.delete(`/Spendings/${id}`)

export const updateCategory = (id, data) => axiosInstance.put(`/CostCategories/${id}`, data)
export const updateSpending = (id, data) => axiosInstance.put(`/Spendings/${id}`, data)

export const createCategory = data => axiosInstance.post(`/CostCategories`, data)
export const createSpending = data => axiosInstance.post(`/Spendings`, data)