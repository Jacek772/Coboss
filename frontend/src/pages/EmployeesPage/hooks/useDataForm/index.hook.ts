import { useCallback, useState } from "react"
import EmployeeDataFormState from "../../types/EmployeeDataFormState"
import ActionTypeEnum from "../../../../types/ActionTypeEnum"
import { useMutation, useQueryClient } from "@tanstack/react-query"
import useGlobalModal from "../../../../hooks/useGlobalModal/index.hook"
import useFormRows from "../useFormRows/index.hook"
import EmployeesService from "../../../../services/EmployeesService"
import CreateEmployeeCommand from "../../../../types/Commands/CreateEmployeeCommand"
import UpdateEmployeeCommand from "../../../../types/Commands/UpdateEmployeeCommand"
import GlobalModalTypeEnum from "../../../../components/GlobalModal/types/GlobalModalTypeEnum"
import GlobalModalButtonsTypeEnum from "../../../../components/GlobalModal/types/GlobalModalButtonsTypeEnum"
import GlobalModalClickResultEnum from "../../../../components/GlobalModal/types/GlobalModalClickResultEnum"

const useDataForm = () => {
  const [dataFormState, setDataFormState] = useState<EmployeeDataFormState>({
    visible: false,
    action: ActionTypeEnum.NONE,
    employeData: {
      code: "",
      name: "",
      surname: "",
      nip: "",
      pesel: "",
      dateOfBirth: "",
      user: {
        email: ""
      }
    }
  })

  const queryClient = useQueryClient()
  const [showGlobalModal, hideGlobalModal] = useGlobalModal()
  const [formRows] = useFormRows()

  const createEmployeeMutation = useMutation({
    mutationKey: ["createEmployee"],
    mutationFn: async (createEmployeeCommand: CreateEmployeeCommand) => {
      const employeesService: EmployeesService = EmployeesService.getInstance()
      await employeesService.createAsync(createEmployeeCommand)
    }
  })

  const updateEmployeeMutation = useMutation({
    mutationKey: ["updateEmployee"],
    mutationFn: async (updateEmployeeCommand: UpdateEmployeeCommand) => {
      const employeesService: EmployeesService = EmployeesService.getInstance()
      await employeesService.updateAsync(updateEmployeeCommand)
    }
  })

  
  const handleSave = useCallback(async (formData) => {
    if(dataFormState.action === ActionTypeEnum.ADD)
    {
      const command: CreateEmployeeCommand = {
        name: formData.name,
        surname: formData.surname,
        dateOfBirth: formData.dateOfBirth,
        nip: formData.nip,
        pesel: formData.pesel
      }

      await createEmployeeMutation.mutateAsync(command, {
        onSuccess: async () => {
          await queryClient.invalidateQueries({ queryKey: ["employess"] })
          setDataFormState(s => ({
            ...s,
            visible: false
          }))
        },
        onError: (error: Error) => {
          showGlobalModal({
            title: "Error",
            text: error.message,
            modalType: GlobalModalTypeEnum.Warning,
            buttonsType: GlobalModalButtonsTypeEnum.Ok,
            callback: (clickResult: GlobalModalClickResultEnum) => {
              hideGlobalModal()
            }
          })
        }
      })
    }
    else if(dataFormState.action === ActionTypeEnum.EDIT)
    {
      const updateEmployeeCommand: UpdateEmployeeCommand = {
        id: formData.id,
        name: formData.name,
        surname: formData.surname,
        pesel: formData.pesel,
        nip: formData.nip,
        dateOfBirth: formData.dateOfBirth
      }

      await updateEmployeeMutation.mutateAsync(updateEmployeeCommand, {
        onSuccess: async () => {
          await queryClient.invalidateQueries({ queryKey: ["employess"] })
          setDataFormState(s => ({
            ...s,
            visible: false
          }))
        },
        onError: (error: Error) => {
          showGlobalModal({
            title: "Error",
            text: error.message,
            modalType: GlobalModalTypeEnum.Warning,
            buttonsType: GlobalModalButtonsTypeEnum.Ok,
            callback: (clickResult: GlobalModalClickResultEnum) => {
              hideGlobalModal()
            }
          })
        }
      })
    }
  }, [dataFormState, createEmployeeMutation, updateEmployeeMutation, queryClient, showGlobalModal, hideGlobalModal])

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
    handleSave, 
    handleClose
  }
}

export default useDataForm