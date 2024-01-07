import { useMutation, useQueryClient } from "@tanstack/react-query"

// Services
import ProjectsService from "../../services/ProjectsService"

// Types
import Grid from "../../components/Grid"
import gridColDefs from "./configuration/gridColDefs"
import PageBar from "../../components/PageBar"
import ActionButtonsBar from "../../components/ActionButtonsBar"
import { useCallback, useMemo } from "react"
import ActionTypeEnum from "../../types/ActionTypeEnum"
import DataForm from "../../components/DataForm"

// Css
import styles from "./index.module.css"
import ActionButtonDef from "../../components/ActionButtonsBar/types/ActionButtonDef"
import ActionButtonType from "../../components/ActionButtonsBar/types/enums/ActionButtonType"
import useGlobalModal from "../../hooks/useGlobalModal/index.hook"
import GlobalModalButtonsTypeEnum from "../../components/GlobalModal/types/GlobalModalButtonsTypeEnum"
import GlobalModalTypeEnum from "../../components/GlobalModal/types/GlobalModalTypeEnum"
import GlobalModalClickResultEnum from "../../components/GlobalModal/types/GlobalModalClickResultEnum"
import FiltersBar from "../../components/FiltersBar"
import useFiltersBarItems from "./hooks/useFiltersBarItems/index.hook"
import FiltersBarValue from "../../components/FiltersBar/types/FiltersBarValue"
import useGrid from "./hooks/useGrid/index.hook"
import useDataForm from "./hooks/useDataForm/index.hook"

const ProjectsPage: React.FC = () => {
  const queryClient = useQueryClient()
  const [showGlobalModal, hideGlobalModal] = useGlobalModal()
  const gridData = useGrid()
  const dataFormData = useDataForm()
  const [filtersBarItems] = useFiltersBarItems()

  const deleteProjectsMutation = useMutation({
    mutationKey: ["deleteProject"],
    mutationFn: async (ids: number[]) => {
      const projectsService: ProjectsService = ProjectsService.getInstance()
      await projectsService.deleteAsync(ids)
    }
  })

  const handleGridRowDoubleClick = useCallback((index: number, rowData: any) => {
    dataFormData.setDataFormState(s => ({
      ...s,
      projectData: rowData.data,
      action: ActionTypeEnum.EDIT,
      visible: true
    }))
  }, [dataFormData])

  const handleClickAdd = useCallback(() => {
    dataFormData.setDataFormState(s => ({
      ...s,
      visible: true,
      action: ActionTypeEnum.ADD,
      projectData: {
        number: "?",
        name:"?",
        description: "",
        term: "",
        manager: {
          id: 0
        } 
      }
    }))
  },[dataFormData])

  const handleClickDelete = useCallback(async () => {
    const ids: number[] = gridData.gridState.selectedRows.map(x => x.data.id as number)
    if(ids.length === 0)
    {
      return
    }

    showGlobalModal({
      title: "Delete projects",
      text: "Are you sure that want delete selected projects?", 
      modalType: GlobalModalTypeEnum.Warning,
      buttonsType: GlobalModalButtonsTypeEnum.YesNo,
      callback: (clickResult: GlobalModalClickResultEnum) => {
        if(clickResult === GlobalModalClickResultEnum.Yes) {
          deleteProjectsMutation.mutate(ids, {
            onSuccess: () => {
              queryClient.invalidateQueries({ queryKey: ["projects"] })
            },
            onError: () => {

            }
           })
        }
        hideGlobalModal()
      }
    })
  }, [gridData.gridState, deleteProjectsMutation, queryClient, showGlobalModal, hideGlobalModal])

  const actionButtonDefs: ActionButtonDef[] = useMemo<ActionButtonDef[]>(() => [
    {
      text: "Add", 
      type: ActionButtonType.Primary, 
      onClick: handleClickAdd
    },
    { 
      text: "Delete", 
      type: ActionButtonType.Danger, 
      onClick: handleClickDelete
    }
  ], [handleClickAdd, handleClickDelete])

  const createQuery = useCallback((values: FiltersBarValue[]) => {
    let query = {}

    const periodFilter = values.find(x => x.name === "term")
    if(periodFilter)
    {
      const valueFrom = periodFilter.values[0]
      const valueTo = periodFilter.values[1]

      if(valueFrom && valueTo)
      {
        query = {
          ...query,
          termFrom: new Date(valueFrom),
          termTo: new Date(valueTo)
        }
      }
      else
      {
        query = {
          ...query,
          termFrom: null,
          termTo: null
        }
      }
    }

    const managerFilter = values.find(x => x.name === "manager")
    if(managerFilter)
    {
      const value = managerFilter.values[0]
      if(value)
      {
        query = {
          ...query,
          managerId: parseInt(value)
        }
      }
      else
      {
        query = {
          ...query,
          managerId: null
        }
      }
    }

    return query
  }, [])

  const handleChangeFiltersBar = useCallback((values: FiltersBarValue[])  => {
    gridData.setGridState(s => ({
      ...s,
      query: createQuery(values)
    }))
  }, [gridData, createQuery])

  return <div className={styles.pageContainer}>
    <PageBar
      caption="Projects"
      onChangeInput={
        (text: string) => {
          gridData.setGridState(s => ({...s, query: { ...s.query, searchText: text }}))
        }
      }
    />
    <div className={styles.filtersbarContainer}>
      <FiltersBar items={filtersBarItems} onChange={handleChangeFiltersBar} />
    </div>
    <div className={styles.actionbuttonsbarContainer}>
      <ActionButtonsBar buttonsData={actionButtonDefs} />
    </div>
    <div className={styles.girdContainer}>
      <Grid
        colDefs={gridColDefs}
        rowsData={gridData.data}
        onSelectionChanged={gridData.handleSelectionChanged}
        onSortChanged={gridData.handleSortChanged}
        onRowDoubleClick={handleGridRowDoubleClick}
      />
    </div>
    {
      dataFormData.dataFormState.visible ?
      <DataForm
        caption={`${dataFormData.dataFormState.projectData.name} (${dataFormData.dataFormState.projectData.number})`}
        data={dataFormData.dataFormState.projectData}
        rows={dataFormData.formRows}
        onSave={dataFormData.handleSave}
        onClose={dataFormData.handleClose}
      />
      :
      null
    }
  </div>
}

export default ProjectsPage