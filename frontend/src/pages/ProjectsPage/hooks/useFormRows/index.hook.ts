// Libraries
import { z } from "zod"
import { useCallback, useEffect, useState } from "react"
import { useQuery } from "@tanstack/react-query"

// Services
import EmployeesService from "../../../../services/EmployeesService"

// Types
import DataFormRow from "../../../../components/DataForm/types/DataFormRow"
import DataFormFieldType from "../../../../components/DataFormField/types/enums/DataFormFieldType"
import DataFormFieldItemOption from "../../../../components/DataFormField/types/DataFormFieldItemOption"

const useFormRows = () => {
  const [state, setState] = useState({
    formRows: []
  })

  const employessQuery = useQuery({
    queryKey: ["employessOptionsProjectsPageForm"],
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

    const managerOptions: DataFormFieldItemOption[] = employessQuery.data?.map(x => ({
      text: `${x.name} ${x.surname}`,
      value: x.id.toString()
    }))

    const formRows: DataFormRow[] = [
      {
        items: [
          { label:"Number", isReadonly: true, name: "number", type: DataFormFieldType.String },
          { label:"Name", name: "name", type: DataFormFieldType.String, validationSchema: z.string().min(1, { message: "Field is required" }) },
        ]
      },
      {
        items: [
          { 
            label:"Term", name: "term", type: DataFormFieldType.Date, validationSchema: z
              .string()
              .min(1, "Field is required")
              .transform(x => new Date(x))
          },
          {
            label: "Manager", name:"manager.id", type: DataFormFieldType.Select, options: managerOptions
          }
        ]
      },
      {
        items: [
          { label:"Description", name: "description", type: DataFormFieldType.MultilineString, height: 200, width: 600 },
        ]
      }
    ]

    setState(s => ({
      ...s,
      formRows: [...formRows]

    }))
  }, [employessQuery])

  useEffect(() => {
    getData()
  }, [employessQuery.isSuccess])

  return [state.formRows]
}

export default useFormRows