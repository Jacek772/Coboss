import ActionButtonType from "./enums/ActionButtonType"

interface IActionButtonDef {
  text: string
  type: ActionButtonType
  onClick: () => void
}

export default IActionButtonDef