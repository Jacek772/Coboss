import IUserDTO from "./IUserDTO"

interface IEmployeeDTO {
  id: number
  name: string
  surname: string
  PESEL: string
  NIP: string
  User: IUserDTO
  EmployeeHistories: any[]
}

export default IEmployeeDTO