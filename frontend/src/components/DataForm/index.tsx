// React
import { useCallback, useEffect, useState } from "react"

// Hooks
import useGlobalModal from "../../hooks/useGlobalModal/index.hook";

// Utils
import ObjectUtils from "../../utils/ObjectUtils"
import ValidationUtils from "../../utils/ValidationUtils"

//Types
import DataFormProps from "./types/DataFormProps"
import DataFormRow from "./types/DataFormRow"
import DataFormFieldData from "./types/DataFormFieldData"
import GlobalModalClickResultEnum from "../GlobalModal/types/GlobalModalClickResultEnum"
import GlobalModalButtonsTypeEnum from "../GlobalModal/types/GlobalModalButtonsTypeEnum"
import GlobalModalTypeEnum from "../GlobalModal/types/GlobalModalTypeEnum"
import ValidationResult from "../../utils/types/ValidationResult"
import DataFormFieldDataState from "./types/DataFormFieldDataState"

// Components
import DataFormField from "../DataFormField"

// Css
import "./index.css"

const DataForm = <T,>({ caption, data, rows, onSave, onClose }: DataFormProps<T>) => {
  const [dataFormFieldsDataState, setDataFormFieldsDataState] = useState<DataFormFieldDataState[]>([])
  const [stateData, setStateData] = useState<T>(() => JSON.parse(JSON.stringify(data)))

  const [showGlobalModal, hideGlobalModal] = useGlobalModal()

  useEffect(() => {
    const dataFormFieldsData: DataFormFieldDataState[] = rows.reduce((acc, rowData) => {
      const fieldsData = rowData.items?.map(fieldData => {
        return { 
          name: fieldData.name, 
          type: fieldData.type, 
          value: ObjectUtils.getValueByPath(stateData, fieldData.name), 
          validationSchema: fieldData.validationSchema 
        }
      })
      return [...acc, ...fieldsData]
    }, [])

    setDataFormFieldsDataState([...dataFormFieldsData])
  },[rows, stateData])

  const handleChange = (name: string, value: string) => {
    const stateDataCopy: T = ObjectUtils.setValueByPath<T, string>(stateData, name, value)
    setStateData({ ...stateDataCopy })
  }

  const isDataChanged = useCallback((): boolean => {
    return ObjectUtils.objectsDifferent(stateData, data)
  }, [stateData, data])

  const validate = useCallback(() => {
    const dataFormFieldsData: DataFormFieldDataState[] = dataFormFieldsDataState.map(x => {
      if(!x.validationSchema)
      {
        return { ...x }
      }

      const validationResult: ValidationResult = ValidationUtils.validateValue(x.validationSchema, x.value)
      return { ...x, errorMessage: validationResult.success ? "" : validationResult.message }
    })

    setDataFormFieldsDataState([...dataFormFieldsData])
    return dataFormFieldsData.every(x => !x.errorMessage)
  },[dataFormFieldsDataState])

  const handleClickSave = useCallback<React.MouseEventHandler<HTMLButtonElement>>(() => {
    if(!isDataChanged() && !ObjectUtils.objectFieldsEmpty(stateData))
    {
      onClose?.()
      return
    }

    if(validate())
    {
      showGlobalModal({
        title: "Save changes",
        text: "Do you want save inserted changes?",
        modalType: GlobalModalTypeEnum.Warning,
        buttonsType: GlobalModalButtonsTypeEnum.YesNo,
        callback: (clickResult: GlobalModalClickResultEnum) => {
          if(clickResult === GlobalModalClickResultEnum.Yes)
          {
            onSave?.({...stateData})
          }
          hideGlobalModal()
        }
      })
    }
  }, [onClose, onSave, showGlobalModal, isDataChanged, hideGlobalModal, validate, stateData])

  const handleClickClose = useCallback(() => {
    if(!isDataChanged())
    {
      onClose?.()
      return
    }

    showGlobalModal({
      title: "Discard changes",
      text: "Do you want discard inserted changes?",
      modalType: GlobalModalTypeEnum.Warning,
      buttonsType: GlobalModalButtonsTypeEnum.YesNo,
      callback: (clickResult: GlobalModalClickResultEnum) => {
        if(clickResult === GlobalModalClickResultEnum.Yes)
        {
          onClose?.()
        }
        hideGlobalModal()
      }
    })
  }, [isDataChanged, onClose, showGlobalModal, hideGlobalModal])

  const handleError = useCallback((name: string, message: string) => {
    const dataFormFieldsData: DataFormFieldDataState[] = [...dataFormFieldsDataState]
    const index: number = dataFormFieldsData.findIndex(x => x.name === name)
    if(index >= 0)
    {
      dataFormFieldsData[index].errorMessage = message
      setDataFormFieldsDataState([...dataFormFieldsData])
    }
  },[dataFormFieldsDataState])

  const getErrorMessage = useCallback((name: string): string => {
    return dataFormFieldsDataState?.find(x => x.name === name)?.errorMessage ?? ""
  },[dataFormFieldsDataState])

  if(!stateData)
  {
    return null
  }

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
      <button onClick={handleClickSave} className="button button-secondary dataform-button-save">Save</button>
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
                dataFormRow.items.map((fieldData: DataFormFieldData, indexItem: number) => {
                  return <DataFormField
                    key={indexItem}
                    {...fieldData}
                    value={ObjectUtils.getValueByPath(stateData, fieldData.name)}
                    errorMessage={getErrorMessage(fieldData.name)}
                    onChange={handleChange}
                    onError={handleError}
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