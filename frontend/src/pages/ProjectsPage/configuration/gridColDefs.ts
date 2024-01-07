// Types
import ColDefProps from "../../../components/Grid/types/ColDefProps"
import GridColTypeEnum from "../../../components/Grid/types/enums/GridColTypeEnum"

const gridColDefs: ColDefProps[] = [
  {
    caption:"Number",
    field:"number",
    width: 200
  },
  {
    caption:"Name",
    field:"name",
    width: 200
  },
  {
    caption:"Term",
    field:"term",
    width: 200,
    type: GridColTypeEnum.Date
  },
  {
    caption: "Manager code",
    field: "manager.code",
    width: 200
  }
]

export default gridColDefs