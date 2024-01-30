// Types
import ColDefProps from "../../../components/Grid/types/ColDefProps"
import GridColTypeEnum from "../../../components/Grid/types/enums/GridColTypeEnum"

const gridColDefs: ColDefProps[] = [
  {
    caption:"Name",
    field:"name",
    width: 200
  },
  {
    caption:"Date",
    field:"date",
    width: 200,
    type: GridColTypeEnum.Date
  },
  {
    caption:"Term",
    field:"term",
    width: 200,
    type: GridColTypeEnum.Date
  }
]

export default gridColDefs