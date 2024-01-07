import GridColTypeEnum from "./enums/GridColTypeEnum"

type ColDefProps = {
  caption: string,
  field: string,
  width: number,
  type?: GridColTypeEnum
}

export default ColDefProps

