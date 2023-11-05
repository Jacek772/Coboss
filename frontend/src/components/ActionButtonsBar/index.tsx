// Types
import IActionButtonsBarProps from "./types/IActionButtonsBarProps"

// Css
import "./index.css"
import { useCallback } from "react"
import ActionButtonType from "./types/enums/ActionButtonType"

const ActionButtonsBar: React.FC<IActionButtonsBarProps> = ({ buttonsData }) => {

  const getStyleClassName = useCallback((actionButtonType: ActionButtonType): string => {
    switch(actionButtonType)
    {
      case ActionButtonType.Secondary:
        return "actionbuttonsbar-button-secondary"
      case ActionButtonType.Danger:
        return "actionbuttonsbar-button-danger"
      case ActionButtonType.Warning:
        return "actionbuttonsbar-button-warning"
      case ActionButtonType.Success:
        return "actionbuttonsbar-button-success"
      case ActionButtonType.Primary:
      default:
        return "actionbuttonsbar-button-primary";
    }
  }, [])

  return <div className="actionbuttonsbar-container">
    {
      buttonsData.map((x, index) => {
        return <button
          className={`actionbuttonsbar-button ${getStyleClassName(x.type)}`}
          key={index}
          onClick={() => x.onClick()} 
        >{x.text}</button>
      })
    }
  </div>
}

export default ActionButtonsBar