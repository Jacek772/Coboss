// React
import { useCallback, useEffect, useState } from "react"

// Types
import IFiltersBarItemProps from "./types/IFiltersBarItemProps"

//Css
import "./index.css"
import FiltersBarItemType from "./types/enums/FiltersBarItemType"
import IFiltersBarItemState from "./types/IFiltersBarItemState"

const FiltersBarItem: React.FC<IFiltersBarItemProps> = ({ name, label, type, onChange, options = [] }: IFiltersBarItemProps) => {

  const [state, setState] = useState<IFiltersBarItemState>({ value: "", valueFrom: "", valueTo: "" })

  useEffect(() => {
    if(type === FiltersBarItemType.DatePeriod) {
      if(state.valueFrom && state.valueTo)
      {
        const dateFrom = new Date(state.valueFrom)
        const dateTo = new Date(state.valueTo)

        onChange(name, [dateFrom.toJSON(), dateTo.toJSON()])
      }
    }
    else
    {
      onChange(name, [state.value])
    }
  }, [state])

  const handleChange = (e) => {
    if(type === FiltersBarItemType.DatePeriod) {
      setState({
        ...state,
        [e.target.name]: e.target.value
      })
    }
    else
    {
      setState({
        ...state,
        value: e.target.value
      })
    }
  }

  const getInputsByType = useCallback(() => {
    switch(type)
    {
      case FiltersBarItemType.Number:
        return <input 
          className="input filtersbaritem-input" 
          type="text"
          value={state.value}
          onChange={handleChange} />
      case FiltersBarItemType.Date:
        return <input 
          className="input filtersbaritem-input" 
          type="date" 
          value={state.value}
          onChange={handleChange}/>
      case FiltersBarItemType.DatePeriod:
        return <>
          <input
            name="valueFrom"
            className="input filtersbaritem-input" 
            type="date"
            value={state.valueFrom}
            onChange={handleChange} />
          <input
            name="valueTo"
            className="input filtersbaritem-input" 
            type="date"
            value={state.valueTo}
            onChange={handleChange} />
        </>
      case FiltersBarItemType.Select:
        return <select className="input filtersbaritem-input" onChange={handleChange} value={state.value}>
          {
            options.map((x, index) => {
              return <option key={index} value={x.value}>{x.text}</option>
            })
          }
        </select>
      case FiltersBarItemType.String:
      default:
        return <input 
          className="input filtersbaritem-input" 
          type="text"
          value={state.value}
          onChange={handleChange} />
    }
  }, [type, options, state.value])

  return <div className="filtersbaritem-container">
      <label className="filtersbaritem-label">{label}</label>
      {getInputsByType()}
    </div>
}

export default FiltersBarItem