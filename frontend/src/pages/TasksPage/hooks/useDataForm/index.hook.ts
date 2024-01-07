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
    businessTaskData: {
      name:"",
      description: "",
      date: "",
      term: "",
      project: {
        id: 0
      }
    }
  })

  const queryClient = useQueryClient()
  const [showGlobalModal, hideGlobalModal] = useGlobalModal()
  const [formRows] = useFormRows()

  const createBusinessTaskMutation = useMutation({
    mutationKey: ["createBusinnessTask"],
    mutationFn: async (command: CreateBusinnessTaskCommand) => {
      const projectsService: BusinnessTasksService = BusinnessTasksService.getInstance()
      await projectsService.createAsync(command);
    }
  })

  const updateBusinessTaskMutation = useMutation({
    mutationKey: ["updateBusinnessTask"],
    mutationFn: async (command: UpdateBusinnessTaskCommand) => {
      const projectsService: BusinnessTasksService = BusinnessTasksService.getInstance()
      await projectsService.updateAsync(command)
    }
  })

  const handleSave = useCallback(async (formData) => {
    if(dataFormState.action === ActionTypeEnum.ADD)
    {
      const command: CreateBusinnessTaskCommand = {
        name: formData.name,
        description: formData.description,
        term: formData.term,
        date: formData.date,
        projectId: formData.project.id
      }

      await createBusinessTaskMutation.mutateAsync(command, {
        onSuccess: async () => {
          await queryClient.invalidateQueries({ queryKey: ["businnessTasks"] })
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
      const command: UpdateBusinnessTaskCommand = {
        id: formData.id,
        name: formData.name,
        description: formData.description,
        term: formData.term
      }

      await updateBusinessTaskMutation.mutateAsync(command, {
        onSuccess: async () => {
          await queryClient.invalidateQueries({ queryKey: ["businnessTasks"] })
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
  }, [dataFormState, createBusinessTaskMutation, updateBusinessTaskMutation, queryClient, hideGlobalModal, showGlobalModal])

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