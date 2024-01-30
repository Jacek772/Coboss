import { useQuery, useQueryClient } from "@tanstack/react-query"
import GetBusinnessTasksQuery from "../../../../types/Query/GetBusinnessTasksQuery"
import { useCallback, useEffect, useState } from "react"
import BusinnessTasksService from "../../../../services/BusinnessTasksService"
import SortDirectionEnum from "../../../../components/Grid/types/enums/SortDirectionEnum"
import IRowData from "../../../../components/Grid/types/IRowData"

const useGrid = () => {
  const [gridState, setGridState] = useState({
    selectedRows: [],
    query: {}
  })

  const queryClient = useQueryClient()

  const projectsQuery = useQuery({
    queryKey: ["businnessTasks", gridState.query],
    queryFn: async () => {
      const query: GetBusinnessTasksQuery = {
        // searchText: gridState.query.searchText,
        // orderBy: gridState.query.orderBy,
        // dateFrom: gridState.query.dateFrom,
        // dateTo: gridState.query.dateTo,
        // termFrom: gridState.query.termFrom,
        // termTo: gridState.query.termTo,
        // projectId: gridState.query.projectId
      }

      return await BusinnessTasksService
        .getInstance()
        .getAllAsync(query)
    },
    staleTime: 60000
  })

  useEffect(() => {
    queryClient.invalidateQueries({ queryKey: ["businnessTasks"] })
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