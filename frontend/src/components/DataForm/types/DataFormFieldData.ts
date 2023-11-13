import DataFormFieldType from "../../DataFormField/types/enums/DataFormFieldType"

type DataFormFieldData = {
  label?: string
  name: string
  type: DataFormFieldType
  height?: number
  width?: number
  options?: []
}

export default DataFormFieldData