import IColDef from "./IColDefProps"

interface IGridProps {
  colDefs: IColDef[],
  rowsData: any[],
  onRowClick?: (index: number, rowData: any) => void
}

export default IGridProps