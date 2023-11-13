// React
import { useCallback, useEffect, useState } from "react"

//Types
import DataFormFieldProps from "./types/DataFormFieldProps"
import DataFormFieldType from "./types/enums/DataFormFieldType"
import DataFormFieldState from "./types/DataFormFieldState"

// Css
import "./index.css"

const DataFormField: React.FC<DataFormFieldProps> = ({ name, type, value, label, options, onChange, height, labelWidth = 100, width = 200 }) => {
  const [state, setState] = useState<DataFormFieldState>({ value })

  useEffect(() => {
    onChange(name, state.value)
  }, [state])

  const handleChange = useCallback((value: string) => {
    setState({
      ...state,
      value
    })
  }, [state])

  const getInputsByType = useCallback(() => {
    let style: React.CSSProperties = {}

    if(height) {
      style = { ...style, height }
    }

    if(width) {
      style = { ...style, width }
    }

    switch(type){
      case DataFormFieldType.Date:
        return <input
          className="input dataformfield-input"
          type="date"
          style={style}
          value={state.value}
          onChange={(e) => handleChange(e.target.value)}/>
      case DataFormFieldType.Number:
        return <input 
          className="input dataformfield-input"
          type="number"
          style={style}
          value={state.value}
          onChange={(e) => handleChange(e.target.value)} />
      case DataFormFieldType.Select:
        return <select className="input dataformfield-input"
          onChange={(e) => handleChange(e.target.value)}
          style={style}
          value={state.value}>
          {
            options?.map((x, index) => {
              return <option key={index} value={x.value}>{x.text}</option>
            })
          }
        </select>
      case DataFormFieldType.MultilineString:
        return <textarea 
          className="input dataformfield-input"
          style={style}
          value={state.value}
          onChange={(e) => handleChange(e.target.value)}
        ></textarea>
      case DataFormFieldType.String:
      default:
        return <input 
          className="input dataformfield-input"
          style={style}
          type="text"
          value={state.value}
          onChange={(e) => handleChange(e.target.value)} />
    }
  }, [handleChange, type, state, options])

  return <div className="dataformfield-container">
    <label style={{ width: labelWidth }} className="dataformfield-label">{label}</label>
    {getInputsByType()}
  </div>
}

export default DataFormField