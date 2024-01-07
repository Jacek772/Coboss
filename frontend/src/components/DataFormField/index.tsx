// React
import { useCallback, useEffect, useState } from "react"

//Types
import DataFormFieldProps from "./types/DataFormFieldProps"
import DataFormFieldType from "./types/enums/DataFormFieldType"
import DataFormFieldState from "./types/DataFormFieldState"

// Utils
import DateUtils from "../../utils/DateUtils"

// Css
import "./index.css"
import ValidationUtils from "../../utils/ValidationUtils"
import ValidationResult from "../../utils/types/ValidationResult"

const DataFormField: React.FC<DataFormFieldProps> = ({ 
  name, type, value, label, options, height, isReadonly = false, labelWidth = 100, width = 200, errorMessage,
  onChange, onError, validationSchema
}) => {
  const [state, setState] = useState<DataFormFieldState>({ 
    value: value,
  })

  useEffect(() => {
    if(state.value !== value)
    {
      onChange(name, state.value)
    }
  }, [state.value, onChange, name])

  useEffect(() => {
    setState(s => ({
      ...s,
      value
    }))
  }, [value])

  const handleChange = useCallback((value: string) => {
    setState({
      ...state,
      value
    })
  }, [state])

  const handleBlur = useCallback(() => {
    const validationResult: ValidationResult = ValidationUtils.validateValue(validationSchema, state.value)
    if(!validationResult.success)
    {
      onError?.(name, validationResult.message)
    }
  },[onError, validationSchema, name, state.value])

  const getInputsByType = useCallback(() => {
    let style: React.CSSProperties = {}

    if(height) {
      style = { ...style, height }
    }

    if(width) {
      style = { ...style, width }
    }

    if(errorMessage)
    {
      style = { ...style, borderColor: "red" }
    }

    switch(type){
      case DataFormFieldType.Date:
        return <input
          className={`input dataformfield-input ${isReadonly ? "dataformfield-input-readonly" : ""}`}
          type="date"
          style={style}
          readOnly={isReadonly}
          value={DateUtils.parseStringToInputDateFormat(state.value)}
          onBlur={handleBlur}
          onChange={(e) => handleChange(e.target.value)}/>
      case DataFormFieldType.Number:
        return <input 
          className={`input dataformfield-input ${isReadonly ? "dataformfield-input-readonly" : ""}`}
          type="number"
          style={style}
          readOnly={isReadonly}
          value={state.value ?? ""}
          onBlur={handleBlur}
          onChange={(e) => handleChange(e.target.value)} />
      case DataFormFieldType.Select:
        return <select className="input dataformfield-input"
          style={style}
          disabled={isReadonly}
          value={state.value ?? ""}
          onBlur={handleBlur}
          onChange={(e) => handleChange(e.target.value)}>
          {
            options?.map((x, index) => {
              return <option key={index} value={x.value}>{x.text}</option>
            })
          }
        </select>
      case DataFormFieldType.MultilineString:
        return <textarea 
          className={`input dataformfield-input dataformfield-input-textarea ${isReadonly ? "dataformfield-input-readonly" : ""}`}
          style={style}
          readOnly={isReadonly}
          value={state.value ?? ""}
          onBlur={handleBlur}
          onChange={(e) => handleChange(e.target.value)}
        ></textarea>
      case DataFormFieldType.String:
      default:
        return <input 
          className={`input dataformfield-input ${isReadonly ? "dataformfield-input-readonly" : ""}`}
          style={style}
          type="text"
          readOnly={isReadonly}
          value={state.value ?? ""}
          onBlur={handleBlur}
          onChange={(e) => handleChange(e.target.value)} />
    }
  }, [handleChange, handleBlur, type, state, options, height, width, isReadonly, errorMessage])

  return <div className="dataformfield-container">
    <div className="dataformfield-input-container">
      <label style={{ width: labelWidth }} className="dataformfield-label">{label}</label>
      {getInputsByType()}
    </div>
 
    <p style={{ marginLeft: labelWidth + 10 }} className="dataformfield-error">{errorMessage}</p>
  </div>
}

export default DataFormField