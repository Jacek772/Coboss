import IRowData from "../../../components/Grid/types/IRowData"

type TasksPageGridState = {
  selectedRows: IRowData[]
  query: {
    searchText?: string, 
    orderBy?: string,
    dateFrom?: Date
    dateTo?: Date
    termFrom?: Date
    termTo?: Date
    projectId?: number
  }
}

export default TasksPageGridState