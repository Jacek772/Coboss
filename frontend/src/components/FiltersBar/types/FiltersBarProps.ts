import FiltersBarItemData from "./FiltersBarItemData"
import FiltersBarValue from "./FiltersBarValue"

type FiltersBarProps = {
 items: FiltersBarItemData[]
 onChange?: (values:FiltersBarValue[]) => void
}

export default FiltersBarProps