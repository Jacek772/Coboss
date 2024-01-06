import BusinnessTaskDTO from "./BusinnessTaskDTO"
import EmployeeDTO from "./EmployeeDTO"

type ProjectDTO = {
  number: string
  name: string
  description: string
  term: Date
  manager: EmployeeDTO
  businnessTasks: BusinnessTaskDTO[]
}

export default ProjectDTO