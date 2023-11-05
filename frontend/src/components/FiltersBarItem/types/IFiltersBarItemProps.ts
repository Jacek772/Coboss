import IFiltersBarItemOption from "./IFiltersBarItemOption"
import FiltersBarItemType from "./enums/FiltersBarItemType"

interface IFiltersBarItemProps {
  name: string
  label: string
  type: FiltersBarItemType
  options?: IFiltersBarItemOption[]
  onChange: (name: string, values: string[]) => void
}

export default IFiltersBarItemProps