import ActionButtonType from "./enums/ActionButtonType"

type ActionButtonDef = {
  text: string
  type: ActionButtonType
  onClick: () => void
}

export default ActionButtonDef