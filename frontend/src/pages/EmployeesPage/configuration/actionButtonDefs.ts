// Types
import IActionButtonDef from "../../../components/ActionButtonsBar/types/IActionButtonDef";
import ActionButtonType from "../../../components/ActionButtonsBar/types/enums/ActionButtonType";

const actionButtonDefs: IActionButtonDef[] = [
  { text: "Test1", type: ActionButtonType.Danger, onClick: () => {} },
  { text: "Test2", type: ActionButtonType.Primary, onClick: () => {} },
  { text: "Test3", type: ActionButtonType.Secondary, onClick: () => {} },
  { text: "Test4", type: ActionButtonType.Success, onClick: () => {} },
  { text: "Test5", type: ActionButtonType.Warning, onClick: () => {} }
]

export default actionButtonDefs