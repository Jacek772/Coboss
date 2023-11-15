import { configureStore } from '@reduxjs/toolkit'

// Reducers
import authReducer from './slices/authSlice'
import globalModalReducer from './slices/globalModalSlice'

const store = configureStore({
  reducer: {
    auth: authReducer,
    globalModal: globalModalReducer
  },
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

export default store