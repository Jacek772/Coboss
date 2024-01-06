import UserDTO from "./UserDTO"

type EmployeeDTO = {
  id: number
  name: string
  surname: string
  PESEL: string
  NIP: string
  User: UserDTO
  EmployeeHistories: any[]
}

export default EmployeeDTO