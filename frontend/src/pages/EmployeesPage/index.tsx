// React
import { useCallback, useMemo } from "react"
import { useMutation, useQueryClient } from "@tanstack/react-query"

// Hooks
import useGlobalModal from "../../hooks/useGlobalModal/index.hook"

// Components
import Grid from "../../components/Grid"
import ActionButtonsBar from "../../components/ActionButtonsBar"
import DataForm from "../../components/DataForm"
import PageBar from "../../components/PageBar"

// Hooks
import useDataForm from "./hooks/useDataForm/index.hook"
import useGrid from "./hooks/useGrid/index.hook"

// Types
import GlobalModalTypeEnum from "../../components/GlobalModal/types/GlobalModalTypeEnum"
import GlobalModalButtonsTypeEnum from "../../components/GlobalModal/types/GlobalModalButtonsTypeEnum"
import GlobalModalClickResultEnum from "../../components/GlobalModal/types/GlobalModalClickResultEnum"
import ActionButtonType from "../../components/ActionButtonsBar/types/enums/ActionButtonType"
import ActionButtonDef from "../../components/ActionButtonsBar/types/ActionButtonDef"
import ActionTypeEnum from "../../types/ActionTypeEnum"

// Configuration
import gridColDefs from "./configuration/gridColDefs"

// Services
import EmployeesService from "../../services/EmployeesService"

// Css
import styles from "./index.module.css"

const EmployeesPage: React.FC = () => {
  const queryClient = useQueryClient()
  const [showGlobalModal, hideGlobalModal] = useGlobalModal()
  const dataFormData = useDataForm()
  const gridData = useGrid()

  const deleteEmployeeMutation = useMutation({
    mutationKey: ["deleteEmployee"],
    mutationFn: async (ids: number[]) => {
      const employeesService: EmployeesService = EmployeesService.getInstance()
      await employeesService.deleteAsync(ids)
    }
  })

  const handleGridRowDoubleClick = useCallback((index: number, rowData: any) => {
    dataFormData.setDataFormState(s => ({
      ...s,
      employeData: rowData.data,
      action: ActionTypeEnum.EDIT,
      visible: true
    }))
  }, [dataFormData])

  const handleClickDelete = useCallback(() => {
    const ids: number[] = gridData.gridState.selectedRows.map(x => x.data.id as number)
    if(ids.length === 0)
    {
      return
    }

    showGlobalModal({
      title: "Delete employees", 
      text: "Are you sure that want delete selected employees?",
      modalType: GlobalModalTypeEnum.Warning,
      buttonsType: GlobalModalButtonsTypeEnum.YesNo, 
      callback: (clickResult: GlobalModalClickResultEnum) => {
        if(clickResult === GlobalModalClickResultEnum.Yes) {
          deleteEmployeeMutation.mutate(ids, { 
            onSuccess: () => {
              queryClient.invalidateQueries({ queryKey: ["employess"] })
            },
            onError: () => {

            }
          })
        }
        hideGlobalModal()
      }
    })

  }, [gridData.gridState.selectedRows, deleteEmployeeMutation, queryClient, showGlobalModal, hideGlobalModal])

  const handleClickAdd = useCallback(() => {
    dataFormData.setDataFormState(s => ({
      ...s,
      visible: true,
      action: ActionTypeEnum.ADD,
      employeData: {
        code: "?",
        name: "",
        surname: "",
        nip: "",
        pesel: "",
        dateOfBirth: "",
        user: {
          email: ""
        }
      }
    }))
  },[dataFormData])

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

  return <div className={styles.pageContainer}>
      <PageBar 
        caption="Employees"
        onChangeInput={
          (text: string) => gridData.setGridState(s => (
            { 
              ...s, 
              query: {
                ...s.query,
                searchText: text 
              }
            }
          ))
        }
      />
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
            caption={`${dataFormData.dataFormState.employeData.name} ${dataFormData.dataFormState.employeData.surname} (${dataFormData.dataFormState.employeData.code})`}
            data={dataFormData.dataFormState.employeData}
            onSave={dataFormData.handleSave}
            onClose={dataFormData.handleClose}
            rows={dataFormData.formRows}/>
            :
            null
      }
  </div>
}

export default EmployeesPage