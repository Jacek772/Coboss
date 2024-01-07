import IRowData from "../../../components/Grid/types/IRowData"

type ProjectsPageGridState = {
  selectedRows: IRowData[]
  query: {
    searchText?: string, 
    orderBy?: string,
    managerId?: number
    dateFrom?: Date
    dateTo?: Date
  }
}

export default ProjectsPageGridState