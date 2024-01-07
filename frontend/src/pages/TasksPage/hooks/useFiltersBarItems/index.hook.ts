import { useCallback, useEffect, useState } from "react"
import FiltersBarItemType from "../../../../components/FiltersBarItem/types/enums/FiltersBarItemType"
import FiltersBarItemData from "../../../../components/FiltersBar/types/FiltersBarItemData"
import ProjectsService from "../../../../services/ProjectsService"
import { useQuery } from "@tanstack/react-query"
import FiltersBarItemOption from "../../../../components/FiltersBarItem/types/FiltersBarItemOption"

const useFiltersBarItems = () => {
  const [state, setState] = useState({
    filtersBarItems: []
  })

  const projectsQuery = useQuery({
    queryKey: ["projectsOptionsTaskssPageFiltersBar"],
    queryFn: async() => {
      return ProjectsService
        .getInstance()
        .getAllAsync()
    },
    staleTime: 60000
  })

  const getData = useCallback(async () => {
    if(!projectsQuery.isSuccess)
    {
      return
    }

    const projectOptions: FiltersBarItemOption[] = projectsQuery.data?.map(x => ({
      text: `${x.number} - ${x.name}`,
      value: x.id.toString()
    }))

    projectOptions.unshift({ text: "", value: null })

    const filtersBarItems: FiltersBarItemData[] = [
      { label: "date", type: FiltersBarItemType.DatePeriod, name: "date" },
      { label: "term", type: FiltersBarItemType.DatePeriod, name: "term" },
      { label: "project", type: FiltersBarItemType.Select, name: "project", options: projectOptions }
    ]

    setState(s => ({
      ...s,
      filtersBarItems: [...filtersBarItems]
    }))
  }, [projectsQuery])

  useEffect(() => {
    getData()
  }, [projectsQuery.isSuccess])

  return [state.filtersBarItems]
}

export default useFiltersBarItems