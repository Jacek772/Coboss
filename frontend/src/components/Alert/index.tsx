import React from "react"

// Types
import AlertProps from "./types/AlertProps"

// Css
import "./index.css"
import AlertType from "./types/AlertType"

const Alert: React.FC<AlertProps> = ({ caption, text, type })  => {
  const getAlertTypeClass = (type: AlertType): string => {
    switch(type)
    {
      case AlertType.Danger:
        return "alert-danger"
      default:
        return ""
    }
  }

  return <div className={`alert ${getAlertTypeClass(type)}`}>
    <h2 className="alert-caption">{caption}</h2>
    <p className="alert-text">{text}</p>
  </div>
}

export default Alert