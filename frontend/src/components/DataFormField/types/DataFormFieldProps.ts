// Types
import DataFormFieldItemOption from "./DataFormFieldItemOption"
import DataFormFieldType from "./enums/DataFormFieldType"

type DataFormFieldProps = {
  name: string
  type: DataFormFieldType
  value?: string
  label?: string
  labelWidth?: number
  height?: number
  width?: number
  options?: DataFormFieldItemOption[]
  onChange: (name: string, value: string) => void
}

export default DataFormFieldProps