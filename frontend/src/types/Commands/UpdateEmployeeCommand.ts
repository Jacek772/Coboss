type UpdateEmployeeCommand = {
  id: number
  name?: string
  surname?: string
  pesel?: string
  nip?: string
  dateOfBirth?: Date
}

export default UpdateEmployeeCommand