import { useCallback, useMemo } from "react"
import ActionButtonsBar from "../ActionButtonsBar"
import ActionButtonDef from "../ActionButtonsBar/types/ActionButtonDef"
import ActionButtonType from "../ActionButtonsBar/types/enums/ActionButtonType"
import Grid from "../Grid"
import ColDefProps from "../Grid/types/ColDefProps"
import GridColTypeEnum from "../Grid/types/enums/GridColTypeEnum"
import useGrid from "./hooks/useGrid/index.hook"
import useDataForm from "./hooks/useDataForm/index.hook"
import ActionTypeEnum from "../../types/ActionTypeEnum"
import DataForm from "../DataForm"
// import styles from "../index.module.css"

const gridColDefs: ColDefProps[] = [
  {
    caption:"Date",
    field:"date",
    width: 200,
    type: GridColTypeEnum.Date
  },
  {
    caption: "Time span",
    field: "timeSpan",
    width: 200,
  },
  {
    caption: "Description",
    field: "description",
    width: 200,
  }
]

const TaskRealisationsGrid: React.FC<any> = ({ data, setData }) => {
  const gridData = useGrid()
  const dataFormData = useDataForm()

  const handleClickAdd = useCallback(() => {
    dataFormData.setDataFormState(s => ({
      ...s,
      taskRealisationData: {
        id: 0,
        date: null,
        timeSpan: "",
        description: ""
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
    dataFormData.setDataFormState(s => ({
      ...s,
      taskRealisationData: {
        id: rowData.data.id,
        date: rowData.data.date,
        timeSpan: rowData.data.timeSpan,
        description: rowData.data.description,
      },
      action: ActionTypeEnum.EDIT,
      visible: true
    }))
  }, [dataFormData])

  const handleSave = useCallback(async (formData) => {
    if(formData.id < 0) {
      const taskRealisation = {
        id: formData.id,
        date: formData.date,
        timeSpan: formData.timeSpan,
        description: formData.description,
      }

      setData([...data, taskRealisation])
    }
    else
    {
      const updatedTaskRealisations = [...data]

      const index = updatedTaskRealisations.findIndex(x => x.id === formData.id)
      if(index >= 0)
      {
        updatedTaskRealisations[index].date = formData.date
        updatedTaskRealisations[index].timeSpan = formData.timeSpan
        updatedTaskRealisations[index].description = formData.description
      }

      setData([...updatedTaskRealisations])
    }

    dataFormData.setDataFormState(s => ({
      ...s,
      taskRealisationData: {
        id: 0,
        date: "",
        timeSpan: "00:00",
        description: "",
      },
      action: ActionTypeEnum.NONE,
      visible: false
    }))
  }, [data, dataFormData, setData])

  return <>
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
        caption="Task realisation"
        data={dataFormData.dataFormState.taskRealisationData}
        rows={dataFormData.formRows}
        onSave={handleSave}
        onClose={dataFormData.handleClose}
      />
      :
      null
    }
  </>
}

export default TaskRealisationsGrid