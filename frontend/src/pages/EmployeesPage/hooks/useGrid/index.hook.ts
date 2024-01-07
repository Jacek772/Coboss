import { useQuery, useQueryClient } from "@tanstack/react-query"
import { useCallback, useEffect, useState } from "react"
import GetEmployeesQuery from "../../../../types/Query/GetEmployeesQuery"
import EmployeesService from "../../../../services/EmployeesService"
import SortDirectionEnum from "../../../../components/Grid/types/enums/SortDirectionEnum"
import IRowData from "../../../../components/Grid/types/IRowData"

const useGrid = () => {
  const [gridState, setGridState] = useState({
    selectedRows: [],
    query: {
      searchText: "",
      orderBy: ""
    }
  })

  const queryClient = useQueryClient()

  const employessQuery = useQuery({
    queryKey: ["employess", gridState.query],
    queryFn: async () => {
      const query: GetEmployeesQuery = {
        searchText: gridState.query.searchText,
        orderBy: gridState.query.orderBy
      };
  
      return await EmployeesService
        .getInstance()
        .getAllAsync(query)
    },
    staleTime: 60000
  })

  useEffect(() => {
    queryClient.invalidateQueries({ queryKey: ["employess"] })
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
    data: employessQuery.data,
    gridState,
    setGridState,
    handleSortChanged,
    handleSelectionChanged
  }
}

export default useGrid