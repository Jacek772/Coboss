import { useCallback, useState } from "react"
import ActionTypeEnum from "../../../../types/ActionTypeEnum"
import { useMutation, useQueryClient } from "@tanstack/react-query"
import useGlobalModal from "../../../../hooks/useGlobalModal/index.hook"
import CreateBusinnessTaskCommand from "../../../../types/Commands/CreateBusinnessTaskCommand"
import UpdateBusinnessTaskCommand from "../../../../types/Commands/UpdateBusinnessTaskCommand"
import BusinnessTasksService from "../../../../services/BusinnessTasksService"
import GlobalModalTypeEnum from "../../../../components/GlobalModal/types/GlobalModalTypeEnum"
import GlobalModalButtonsTypeEnum from "../../../../components/GlobalModal/types/GlobalModalButtonsTypeEnum"
import GlobalModalClickResultEnum from "../../../../components/GlobalModal/types/GlobalModalClickResultEnum"
import useFormRows from "../useFormRows/index.hook"

const useDataForm = () => {
  const [dataFormState, setDataFormState] = useState({
    visible: false,
    action: ActionTypeEnum.NONE,
    employeeData: {
      id: "",
      employeeId: 0,
      code: "",
      name: "",
      surname: ""
    }
  })

  const [formRows] = useFormRows()

  const handleClose = useCallback(() => {
    setDataFormState(s => ({
      ...s,
      visible: false
    }))
  }, [])

  return {
    formRows,
    dataFormState,
    setDataFormState,
    handleClose
  }
}

export default useDataForm