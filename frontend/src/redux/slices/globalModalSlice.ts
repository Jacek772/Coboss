import { createSlice } from '@reduxjs/toolkit'
import { v4 as uuidv4 } from 'uuid';

import GlobalModalButtonsTypeEnum from '../../components/GlobalModal/types/GlobalModalButtonsTypeEnum'
import GlobalModalClickResultEnum from '../../components/GlobalModal/types/GlobalModalClickResultEnum'
import GlobalModalTypeEnum from '../../components/GlobalModal/types/GlobalModalTypeEnum'
import ReduxActionType from '../types/ReduxActionType'

const initialState = {
  visible: false,
  data: {
    key: "",
    title: "Czy chcesz zapisać zmiany?",
    text: "",
    modalType: GlobalModalTypeEnum.Info,
    buttonsType: GlobalModalButtonsTypeEnum.YesNo
  },
  result: {
    key: "",
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
      state.result.key = uuidv4()
      state.result.clickResult = action.payload
    }
  },
})

export const { setGlobalModalVisibility, setGlobalModalData, setGlobalModalClickResult } = globalModalSlice.actions

export default globalModalSlice.reducer

export type GlobalModalState = ReturnType<typeof globalModalSlice.getInitialState>