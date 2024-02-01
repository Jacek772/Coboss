// Libraries
import { z } from "zod"
import { useQuery } from "@tanstack/react-query"
import { useCallback, useEffect, useState } from "react"

import DataFormFieldItemOption from "../../../../components/DataFormField/types/DataFormFieldItemOption"
import DataFormRow from "../../../../components/DataForm/types/DataFormRow"
import DataFormFieldType from "../../../../components/DataFormField/types/enums/DataFormFieldType"
import DataFormRowTypeEnum from "../../../../components/DataForm/types/DataFormRowTypeEnum"
import EmployeesService from "../../../../services/EmployeesService"

const useFormRows = () => {
  const [state, setState] = useState({
    formRows: []
  })

  const employessQuery = useQuery({
    queryKey: ["employessOptionsEmployeessGrid"],
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

    const employeeOptions: DataFormFieldItemOption[] = employessQuery.data?.map(x => ({
      text: `${x.name} ${x.surname}`,
      value: x.id.toString()
    }))

    const formRows: DataFormRow[] = [
      {
        type: DataFormRowTypeEnum.Fields,
        items: [
          { label:"Employee", name:"employeeId", type: DataFormFieldType.Select, options: employeeOptions },
        ]
      },
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