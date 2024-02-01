// Libraries
import { z } from "zod"
import { useQuery } from "@tanstack/react-query"
import { useCallback, useEffect, useState } from "react"

// Services
import ProjectsService from "../../../../services/ProjectsService"

import DataFormFieldItemOption from "../../../../components/DataFormField/types/DataFormFieldItemOption"
import DataFormRow from "../../../../components/DataForm/types/DataFormRow"
import DataFormFieldType from "../../../../components/DataFormField/types/enums/DataFormFieldType"
import DataFormRowTypeEnum from "../../../../components/DataForm/types/DataFormRowTypeEnum"
import CommentsGrid from "../../../../components/CommentsGrid"
import TaskRealisationsGrid from "../../../../components/TaskRealisationsGrid"
import EmployeesGrid from "../../../../components/EmployeesGrid"


const useFormRows = () => {
  const [state, setState] = useState({
    formRows: []
  })

  const projectsQuery = useQuery({
    queryKey: ["projectsOptionsTasksPageForm"],
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

    const projectOptions: DataFormFieldItemOption[] = projectsQuery.data?.map(x => ({
      text: `${x.number} - ${x.name}`,
      value: x.id.toString()
    }))

    projectOptions.unshift({ text: "", value: "" })

    const formRows: DataFormRow[] = [
      {
        type: DataFormRowTypeEnum.Fields,
        items: [
          { label:"Name", name: "name", type: DataFormFieldType.String, validationSchema: z.string().min(1, { message: "Field is required" }) },
          { label:"Date", name: "date", type: DataFormFieldType.Date, validationSchema: z
              .string()
              .min(1, "Field is required")
              .transform(x => new Date(x)) 
          },
          { label:"Term", name: "term", type: DataFormFieldType.Date, validationSchema: z
              .string()
              .min(1, "Field is required")
              .transform(x => new Date(x)) 
          },
        ]
      },
      {
        type: DataFormRowTypeEnum.Fields,
        items: [
          { label: "Project", name:"projectId", type: DataFormFieldType.Select, options: projectOptions }
        ]
      },
      {
        type: DataFormRowTypeEnum.Fields,
        items: [
          { label:"Description", name: "description", type: DataFormFieldType.MultilineString, height: 200, width: 600, validationSchema: z.string().min(1, { message: "Field is required" })  },
        ]
      },
      {
        type: DataFormRowTypeEnum.Components,
        caption: "Comments",
        dataField: "comments",
        components: [
          CommentsGrid
        ]
      },
      {
        type: DataFormRowTypeEnum.Components,
        caption: "Task realisations",
        dataField: "taskRealisations",
        components: [
          TaskRealisationsGrid
        ]
      },
      {
        type: DataFormRowTypeEnum.Components,
        caption: "Employees",
        dataField: "employees",
        components: [
          EmployeesGrid
        ]
      }
    ]

    setState(s => ({
      ...s,
      formRows: [...formRows]

    }))
  }, [projectsQuery])

  useEffect(() => {
    getData()
  }, [projectsQuery.isSuccess])

  return [state.formRows]
}


export default useFormRows