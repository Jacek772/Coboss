import React from "react"
import DataFormFieldData from "./DataFormFieldData"
import DataFormRowTypeEnum from "./DataFormRowTypeEnum"

type DataFormRow = {
  type: DataFormRowTypeEnum,
  items?: DataFormFieldData[]
  dataField?: string
  components?: React.FC[]
  caption?: string
  height?: number
}

export default DataFormRow