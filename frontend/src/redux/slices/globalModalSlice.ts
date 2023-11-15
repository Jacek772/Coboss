import { createSlice } from '@reduxjs/toolkit'
import GlobalModalButtonsTypeEnum from '../../components/GlobalModal/types/GlobalModalButtonsTypeEnum'
import GlobalModalClickResultEnum from '../../components/GlobalModal/types/GlobalModalClickResultEnum'
import GlobalModalTypeEnum from '../../components/GlobalModal/types/GlobalModalTypeEnum'
import ReduxActionType from '../types/ReduxActionType'

const initialState = {
  visible: false,
  data: {
    key: "",
    title: "Czy chcesz zapisać zmiany?",
    text: "", //"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque faucibus eleifend odio, sed ultrices augue tincidunt laoreet. Nam id orci id mauris fermentum pharetra sed nec sem. Pellentesque quis pharetra magna. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque faucibus eleifend odio, sed ultrices augue tincidunt laoreet. Nam id orci id mauris fermentum pharetra sed nec sem. Pellentesque quis pharetra magna.",
    modalType: GlobalModalTypeEnum.Info,
    buttonsType: GlobalModalButtonsTypeEnum.YesNo
  },
  result: {
    clickResult: GlobalModalClickResultEnum.None
  }
}

export const globalModalSlice = createSlice({
  name: "globalModal",
  initialState,
  reducers: {
    setGlobalModalVisibility: (state, action: ReduxActionType<boolean>) => {
      state.visible = action.payload
    },
    setGlobalModalData: (state, action: ReduxActionType<typeof initialState.data>) => {
      state.data = { ...state.data, ...action.payload }
    },
    setGlobalModalClickResult: (state, action: ReduxActionType<GlobalModalClickResultEnum>) => {
      state.result.clickResult = action.payload
    }
  },
})

export const { setGlobalModalVisibility, setGlobalModalData, setGlobalModalClickResult } = globalModalSlice.actions

export default globalModalSlice.reducer

export type GlobalModalState = ReturnType<typeof globalModalSlice.getInitialState>