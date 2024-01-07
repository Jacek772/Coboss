// Libraries
import { z } from "zod"

import { useCallback, useEffect, useState } from "react"
import DataFormRow from "../../../../components/DataForm/types/DataFormRow"
import DataFormFieldType from "../../../../components/DataFormField/types/enums/DataFormFieldType"

const useFormRows = () => {
  const [state, setState] = useState({
    formRows: []
  })

  const getData = useCallback(async () => {
    const formRows: DataFormRow[] = [
      {
        caption:"User",
        items: [
          { label:"Email", isReadonly: true, name: "user.email", type: DataFormFieldType.String }
        ]
      },
      {
        caption:"Employee",
        items: [
          { label:"Code", isReadonly: true, name: "code", type: DataFormFieldType.String },
          { label:"Name", name: "name", type: DataFormFieldType.String, validationSchema: z.string().min(1, {message: "Field is required" }) },
          { label:"Surname", name: "surname", type: DataFormFieldType.String, validationSchema: z.string().min(1, {message: "Field is required" }) },
        ]
      },
      {
        items: [
          { 
            label:"Date of birth", name: "dateOfBirth", type: DataFormFieldType.Date, validationSchema: z
            .string()
            .min(1, "Field is required")
            .transform(x => new Date(x))
          },
          { label:"PESEL", name: "pesel", type: DataFormFieldType.String },
          { label:"NIP", name: "nip", type: DataFormFieldType.String }
        ]
      }
    ]

    setState(s => ({
      ...s,
      formRows
    }))
  }, [])

  useEffect(() => {
    getData()
  }, [])

  return [state.formRows]
}

export default useFormRows