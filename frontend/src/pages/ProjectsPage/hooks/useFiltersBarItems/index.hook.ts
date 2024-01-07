import { useCallback, useEffect, useState } from "react"
import FiltersBarItemData from "../../../../components/FiltersBar/types/FiltersBarItemData"
import FiltersBarItemType from "../../../../components/FiltersBarItem/types/enums/FiltersBarItemType"
import { useQuery } from "@tanstack/react-query"
import EmployeesService from "../../../../services/EmployeesService"
import FiltersBarItemOption from "../../../../components/FiltersBarItem/types/FiltersBarItemOption"

const useFiltersBarItems = () => {
  const [state, setState] = useState({
    filtersBarItems: []
  })

  const employessQuery = useQuery({
    queryKey: ["employessOptionsProjectsPageFiltersBar"],
    queryFn: async() => {
      return EmployeesService
        .getInstance()
        .getAllAsync()
    },
    staleTime: 60000
  })

  const getData = useCallback(async () => {
    if(!employessQuery.isSuccess)
    {
      return
    }

    const managerOptions: FiltersBarItemOption[] = employessQuery.data?.map(x => ({
      text: `${x.name} ${x.surname}`,
      value: x.id.toString()
    }))

    managerOptions.unshift({ text: "", value: null })

    const filtersBarItems: FiltersBarItemData[] = [
      { label: "term", type: FiltersBarItemType.DatePeriod, name: "term" },
      { label: "manager", type: FiltersBarItemType.Select, name: "manager", options: managerOptions  }
    ]

    setState(s => ({
      ...s,
      filtersBarItems: [...filtersBarItems]
    }))
  }, [employessQuery])

  useEffect(() => {
    getData()
  }, [employessQuery.isSuccess])

  return [state.filtersBarItems]
}

export default useFiltersBarItems