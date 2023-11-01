// React
import React from "react"
import { NavigateFunction, useNavigate } from "react-router-dom"

// Css
import "./index.css"

const Header: React.FC = () => {
  const navigate: NavigateFunction = useNavigate()

  const handeClickLogout = (): void => {
    navigate("/logout")
  }

  return <header className="header-container">
    <img className="header-logo" src="./gfx/png/cobos_logo_1.png" alt="cobos_logo_1"/>
    <div className="header-items">
      <button 
        className="button button-secondary"
        onClick={handeClickLogout}>Logout</button>
    </div>
  </header>
}

export default Header