import DataFormRow from "./DataFormRow"

 type DataFormProps<T> = {
  caption?: string,
  data: T
  rows?: DataFormRow[]
  onSave?: (data: any) => void
  onClose?: () => void
}

export default DataFormProps