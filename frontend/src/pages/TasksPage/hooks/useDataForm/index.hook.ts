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
import CreateBusinnessTaskCommentCommand from "../../../../types/Commands/CreateBusinnessTaskCommentCommand"
import BusinnessTasksCommentsService from "../../../../services/BusinnessTasksCommentsService"
import BusinnessTaskRealisationsService from "../../../../services/BusinnessTaskRealisationsService"

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
      },
      comments: [],
      taskRealisations: [],
      employees: []
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

  // const createCommentMutation = useMutation({
  //   mutationKey: ["createComment"],
  //   mutationFn: async (command: CreateBusinnessTaskCommentCommand) => {
  //     const businnessTasksCommentsService: BusinnessTasksCommentsService = BusinnessTasksCommentsService.getInstance()
  //     await businnessTasksCommentsService.createAsync(command)
  //   }
  // })

  const deleteCommentsMutation = useMutation({
    mutationKey: ["deleteComments"],
    mutationFn: async (ids: number[]) => {
      const businnessTasksCommentsService: BusinnessTasksCommentsService = BusinnessTasksCommentsService.getInstance()
      await businnessTasksCommentsService.deleteAsync(ids)
    }
  })

  const deleteBusinnessTaskRealisationsMutation  = useMutation({
    mutationKey: ["deleteBusinnessTaskRealisations"],
    mutationFn: async (ids: number[]) => {
      const businnessTaskRealisationsService: BusinnessTaskRealisationsService = BusinnessTaskRealisationsService.getInstance()
      await businnessTaskRealisationsService.deleteAsync(ids)
    }
  })

  const handleSave = useCallback(async (formData) => {
    if(dataFormState.action === ActionTypeEnum.ADD)
    {
      const newComments: CreateBusinnessTaskCommentCommand[] = formData.comments.filter(x => x.id < 0).map(x => ({
        text: x.text,
        date: x.date,
        userId: x.user.id,
        taskId: 0
      }))

      // Comments
      const command: CreateBusinnessTaskCommand = {
        name: formData.name,
        description: formData.description,
        term: formData.term,
        date: formData.date,
        projectId: formData.projectId,
        comments: newComments
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
        date: formData.date,
        term: formData.term,
        projectId: formData.projectId,
      }

      // Comments
      command.newComments = formData.comments.filter(x => x.id < 0).map(x => ({
        text: x.text,
        date: x.date,
        userId: x.user.id,
      }))

      command.updatedComments = formData.comments.filter(x => x.id > 0).map(x => ({
        id: x.id,
        text: x.text,
      })) 

      
      // Task realisations
      command.newTaskRealisations = formData.taskRealisations.filter(x => x.id < 0).map(x => ({
        date: x.date,
        timeSpan: x.timeSpan,
        description: x.description,
        employeeId: x.employee.id,
        taskId: 0
      }))

      command.updatedTaskRealisations = formData.taskRealisations.filter(x => x.id > 0).map(x => ({
        id: x.id,
        date: x.date,
        timeSpan: x.timeSpan,
        description: x.description,
        employeeId: x.employee.id,
      }))

      console.log(command)

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

      const commentsToDeleteIds: number[] = dataFormState.businessTaskData.comments
        .filter(x => !formData.comments.some(y => y.id === x.id))
        .map(x => x.id)

      if(commentsToDeleteIds.length > 0)
      {
        await deleteCommentsMutation.mutateAsync(commentsToDeleteIds, {
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

      const taskRealisationsToDeleteIds: number[] = dataFormState.businessTaskData.taskRealisations
        .filter(x => !formData.taskRealisations.some(y => y.id === x.id))
        .map(x => x.id)

      if(taskRealisationsToDeleteIds.length > 0)
      {
        deleteBusinnessTaskRealisationsMutation.mutateAsync(taskRealisationsToDeleteIds, {
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
    }
  }, [dataFormState, createBusinessTaskMutation, updateBusinessTaskMutation, queryClient, hideGlobalModal, showGlobalModal, deleteCommentsMutation])

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