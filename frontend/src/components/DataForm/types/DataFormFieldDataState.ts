
// Libraries
import { z } from "zod"

//Types
import DataFormFieldType from "../../DataFormField/types/enums/DataFormFieldType"

type DataFormFieldDataState = {
  name: string
  type: DataFormFieldType
  value: string
  validationSchema?: z.ZodSchema
  errorMessage?: string
}

export default DataFormFieldDataState