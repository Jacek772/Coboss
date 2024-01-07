// React
import { useEffect, useState } from "react"

// Components
import FiltersBarItem from "../FiltersBarItem"

// Types
import FiltersBarProps from "./types/FiltersBarProps"
import FiltersBarState from "./types/FiltersBarState"
import FiltersBarValue from "./types/FiltersBarValue"

//Css
import "./index.css"

const FiltersBar: React.FC<FiltersBarProps> = ({ items, onChange }) => {
  const [state, setState] = useState<FiltersBarState>({ values: [] })

  const handleChange = (name: string, values: string[]) => {
    const valueIndex: number = state.values.findIndex(x => x.name === name)
    if(valueIndex >= 0) {
      const updatedValues: FiltersBarValue[] = [...state.values]
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
    onChange?.([...state.values])
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