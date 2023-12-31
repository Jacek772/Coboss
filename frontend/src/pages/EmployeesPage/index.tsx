// React
import { useCallback, useEffect, useMemo, useState } from "react"
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query"

// Hooks
import useGlobalModal from "../../hooks/useGlobalModal/index.hook"

// Components
import Grid from "../../components/Grid"
import ActionButtonsBar from "../../components/ActionButtonsBar"
import DataForm from "../../components/DataForm"
import PageBar from "../../components/PageBar"

// Types
import GlobalModalTypeEnum from "../../components/GlobalModal/types/GlobalModalTypeEnum"
import GlobalModalButtonsTypeEnum from "../../components/GlobalModal/types/GlobalModalButtonsTypeEnum"
import GlobalModalClickResultEnum from "../../components/GlobalModal/types/GlobalModalClickResultEnum"
import IGetEmployeesQuery from "../../types/Query/IGetEmployeesQuery"
import CreateEmployeeCommand from "../../types/Commands/CreateEmployeeCommand"
import UpdateEmployeeCommand from "../../types/Commands/UpdateEmployeeCommand"
import EmployeesPageGridQueryState from "./types/EmployeesPageGridQueryState"
import EmployeesPageGridState from "./types/EmployeesPageGridState"
import ActionButtonType from "../../components/ActionButtonsBar/types/enums/ActionButtonType"
import IActionButtonDef from "../../components/ActionButtonsBar/types/IActionButtonDef"
import IRowData from "../../components/Grid/types/IRowData"
import SortDirection from "../../components/Grid/types/enums/SortDirection"
import EmployeeDataFormState from "./types/EmployeeDataFormState"

// Configuration
import gridColDefs from "./configuration/gridColDefs"
import formRows from "./configuration/formRows"

// Services
import EmployeesService from "../../services/EmployeesService"

// Css
import "./index.css"
import ActionTypeEnum from "../../types/ActionTypeEnum"

const EmployeesPage: React.FC = () => {
  const [dataFormState, setDataFormState] = useState<EmployeeDataFormState>({
    visible: false,
    action:ActionTypeEnum.NONE,
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

  const [gridState, setGridState] = useState<EmployeesPageGridState>({
    selectedRows: []
  })

  const [gridQueryState, setGridQueryState] = useState<EmployeesPageGridQueryState>({ })

  const queryClient = useQueryClient()

  const [showGlobalModal, hideGlobalModal] = useGlobalModal()

  const employessQuery = useQuery({
    queryKey: ["getEmployess", gridQueryState],
    queryFn: async () => {
      const query: IGetEmployeesQuery = {
        searchText: gridQueryState.searchText,
        orderBy: gridQueryState.orderBy
      };
  
      const employees = await EmployeesService
        .getInstance()
        .getEmployeesAsync(query)
      return employees
    },
    staleTime: 60000
  })

  useEffect(() => {
    queryClient.invalidateQueries({ queryKey: ["getEmployess"] })
  }, [queryClient, gridQueryState])


  const handleGridRowDoubleClick = useCallback((index: number, rowData: any) => {
    setDataFormState({
      ...dataFormState,
      employeData: rowData.data,
      action: ActionTypeEnum.EDIT,
      visible: true
    })
  }, [dataFormState])

  // Mutations
  const createEmployeeMutation = useMutation({
    mutationKey: ["createEmployee"],
    mutationFn: async (createEmployeeCommand: CreateEmployeeCommand) => {
      const employeesService: EmployeesService = EmployeesService.getInstance()
      await employeesService.createEmployeesAsync(createEmployeeCommand)
    }
  })

  const updateEmployeeMutation = useMutation({
    mutationKey: ["updateEmployee"],
    mutationFn: async (updateEmployeeCommand: UpdateEmployeeCommand) => {
      const employeesService: EmployeesService = EmployeesService.getInstance()
      await employeesService.updateEmployeesAsync(updateEmployeeCommand)
    }
  })

  const deleteEmployeeMutation = useMutation({
    mutationKey: ["deleteEmployee"],
    mutationFn: async (ids: number[]) => {
      const employeesService: EmployeesService = EmployeesService.getInstance()
      await employeesService.deleteEmployeesAsync(ids)
    }
  })

  // Handles
  const handleFormSave = useCallback(async (formData) => {
    if(dataFormState.action === ActionTypeEnum.ADD)
    {
      const createEmployeeCommand: CreateEmployeeCommand = {
        name: formData.name,
        surname: formData.surname,
        dateOfBirth: formData.dateOfBirth,
        nip: formData.nip,
        pesel: formData.pesel
      }

      createEmployeeMutation.mutate(createEmployeeCommand, {
        onSuccess: async () => {
          await queryClient.invalidateQueries({ queryKey: ["getEmployess"] })
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

      updateEmployeeMutation.mutate(updateEmployeeCommand, {
        onSuccess: async () => {
          await queryClient.invalidateQueries({ queryKey: ["getEmployess"] })
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

  const handleFormClose = useCallback(() => {
    setDataFormState({
      ...dataFormState,
      visible: false
    })
  }, [dataFormState])


  const handleClickDelete = useCallback(() => {
    const ids: number[] = gridState.selectedRows.map(x => x.data.id as number)

    if(ids.length === 0)
    {
      return
    }

    showGlobalModal({
      title: "Delete employee", 
      text: "Are you sure that want delete selected employee?", 
      buttonsType: GlobalModalButtonsTypeEnum.YesNo, 
      modalType: GlobalModalTypeEnum.Warning,
      callback: (clickResult: GlobalModalClickResultEnum) => {
        if(clickResult === GlobalModalClickResultEnum.Yes)
        {
          deleteEmployeeMutation.mutate(ids, { 
            onSuccess: () => {
              queryClient.invalidateQueries({ queryKey: ["getEmployess"] })
            },
            onError: () => {

            }
          })
        }
        hideGlobalModal()
      }
    })

  }, [gridState.selectedRows, deleteEmployeeMutation, queryClient, showGlobalModal, hideGlobalModal])

  const handleClickAdd = useCallback(() => {
    setDataFormState(s => ({
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
  },[])
  
  const handleGridSortChanged = useCallback((field: string, direction: SortDirection) => {
    setGridQueryState({
      ...gridQueryState,
      orderBy: `${field}:${direction}`
    })

  }, [setGridQueryState, gridQueryState])

  const actionButtonDefs: IActionButtonDef[] = useMemo<IActionButtonDef[]>(() => [
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

  const handleGridSelectionChanged = useCallback((rowsData: IRowData[]) => {
    setGridState({...gridState, selectedRows: rowsData})
  },[gridState, setGridState])

  return <div className="page-container">
      <PageBar 
        caption="Employees"
        onChangeInput={
          (text: string) => setGridQueryState(s => ({ searchText: text }))
        }
      />
      <div className="employeespage-actionbuttonsbar-container">
        <ActionButtonsBar buttonsData={actionButtonDefs} />
      </div>
      <div className="employeespage-gird-container">
        <Grid
          colDefs={gridColDefs}
          rowsData={employessQuery.data}
          onRowDoubleClick={handleGridRowDoubleClick}
          onSelectionChanged={handleGridSelectionChanged}
          onSortChanged={handleGridSortChanged}
        />
      </div>
      {
        dataFormState.visible ?
          <DataForm
            caption={`${dataFormState.employeData.name} ${dataFormState.employeData.surname} (${dataFormState.employeData.code})`}
            data={dataFormState.employeData}
            onSave={handleFormSave}
            onClose={handleFormClose}
            rows={formRows}/>
            :
            null
      }
  </div>
}

export default EmployeesPage