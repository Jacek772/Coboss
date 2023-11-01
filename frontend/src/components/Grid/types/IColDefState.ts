import SortDirection from "./enums/SortDirection"

interface IColDefState {
  caption: string,
  field: string,
  width: number,
  checked: boolean,
  sortDirection: SortDirection
}

export default IColDefState