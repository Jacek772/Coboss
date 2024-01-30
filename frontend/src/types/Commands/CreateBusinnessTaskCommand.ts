import CreateBusinnessTaskCommentCommand from "./CreateBusinnessTaskCommentCommand"

type CreateBusinnessTaskCommand = {
  name: string
  description: string
  date: Date
  term: Date
  projectId: number
  comments: CreateBusinnessTaskCommentCommand[]
}

export default CreateBusinnessTaskCommand