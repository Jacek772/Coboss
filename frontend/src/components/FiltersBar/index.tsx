// React
import { useEffect, useState } from "react"

// Components
import FiltersBarItem from "../FiltersBarItem"

// Types
import IFiltersBarProps from "./types/IFiltersBarProps"
import IFiltersBarState from "./types/IFiltersBarState"
import IFiltersBarValue from "./types/IFiltersBarValue"

//Css
import "./index.css"

const FiltersBar: React.FC<IFiltersBarProps> = ({ items, onChange }:IFiltersBarProps) => {
  const [state, setState] = useState<IFiltersBarState>({ values: [] })

  const handleChange = (name: string, values: string[]) => {
    const valueIndex: number = state.values.findIndex(x => x.name === name)
    if(valueIndex >= 0) {
      const updatedValues: IFiltersBarValue[] = [...state.values]
      updatedValues[valueIndex].values = values
      setState({
        ...state,
        values: updatedValues
      })
    }
    else
    {
      setState({
        ...state,
        values: [...state.values, { name, values }]
      })
    }
  }

  useEffect(() => {
    if(onChange)
    {
      onChange(state.values)
    }
  }, [state.values])

  return <div className="filtersbar-container">
    {
      items.map((x, index) => {
        return <FiltersBarItem
          name={x.name}
          key={index}
          label={x.label} 
          type={x.type}
          options={x.options}
          onChange={handleChange}  />
      })
    }
  </div>
}

export default FiltersBar