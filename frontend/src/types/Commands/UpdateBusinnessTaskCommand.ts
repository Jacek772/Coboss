import CreateBusinnessTaskCommentCommand from "./CreateBusinnessTaskCommentCommand"
import UpdateBusinnessTaskCommentCommand from "./UpdateBusinnessTaskCommentCommand"

type UpdateBusinnessTaskCommand = {
  id: number
  name?: string
  description?: string
  date?: Date
  term?: Date
  projectId?: number,
  newComments?: CreateBusinnessTaskCommentCommand[]
  updatedComments?: UpdateBusinnessTaskCommentCommand[]
}

export default UpdateBusinnessTaskCommand