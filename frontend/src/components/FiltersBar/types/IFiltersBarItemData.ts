// Types
import IFiltersBarItemOption from "../../FiltersBarItem/types/IFiltersBarItemOption"
import FiltersBarItemType from "../../FiltersBarItem/types/enums/FiltersBarItemType"

interface IFiltersBarItemData {
  name: string,
  type: FiltersBarItemType,
  label: string,
  options?: IFiltersBarItemOption[]
}
export default IFiltersBarItemData