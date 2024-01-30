import { useCallback, useMemo } from "react"
import Grid from "../../components/Grid"
import PageBar from "../../components/PageBar"
import useGrid from "./hooks/useGrid/index.hook"
import gridColDefs from "./configuration/gridColDefs"
import ActionButtonType from "../../components/ActionButtonsBar/types/enums/ActionButtonType"
import ActionButtonDef from "../../components/ActionButtonsBar/types/ActionButtonDef"
import useGlobalModal from "../../hooks/useGlobalModal/index.hook"
import GlobalModalTypeEnum from "../../components/GlobalModal/types/GlobalModalTypeEnum"
import GlobalModalButtonsTypeEnum from "../../components/GlobalModal/types/GlobalModalButtonsTypeEnum"
import GlobalModalClickResultEnum from "../../components/GlobalModal/types/GlobalModalClickResultEnum"
import BusinnessTasksService from "../../services/BusinnessTasksService"
import { useMutation, useQueryClient } from "@tanstack/react-query"
import ActionButtonsBar from "../../components/ActionButtonsBar"
import FiltersBar from "../../components/FiltersBar"
import FiltersBarValue from "../../components/FiltersBar/types/FiltersBarValue"
import useFiltersBarItems from "./hooks/useFiltersBarItems/index.hook"
import useDataForm from "./hooks/useDataForm/index.hook"
import DataForm from "../../components/DataForm"
import ActionTypeEnum from "../../types/ActionTypeEnum"

// Css
import styles from "./index.module.css"

const TasksPage: React.FC = () => {
  const queryClient = useQueryClient()

  const [showGlobalModal, hideGlobalModal] = useGlobalModal()
  const gridData = useGrid()
  const dataFormData = useDataForm()
  const [filtersBarItems] = useFiltersBarItems()

  const deleteBusinnessTasksMutation = useMutation({
    mutationKey: ["deleteBusinnessTasks"],
    mutationFn: async (ids: number[]) => {
      const businnessTasksService: BusinnessTasksService = BusinnessTasksService.getInstance()
      await businnessTasksService.deleteAsync(ids)
    }
  })

  const handleGridRowDoubleClick = useCallback((index: number, rowData: any) => {
    dataFormData.setDataFormState(s => ({
      ...s,
      businessTaskData: rowData.data,
      action: ActionTypeEnum.EDIT,
      visible: true
    }))
  }, [dataFormData])

  const handleClickAdd = useCallback(() => {
    dataFormData.setDataFormState(s => ({
      ...s,
      visible: true,
      action: ActionTypeEnum.ADD,
      businessTaskData: {
        name:"?",
        description: "",
        date: "",
        term: "",
        project: {
          id: 0
        },
        comments: [],
        taskRealisations: []
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
      title: "Delete tasks",
      text: "Are you sure that want delete selected tasks?", 
      modalType: GlobalModalTypeEnum.Warning,
      buttonsType: GlobalModalButtonsTypeEnum.YesNo,
      callback: (clickResult: GlobalModalClickResultEnum) => {
        if(clickResult === GlobalModalClickResultEnum.Yes) {
          deleteBusinnessTasksMutation.mutate(ids, {
            onSuccess: async () => {
              await queryClient.invalidateQueries({ queryKey: ["businnessTasks"] })
              dataFormData.setDataFormState(s => ({
                ...s,
                visible: true,
                action: ActionTypeEnum.ADD,
                businessTaskData: {
                  name:"?",
                  description: "",
                  date: "",
                  term: "",
                  project: {
                    id: 0
                  },
                  comments: [],
                  taskRealisations: []
                }
              }))
            },
            onError: () => {

            }
           })
        }
        hideGlobalModal()
      }
    })
  }, [gridData.gridState, deleteBusinnessTasksMutation, queryClient, showGlobalModal, hideGlobalModal])

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
    let query = { ...gridData.gridState.query }

    const dateFilter = values.find(x => x.name === "date")
    if(dateFilter)
    {
      const valueFrom = dateFilter.values[0]
      const valueTo = dateFilter.values[1]

      if(valueFrom && valueTo)
      {
        query = {
          ...query,
          dateFrom: new Date(valueFrom),
          dateTo: new Date(valueTo)
        }
      }
      else
      {
        query = {
          ...query,
          dateFrom: null,
          dateTo: null
        }
      }
    }

    const termFilter = values.find(x => x.name === "term")
    if(termFilter)
    {
      const valueFrom = termFilter.values[0]
      const valueTo = termFilter.values[1]

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

    const projectFilter = values.find(x => x.name === "project")
    if(projectFilter)
    {
      const value = projectFilter.values[0]
      if(value)
      {
        query = {
          ...query,
          projectId: parseInt(value)
        }
      }
      else
      {
        query = {
          ...query,
          projectId: null
        }
      }
    }

    return query
  }, [gridData.gridState])

  const handleChangeFiltersBar = useCallback((values: FiltersBarValue[])  => {
    gridData.setGridState(s => ({
      ...s,
      query: createQuery(values)
    }))
  }, [gridData, createQuery])

  return <div className={styles.pageContainer}>
    <PageBar
      caption="Tasks"
      searchVisible={true}
      onChangeInput={
        (text: string) => {
          gridData.setGridState(s => ({...s, query: { ...s.query, searchText: text }}))
        }
      }
    />
    <div className={styles.filtersbarContainer}>
      <FiltersBar items={filtersBarItems} onChange={handleChangeFiltersBar}/>

    </div>
    <div className={styles.actionbuttonsbarContainer}>
      <ActionButtonsBar buttonsData={actionButtonDefs} />
    </div>
    <div className={styles.girdContainer}>
      <Grid
        colDefs={gridColDefs}
        rowsData={gridData.data}
        onSelectionChanged={gridData.handleSelectionChanged}
        onRowDoubleClick={handleGridRowDoubleClick}
        onSortChanged={gridData.handleSortChanged}
      />
    </div>
    {
      dataFormData.dataFormState.visible ?
      <DataForm
        caption={`${dataFormData.dataFormState.businessTaskData.name}`}
        data={dataFormData.dataFormState.businessTaskData}
        rows={dataFormData.formRows}
        onSave={dataFormData.handleSave}
        onClose={dataFormData.handleClose}
      />
      :
      null
    }
  </div>
}

export default TasksPage