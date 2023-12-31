import ActionTypeEnum from "../../../types/ActionTypeEnum"

type EmployeeDataFormState = {
  visible: boolean,
  action: ActionTypeEnum,
  employeData: {
    code: string,
    name: string,
    surname: string,
    nip: string,
    pesel: string,
    dateOfBirth: string,
    user: {
      email: string
    }
  }
}

export default EmployeeDataFormState