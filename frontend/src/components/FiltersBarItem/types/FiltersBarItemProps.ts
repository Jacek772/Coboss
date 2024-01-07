import FiltersBarItemOption from "./FiltersBarItemOption"
import FiltersBarItemType from "./enums/FiltersBarItemType"

type FiltersBarItemProps = {
  name: string
  label: string
  type: FiltersBarItemType
  options?: FiltersBarItemOption[]
  onChange: (name: string, values: string[]) => void
}

export default FiltersBarItemProps