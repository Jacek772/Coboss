import IColDef from "./IColDefProps"
import IRowData from "./IRowData"
import SortDirection from "./enums/SortDirection"

interface IGridProps {
  colDefs: IColDef[],
  rowsData: any[],
  onRowClick?: (index: number, rowData: any) => void
  onRowDoubleClick?: (index: number, rowData: any) => void
  onScrollEnd?: (lastRow: any) => void
  onSelectionChanged?: (selectedRows: IRowData[]) => void
  onSortChanged?: (field: string, direction: SortDirection) => void
}

export default IGridProps