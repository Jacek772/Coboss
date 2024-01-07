import IColDef from "./ColDefProps"
import IRowData from "./IRowData"
import SortDirectionEnum from "./enums/SortDirectionEnum"

interface IGridProps {
  colDefs: IColDef[],
  rowsData: any[],
  onRowClick?: (index: number, rowData: any) => void
  onRowDoubleClick?: (index: number, rowData: any) => void
  onScrollEnd?: (lastRow: any) => void
  onSelectionChanged?: (selectedRows: IRowData[]) => void
  onSortChanged?: (field: string, direction: SortDirectionEnum) => void
}

export default IGridProps