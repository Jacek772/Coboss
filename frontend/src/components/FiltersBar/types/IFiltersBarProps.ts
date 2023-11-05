import IFiltersBarItemData from "./IFiltersBarItemData"
import IFiltersBarValue from "./IFiltersBarValue"

interface IFiltersBarProps {
 items: IFiltersBarItemData[]
 onChange?: (values:IFiltersBarValue[]) => void
}

export default IFiltersBarProps