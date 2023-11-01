import IColDefState from "./IColDefState"
import IRowData from "./IRowData"

interface IGridState {
  colDefs: IColDefState[],
  rowsData: IRowData[]
}

export default IGridState