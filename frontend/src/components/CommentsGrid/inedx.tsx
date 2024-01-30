import React, { useCallback, useMemo, useState } from "react"
import ColDefProps from "../Grid/types/ColDefProps"
import GridColTypeEnum from "../Grid/types/enums/GridColTypeEnum"
import ActionButtonsBar from "../ActionButtonsBar"
import ActionButtonDef from "../ActionButtonsBar/types/ActionButtonDef"
import ActionButtonType from "../ActionButtonsBar/types/enums/ActionButtonType"
import DataForm from "../DataForm"
import Grid from "../Grid"
import useDataForm from "./hooks/useDataForm/index.hook"
import useGrid from "./hooks/useGrid/index.hook"
import ActionTypeEnum from "../../types/ActionTypeEnum"
import UsersService from "../../services/UsersService"
import { useQuery } from "@tanstack/react-query"
import NumberUtils from "../../utils/NumberUtils"

type CommentsGridProps = {
  data: any
  setData: (data: any) => void
}

const gridColDefs: ColDefProps[] = [
  {
    caption:"Text",
    field:"text",
    width: 400
  },
  {
    caption: "Date",
    field: "date",
    width: 200,
    type: GridColTypeEnum.Date
  }
]

const CommentsGrid: React.FC<CommentsGridProps> = ({ data, setData }) => {
  const gridData = useGrid()
  const dataFormData = useDataForm()

  const currentUserQuery = useQuery({
    queryKey: ["currentUserCommentsGrid"],
    queryFn: async() => {
      return UsersService
        .getInstance()
        .getCurrentAsync()
    },
    staleTime: 60000
  })


  const handleClickAdd = useCallback(() => {
    dataFormData.setDataFormState(s => ({
      ...s,
      commentData: {
        id: -1 * NumberUtils.getRandomInt(),
        text: "",
        date: new Date().toISOString(),
        username: currentUserQuery?.data?.email,
        userId: currentUserQuery?.data?.id
      },
      action: ActionTypeEnum.ADD,
      visible: true
    }))
  }, [currentUserQuery.data, dataFormData])

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
      commentData: {
        id: rowData.data.id,
        date: rowData.data.date,
        text: rowData.data.text,
        username: rowData.data.user.email,
        userId: rowData.data.user.id
      },
      action: ActionTypeEnum.EDIT,
      visible: true
    }))
  }, [dataFormData])

  const handleSave = useCallback(async (formData) => {

    if(formData.id < 0) {
      const comment = {
        id: formData.id,
        date: formData.date,
        text: formData.text,
        user: {
          email: formData.username,
          id: formData.userId
        }
      }
  
      setData([...data, comment])
    }
    else
    {
      const updatedComments = [...data]

      const index = updatedComments.findIndex(x => x.id === formData.id)
      if(index >= 0)
      {
        updatedComments[index].text = formData.text
      }

      setData([...updatedComments])
    }

    dataFormData.setDataFormState(s => ({
      ...s,
      commentData: {
        id: 0,
        date: "",
        text: "",
        username: "",
        userId: 0
      },
      action: ActionTypeEnum.NONE,
      visible: false
    }))
  },[data, dataFormData, setData])


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
        caption="Comment"
        data={dataFormData.dataFormState.commentData}
        rows={dataFormData.formRows}
        onSave={handleSave}
        onClose={dataFormData.handleClose}
      />
      :
      null
    }
  </div>
}

export default CommentsGrid