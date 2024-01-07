import ColDefState from "./ColDefState"
import IRowData from "./IRowData"

interface IGridState {
  colDefs: ColDefState[],
  rowsData: IRowData[]
}

export default IGridState