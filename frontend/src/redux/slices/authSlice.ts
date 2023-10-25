import { createSlice } from '@reduxjs/toolkit'

export const authSlice = createSlice({
  name: "auth",
  initialState: {
    logged: false,
    user: {
      login: ""
    },
  },
  reducers: {
    setLogged: (state) => {
      state.logged = true
    },
    setUnlogged: (state) => {
      state.logged = false
    },
    setUser: (state, action) => {
      state.user = action.payload
    },
  },
})

export const { setLogged, setUnlogged, setUser } = authSlice.actions

export default authSlice.reducer