// React
import { useState } from "react"

//Types
import DataFormProps from "./types/DataFormProps"
import DataFormRow from "./types/DataFormRow"

// Css
import "./index.css"
import DataFormFieldData from "./types/DataFormFieldData"
import DataFormField from "../DataFormField"

const DataForm = <T,>({ caption, data, rows, onSave = null, onClose = null }:DataFormProps<T>) => {
  const [stateData, setStateData] = useState<T>({ ...data })

  const handleChange = (name: string, value: string) => {
    setStateData({
      ...stateData,
      [name]: value
    })
  }

  return <div className="dataform-container">
    <div className="dataform-x-container">
      <img 
        className="dataform-x"
        src="./gfx/svg/close.svg"
        alt="close"
        onClick={() => onClose?.()}
      />
    </div>
    <div className="dataform-header">
      <button onClick={() => onSave?.({...stateData})} className="button button-secondary">Save</button>
      <h2 className="dataform-header-caption">{caption}</h2>
    </div>
    <div className="dataform-body">
      {
        rows?.map((dataFormRow: DataFormRow, indexRow: number) => {
          return <div className="dataform-row">
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