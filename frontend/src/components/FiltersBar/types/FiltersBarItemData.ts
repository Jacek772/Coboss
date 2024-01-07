// Types
import FiltersBarItemOption from "../../FiltersBarItem/types/FiltersBarItemOption"
import FiltersBarItemType from "../../FiltersBarItem/types/enums/FiltersBarItemType"

type FiltersBarItemData = {
  name: string,
  type: FiltersBarItemType,
  label: string,
  options?: FiltersBarItemOption[]
}
export default FiltersBarItemData