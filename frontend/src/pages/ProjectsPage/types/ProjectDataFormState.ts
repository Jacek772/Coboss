import ActionTypeEnum from "../../../types/ActionTypeEnum"

type ProjectDataFormState = {
  visible: boolean
  action: ActionTypeEnum
  projectData: {
    number: string,
    name: string,
    description: string,
    term: string,
    manager: {
      id: number
    }
  }
}

export default ProjectDataFormState