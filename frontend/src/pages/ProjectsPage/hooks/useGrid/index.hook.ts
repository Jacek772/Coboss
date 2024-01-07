import { useQuery, useQueryClient } from "@tanstack/react-query"
import ProjectsService from "../../../../services/ProjectsService"
import GetProjectsQuery from "../../../../types/Query/GetProjectsQuery"
import ProjectsPageGridState from "../../types/ProjectsPageGridState"
import { useCallback, useEffect, useState } from "react"
import SortDirectionEnum from "../../../../components/Grid/types/enums/SortDirectionEnum"
import IRowData from "../../../../components/Grid/types/IRowData"


const useGrid = () => {
  const [gridState, setGridState] = useState<ProjectsPageGridState>({
    selectedRows: [],
    query: {}
  })

  const queryClient = useQueryClient()

  const projectsQuery = useQuery({
    queryKey: ["projects", gridState.query],
    queryFn: async () => {
      const query: GetProjectsQuery = {
        searchText: gridState.query.searchText,
        orderBy: gridState.query.orderBy,
        managerId: gridState.query.managerId,
        termFrom: gridState.query.dateFrom,
        termTo: gridState.query.dateTo
      }

      return await ProjectsService
        .getInstance()
        .getAllAsync(query)
    },
    staleTime: 60000
  })

  useEffect(() => {
    queryClient.invalidateQueries({ queryKey: ["projects"] })
  }, [queryClient, gridState.query])

  const handleSortChanged = useCallback((field: string, direction: SortDirectionEnum) => {
    setGridState(s => ({
      ...s,
      query: {
        ...s.query,
        orderBy: `${field}:${direction}`
      }
    }))
  }, [setGridState])

  const handleSelectionChanged = useCallback((rowsData: IRowData[]) => {
    setGridState(s => ({...s, selectedRows: rowsData}))
  }, [setGridState])

  return {
    data: projectsQuery.data,
    gridState,
    setGridState,
    handleSortChanged,
    handleSelectionChanged
  }
}

export default useGrid