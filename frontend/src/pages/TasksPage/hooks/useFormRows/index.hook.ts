// Libraries
import { z } from "zod"
import { useQuery } from "@tanstack/react-query"
import { useCallback, useEffect, useState } from "react"

// Services
import ProjectsService from "../../../../services/ProjectsService"

import DataFormFieldItemOption from "../../../../components/DataFormField/types/DataFormFieldItemOption"
import DataFormRow from "../../../../components/DataForm/types/DataFormRow"
import DataFormFieldType from "../../../../components/DataFormField/types/enums/DataFormFieldType"

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
        items: [
          { label: "Project", name:"project.id", type: DataFormFieldType.Select, options: projectOptions }
        ]
      },
      {
        items: [
          { label:"Description", name: "description", type: DataFormFieldType.MultilineString, height: 200, width: 600, validationSchema: z.string().min(1, { message: "Field is required" })  },
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