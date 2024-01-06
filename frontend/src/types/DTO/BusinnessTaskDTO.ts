import BusinnessTaskCommentDTO from "./BusinnessTaskCommentDTO"
import BusinnessTaskRealisationDTO from "./BusinnessTaskRealisationDTO"

type BusinnessTaskDTO = {
  name: string
  description: string
  date: Date
  term: Date
  taskRealisations: BusinnessTaskRealisationDTO[]
  comments: BusinnessTaskCommentDTO[]
}

export default BusinnessTaskDTO