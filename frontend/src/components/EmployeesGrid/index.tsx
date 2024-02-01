import React, { useCallback, useMemo } from "react"
import ActionButtonDef from "../ActionButtonsBar/types/ActionButtonDef"
import ActionButtonType from "../ActionButtonsBar/types/enums/ActionButtonType"
import ActionButtonsBar from "../ActionButtonsBar"
import ColDefProps from "../Grid/types/ColDefProps"
import GridColTypeEnum from "../Grid/types/enums/GridColTypeEnum"
import Grid from "../Grid"
import DataForm from "../DataForm"
import useGrid from "./hooks/useGrid/index.hook"
import useDataForm from "./hooks/useDataForm/index.hook"
import ActionTypeEnum from "../../types/ActionTypeEnum"
import NumberUtils from "../../utils/NumberUtils"
import { useQuery } from "@tanstack/react-query"

type EmployeesGridProps = {
  data: any
  setData: (data: any) => void
}

const gridColDefs: ColDefProps[] = [
  {
    caption:"Code",
    field:"code",
    width: 200
  },
  {
    caption: "Name",
    field: "name",
    width: 200,
  },
  {
    caption: "Surname",
    field: "surname",
    width: 200,
  }
]

const EmployeesGrid: React.FC<EmployeesGridProps> = ({ data, setData }) => {
  const gridData = useGrid()
  const dataFormData = useDataForm()

  const currentEmployeeToTaskQuery = useQuery({
    queryKey: ["currentEmployeeToTaskEmployeesGrid"],
    queryFn: async() => {
      // return UsersService
      //   .getInstance()
      //   .getCurrentAsync()
    },
    staleTime: 60000
  })

  const handleClickAdd = useCallback(() => {
    dataFormData.setDataFormState(s => ({
      ...s,
      employeeData: {
        id: "",
        employeeId: 0,
        code: "?",
        name: "",
        surname: "",
      },
      action: ActionTypeEnum.ADD,
      visible: true
    }))
  }, [dataFormData])

  const handleClickDelete = useCallback(() => {
    const ids: number[] = gridData.gridState.selectedRows.map(x => x.data.id as number)
    if(ids.length === 0)
    {
      return
    }

    const updatedData = data.filter(x => !ids.includes(x.id))
    setData?.([...updatedData])
  }, [gridData.gridState, data, setData])

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

  const handleGridRowDoubleClick = useCallback((index: number, rowData: any) => {
    console.log(rowData)
    dataFormData.setDataFormState(s => ({
      ...s,
      employeeData: {
        id: `${rowData.data.id}_${rowData.data.id}`,
        employeeId: rowData.data.id,
        code: rowData.data.code,
        name: rowData.data.name,
        surname: rowData.data.surname,
      },
      action: ActionTypeEnum.EDIT,
      visible: true
    }))
  }, [dataFormData])

  const handleSave = useCallback(async (formData) => {
    const index: number = data.findIndex(x => `${x.id}_${x.id}` === formData.id)
    if(index >= 0)
    {
      const updatedEmployees = [...data]

      const index = updatedEmployees.findIndex(x => x.id === formData.id)
      if(index >= 0)
      {
        updatedEmployees[index].employeeId = formData.text
      }

      setData([...updatedEmployees])
    }
    else
    {
      const employee = {
        id: formData.id,
        employeeId: formData.employeeId,
        code: formData.code,
        name: formData.name,
        surname: formData.surname,
      }
        
      setData([...data, employee])
    }

    dataFormData.setDataFormState(s => ({
      ...s,
      employeeData: {
        id: "",
        employeeId: 0,
        code: "",
        name: "",
        surname: "",
      },
      action: ActionTypeEnum.NONE,
      visible: false
    }))

  }, [data, setData])

  return <div style={{ marginBottom: 40 }}>
  <ActionButtonsBar buttonsData={actionButtonDefs} />
  <Grid
      colDefs={gridColDefs}
      rowsData={data ?? []}
      onSelectionChanged={gridData.handleSelectionChanged}
      onRowDoubleClick={handleGridRowDoubleClick}
    />
        {
      dataFormData.dataFormState.visible ?
      <DataForm
        caption={`${dataFormData.dataFormState.employeeData.code}`}
        data={dataFormData.dataFormState.employeeData}
        rows={dataFormData.formRows}
        onSave={handleSave}
        onClose={dataFormData.handleClose}
      />
      :
      null
    }
  </div>
}

export default EmployeesGrid