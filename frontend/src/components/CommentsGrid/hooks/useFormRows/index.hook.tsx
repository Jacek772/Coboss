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

    const formRows: DataFormRow[] = [
      {
        type: DataFormRowTypeEnum.Fields,
        items: [
          { label:"User", name:"username", type: DataFormFieldType.String, isReadonly: true },
          { label:"Date", name: "date", type: DataFormFieldType.Date, isReadonly: true }
        ]
      },
      {
        type: DataFormRowTypeEnum.Fields,
        items: [
          { label:"Text", name: "text", type: DataFormFieldType.MultilineString, height: 200, width: 600, validationSchema: z.string().min(1, { message: "Field is required" })  },
        ]
      },
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