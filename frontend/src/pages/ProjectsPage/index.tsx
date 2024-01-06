import { useQuery } from "@tanstack/react-query"

// Services
import ProjectsService from "../../services/ProjectsService"

// Types
import ProjectDTO from "../../types/DTO/ProjectDTO"
import Grid from "../../components/Grid"
import gridColDefs from "./configuration/gridColDefs"
import PageBar from "../../components/PageBar"
import ActionButtonsBar from "../../components/ActionButtonsBar"
import { useCallback, useMemo, useState } from "react"
import ProjectDataFormState from "./types/ProjectDataFormState"
import ActionTypeEnum from "../../types/ActionTypeEnum"
import DataForm from "../../components/DataForm"

// Css
import styles from "./index.module.css"
import ActionButtonDef from "../../components/ActionButtonsBar/types/ActionButtonDef"
import ActionButtonType from "../../components/ActionButtonsBar/types/enums/ActionButtonType"
import SortDirection from "../../components/Grid/types/enums/SortDirection"
import IRowData from "../../components/Grid/types/IRowData"

const ProjectsPage: React.FC = () => {
  const [dataFormState, setDataFormState] = useState<ProjectDataFormState>({
    visible: false,
    action: ActionTypeEnum.NONE,
    projectData: {

    }
  })

  const projectsQuery = useQuery({
    queryKey: ["projects"],
    queryFn: async () => {
      const projects: ProjectDTO[] = await ProjectsService
        .getInstance()
        .getProjectsAsync()

      return projects
    }
  })

  const handleGridRowDoubleClick = useCallback((index: number, rowData: any) => {

  }, [])

  const handleGridSortChanged = useCallback((field: string, direction: SortDirection) => {

  }, [])

  const handleClickAdd = useCallback(() => {
    setDataFormState(s => ({
      ...s,
      visible: true,
      action: ActionTypeEnum.ADD,
      projectData: {

      }
    }))
  },[])

  const handleClickDelete = useCallback(() => {

  }, [])

  const handleGridSelectionChanged = useCallback((rowsData: IRowData[]) => {

  }, [])

  const handleFormSave = useCallback(() => {

  }, [])

  const handleFormClose = useCallback(() => {

  }, [])

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
      caption="Projects"
      onChangeInput={(text: string) => {}}
    />
    <div className={styles.actionbuttonsbarContainer}>
      <ActionButtonsBar buttonsData={actionButtonDefs} />
    </div>
    <div className={styles.girdContainer}>
      <Grid
        colDefs={gridColDefs}
        rowsData={projectsQuery.data}
        onRowDoubleClick={handleGridRowDoubleClick}
        onSelectionChanged={handleGridSelectionChanged}
        onSortChanged={handleGridSortChanged}
      />
    </div>
    {
      dataFormState.visible ?
      <DataForm
        caption=""
        data={dataFormState.projectData}
        onSave={handleFormSave}
        onClose={handleFormClose}
      />
      :
      null
    }
  </div>
}

export default ProjectsPage