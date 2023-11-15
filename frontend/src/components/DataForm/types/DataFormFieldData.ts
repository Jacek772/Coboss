// Types
import DataFormFieldItemOption from "../../DataFormField/types/DataFormFieldItemOption"
import DataFormFieldType from "../../DataFormField/types/enums/DataFormFieldType"

type DataFormFieldData = {
  name: string
  type: DataFormFieldType
  isReadonly?: boolean
  label?: string
  labelWidth?: number
  height?: number
  width?: number
  options?: DataFormFieldItemOption[]
}

export default DataFormFieldData