import DataFormRow from "./DataFormRow"

 type DataFormProps<T> = {
  data: T
  caption?: string,
  rows?: DataFormRow[]
  onSave?: (data: any) => void
  onClose?: () => void
}

export default DataFormProps