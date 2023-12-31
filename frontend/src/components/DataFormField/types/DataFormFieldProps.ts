// Libraries
import { z } from "zod"

// Types
import DataFormFieldItemOption from "./DataFormFieldItemOption"
import DataFormFieldType from "./enums/DataFormFieldType"

type DataFormFieldProps = {
  name: string
  type: DataFormFieldType
  onChange: (name: string, value: string) => void
  onError?: (name: string, message: string) => void
  errorMessage?: string
  isReadonly?: boolean
  value?: string
  label?: string
  labelWidth?: number
  height?: number
  width?: number
  options?: DataFormFieldItemOption[]
  validationSchema?: z.ZodSchema
}

export default DataFormFieldProps