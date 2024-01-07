import SortDirectionEnum from "./enums/SortDirectionEnum"
import GridColTypeEnum from "./enums/GridColTypeEnum"

type ColDefState = {
  caption: string,
  field: string,
  width: number,
  type?: GridColTypeEnum,
  checked: boolean,
  sortDirection: SortDirectionEnum
}

export default ColDefState