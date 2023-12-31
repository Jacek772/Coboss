// Libraries
import { useCallback, useEffect, useState } from "react"
import { useDispatch, useSelector } from "react-redux"
import { v4 as uuidv4 } from 'uuid';

// Redux
import { GlobalModalState, setGlobalModalData, setGlobalModalVisibility } from "../../redux/slices/globalModalSlice";
import { RootState } from "../../redux/store";

// types
import UseGlobalModalState from "./types/UseGlobalModalState";
import ShowGlobalModalParams from "./types/ShowGlobalModalParams";


const useGlobalModal = (): [
  (params: ShowGlobalModalParams) => void,
  () => void
] => {
  const [state, setState] = useState<UseGlobalModalState>({
    key: "",
    callbacks: []
  })

  const globalModalState: GlobalModalState = useSelector<RootState, GlobalModalState>(x => x.globalModal)
  const dispatch = useDispatch()

  useEffect(() => {
    if(state.key !== globalModalState.data.key)
    {
      return
    }

    state.callbacks.forEach(callback => callback?.(globalModalState.result.clickResult))
    setState(s => ({
      ...state, 
      key: null,
      callbacks: []
    }))
  },[globalModalState.result])

  const showGlobalModal = useCallback((params: ShowGlobalModalParams) => {
    const key: string = uuidv4()
    setState({
      ...state,
      key,
      callbacks:[...state.callbacks, params.callback]
    })

    dispatch(setGlobalModalData({
      key,
      title: params.title, 
      text: params.text,
      buttonsType: params.buttonsType,
      modalType: params.modalType
    }))
    dispatch(setGlobalModalVisibility(true))
  }, [state, dispatch])

  const hideGlobalModal = useCallback(() => {
    dispatch(setGlobalModalVisibility(false))
  }, [dispatch])
  
  return [showGlobalModal, hideGlobalModal]
}

export default useGlobalModal