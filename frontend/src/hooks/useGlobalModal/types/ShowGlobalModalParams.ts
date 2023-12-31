import GlobalModalButtonsTypeEnum from "../../../components/GlobalModal/types/GlobalModalButtonsTypeEnum"
import GlobalModalClickResultEnum from "../../../components/GlobalModal/types/GlobalModalClickResultEnum"
import GlobalModalTypeEnum from "../../../components/GlobalModal/types/GlobalModalTypeEnum"

 type ShowGlobalModalParams = {
  title: string, 
  text: string, 
  buttonsType: GlobalModalButtonsTypeEnum, 
  modalType: GlobalModalTypeEnum,
  callback?: (clickResult: GlobalModalClickResultEnum) => void
}

export default ShowGlobalModalParams