// React
import { Dispatch, useCallback } from "react"
import { useDispatch, useSelector } from "react-redux"
import { AnyAction } from "@reduxjs/toolkit"

// Redux
import { RootState } from "../../redux/store"
import { GlobalModalState, setGlobalModalClickResult } from "../../redux/slices/globalModalSlice"

// Types
import GlobalModalButtonsTypeEnum from "./types/GlobalModalButtonsTypeEnum"
import GlobalModalClickResultEnum from "./types/GlobalModalClickResultEnum"
import GlobalModalTypeEnum from "./types/GlobalModalTypeEnum"

// Css
import "./index.css"

const GlobalModal: React.FC = () => {
  const dispatch: Dispatch<AnyAction> = useDispatch()

  const globalModalState = useSelector<RootState, GlobalModalState>(x => x.globalModal)

  const handleClick = useCallback((clickResult: GlobalModalClickResultEnum) => {
    dispatch(setGlobalModalClickResult(clickResult))
  }, [])

  const handleClickClose = useCallback(() => {
    dispatch(setGlobalModalClickResult(GlobalModalClickResultEnum.Close))
  }, [])

  const getTypeClassName = useCallback(() => {
    switch(globalModalState.data.modalType)
    {
      case GlobalModalTypeEnum.Warning:
        return "globalmodal-warning"
      case GlobalModalTypeEnum.Danger:
        return "globalmodal-danger"
      case GlobalModalTypeEnum.Success:
        return "globalmodal-success"
      case GlobalModalTypeEnum.Info:
      default:
        return "globalmodal-info"
    }

  }, [globalModalState.data.modalType])

  const getButtons = useCallback(() => {
    switch(globalModalState.data.buttonsType)
    {
      case GlobalModalButtonsTypeEnum.Ok:
        return <>
          <button 
            className="button button-primary"
            onClick={() => handleClick(GlobalModalClickResultEnum.Ok)}>Ok</button>
        </>
      case GlobalModalButtonsTypeEnum.YesNo:
        return <>
          <button 
            className="button button-primary"
            onClick={() => handleClick(GlobalModalClickResultEnum.Yes)}>Yes</button>
          <button
            className="button button-primary" 
            onClick={() => handleClick(GlobalModalClickResultEnum.No)}>No</button>
        </>
      default:
        return <></>
    }
  }, [handleClick, globalModalState.data.buttonsType])

  if(!globalModalState.visible)
  {
    return null
  }

  return <div className={`globalmodal-container ${getTypeClassName()}`}>
    <img className="globalmodal-x"
      onClick={handleClickClose}
      src="./gfx/svg/close.svg" 
      alt="close" />
    <div className="globalmodal-text-container">
      <h2 className="globalmodal-title">{globalModalState.data.title}</h2>
      {
        globalModalState.data.text ?
          <p className="globalmodal-text">{globalModalState.data.text}</p>
          :
          null
      }
    </div>
    <div className="globalmodal-buttons-container">
      {getButtons()}
    </div>
  </div>
}

export default GlobalModal