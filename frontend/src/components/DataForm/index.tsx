// React
import { Dispatch, useCallback, useEffect, useState } from "react"
import { v4 as uuidv4 } from 'uuid';

//Types
import DataFormProps from "./types/DataFormProps"
import DataFormRow from "./types/DataFormRow"

// Css
import "./index.css"
import DataFormFieldData from "./types/DataFormFieldData"
import DataFormField from "../DataFormField"
import { useDispatch, useSelector } from "react-redux"
import { AnyAction } from "@reduxjs/toolkit"

// Actions
import { GlobalModalState, setGlobalModalClickResult, setGlobalModalData, setGlobalModalVisibility } from "../../redux/slices/globalModalSlice"
import GlobalModalClickResultEnum from "../GlobalModal/types/GlobalModalClickResultEnum"
import { RootState } from "../../redux/store"
import GlobalModalButtonsTypeEnum from "../GlobalModal/types/GlobalModalButtonsTypeEnum"
import GlobalModalTypeEnum from "../GlobalModal/types/GlobalModalTypeEnum"
import DataFormState from "./types/DataFormState"

const DataForm = <T,>({ caption, data, rows, onSave = null, onClose = null }:DataFormProps<T>) => {
  const dispatch: Dispatch<AnyAction> = useDispatch()

  const globalModalState = useSelector<RootState, GlobalModalState>(x => x.globalModal)

  const [stateData, setStateData] = useState<T>({ ...data })
  const [state, setState] = useState<DataFormState>({
    key:""
  })

  useEffect(() => {
    if(state.key === globalModalState.data.key)
    {
      const action: string = state.key.split(";")[0]

      if(action === "SAVE")
      {
        if(globalModalState.result.clickResult === GlobalModalClickResultEnum.Yes)
        {
          dispatch(setGlobalModalVisibility(false))
          onSave?.({...stateData})
          console.log("SAVE")
        }
        else
        {
          dispatch(setGlobalModalVisibility(false))
        }
      }

      if(action === "DISCARD")
      {
        if(globalModalState.result.clickResult === GlobalModalClickResultEnum.Yes)
        {
          dispatch(setGlobalModalVisibility(false))
          onClose?.()
          console.log("DISCARD")
        }
        else
        {
          dispatch(setGlobalModalVisibility(false))
        }
      }
    }
  }, [globalModalState.result])

  const handleChange = (name: string, value: string) => {
    setStateData({
      ...stateData,
      [name]: value
    })
  }

  const dataChanged = useCallback<() => boolean>(() => {
    return JSON.stringify(stateData) !== JSON.stringify(data)
  }, [stateData, data])

  const showGlobalModal = useCallback((title: string, text: string, action: string = "action") => {
    const key: string = `${action};${uuidv4()}`
    setState({
      ...state,
      key
    })

    dispatch(setGlobalModalData({
      key,
      title,
      text,
      buttonsType: GlobalModalButtonsTypeEnum.YesNo,
      modalType: GlobalModalTypeEnum.Info
    }))
    dispatch(setGlobalModalVisibility(true))
  }, [dispatch, state])

  const handleClickSave = useCallback<React.MouseEventHandler<HTMLButtonElement>>(() => {
    if(dataChanged())
    {
      const title: string = "Save changes"
      const text: string = "Do you want save inserted changes?"
      showGlobalModal(title, text, "SAVE")
    }
    else
    {
      onClose?.()
    }
  }, [onClose, showGlobalModal, dataChanged])

  const handleClickClose = useCallback(() => {
    if(dataChanged())
    {
      const title: string = "Discard changes"
      const text: string = "Do you want discard inserted changes?"
      showGlobalModal(title, text, "DISCARD")
    }
    else
    {
      onClose?.()
    }
  }, [dataChanged, onClose, showGlobalModal])

  return <div className="dataform-container">
    <div className="dataform-x-container">
      <img 
        className="dataform-x"
        src="./gfx/svg/close.svg"
        alt="close"
        onClick={handleClickClose}
      />
    </div>
    <div className="dataform-header">
      <button onClick={handleClickSave} className="button button-secondary">Save</button>
      <h2 className="dataform-header-caption">{caption}</h2>
    </div>
    <div className="dataform-body">
      {
        rows?.map((dataFormRow: DataFormRow, indexRow: number) => {
          return <div key={indexRow} className="dataform-row">
            {
              dataFormRow.caption ?
                <h2 className="dataform-row-caption">{dataFormRow.caption}</h2>
                :
                null
            }
            <div key={indexRow} className="dataform-row-fields">
              {
                dataFormRow.items.map((item: DataFormFieldData, indexItem: number) => {
                  return <DataFormField
                    key={indexItem}
                    label={item.label}
                    name={item.name}
                    type={item.type}
                    value={stateData[item.name]}
                    height={item.height}
                    width={item.width}
                    options={item.options}
                    onChange={handleChange}
                  />
                })
              }
            </div>
          </div>
        })
      }
    </div>
  </div>
}

export default DataForm