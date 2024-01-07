import { useMutation, useQueryClient } from "@tanstack/react-query";
import CreateProjectCommand from "../../../../types/Commands/CreateProjectCommand";
import ProjectsService from "../../../../services/ProjectsService";
import UpdateProjectCommand from "../../../../types/Commands/UpdateProjectCommand";
import ProjectDataFormState from "../../types/ProjectDataFormState";
import { useCallback, useState } from "react";
import ActionTypeEnum from "../../../../types/ActionTypeEnum";
import useGlobalModal from "../../../../hooks/useGlobalModal/index.hook";
import GlobalModalButtonsTypeEnum from "../../../../components/GlobalModal/types/GlobalModalButtonsTypeEnum";
import GlobalModalTypeEnum from "../../../../components/GlobalModal/types/GlobalModalTypeEnum";
import GlobalModalClickResultEnum from "../../../../components/GlobalModal/types/GlobalModalClickResultEnum";
import useFormRows from "../useFormRows/index.hook";

const useDataForm = () => {
  const [dataFormState, setDataFormState] = useState<ProjectDataFormState>({
    visible: false,
    action: ActionTypeEnum.NONE,
    projectData: {
      number: "",
      name:"",
      description: "",
      term: "",
      manager: {
        id: 0
      }
    }
  })

  const queryClient = useQueryClient()
  const [showGlobalModal, hideGlobalModal] = useGlobalModal()
  const [formRows] = useFormRows()

  const createProjectMutation = useMutation({
    mutationKey: ["createProject"],
    mutationFn: async (command: CreateProjectCommand) => {
      const projectsService: ProjectsService = ProjectsService.getInstance()
      await projectsService.createAsync(command);
    }
  })

  const updateProjectMutation = useMutation({
    mutationKey: ["updateProject"],
    mutationFn: async (updateEmployeeCommand: UpdateProjectCommand) => {
      const projectsService: ProjectsService = ProjectsService.getInstance()
      await projectsService.updateAsync(updateEmployeeCommand)
    }
  })

  const handleSave = useCallback(async (formData) => {
    if(dataFormState.action === ActionTypeEnum.ADD)
    {
      const command: CreateProjectCommand = {
        name: formData.name,
        description: formData.description,
        term: formData.term,
        managerId: formData.manager.id
      }

      await createProjectMutation.mutateAsync(command, {
        onSuccess: async () => {
          await queryClient.invalidateQueries({ queryKey: ["projects"] })
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
      const command: UpdateProjectCommand = {
        id: formData.id,
        name: formData.name,
        description: formData.description,
        term: formData.term,
        managerId: formData.manager.id,
      }

      await updateProjectMutation.mutateAsync(command, {
        onSuccess: async () => {
          await queryClient.invalidateQueries({ queryKey: ["projects"] })
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
  }, [dataFormState, createProjectMutation, updateProjectMutation, queryClient, hideGlobalModal, showGlobalModal])

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