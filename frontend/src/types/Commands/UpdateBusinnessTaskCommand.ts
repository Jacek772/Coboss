import CreateBusinnessTaskCommentCommand from "./CreateBusinnessTaskCommentCommand"
import CreateBusinnessTaskRealisationCommand from "./CreateBusinnessTaskRealisationCommand"
import UpdateBusinnessTaskCommentCommand from "./UpdateBusinnessTaskCommentCommand"
import UpdateBusinnessTaskRealisationCommand from "./UpdateBusinnessTaskRealisationCommand"

type UpdateBusinnessTaskCommand = {
  id: number
  name?: string
  description?: string
  date?: Date
  term?: Date
  projectId?: number,
  newComments?: CreateBusinnessTaskCommentCommand[]
  updatedComments?: UpdateBusinnessTaskCommentCommand[]
  newTaskRealisations?: CreateBusinnessTaskRealisationCommand[]
  updatedTaskRealisations?: UpdateBusinnessTaskRealisationCommand[]
}

export default UpdateBusinnessTaskCommand