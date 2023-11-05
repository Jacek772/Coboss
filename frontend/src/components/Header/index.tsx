// React
import React from "react"
import { NavigateFunction, useNavigate } from "react-router-dom"

// Types
import IHeaderProps from "./types/IHeaderProps"

// Css
import "./index.css"

const Header: React.FC<IHeaderProps> = ({ onClickHamburger }: IHeaderProps) => {
  const navigate: NavigateFunction = useNavigate()

  const handeClickLogout = (): void => {
    navigate("/logout")
  }

  return <header className="header-container">
    <img id="headerLogo" className="header-logo" src="./gfx/png/cobos_logo_1.png" alt="cobos_logo_1"/>
    <img id="headerHamburgerbutton" 
      className="header-hamburgerbutton"
      onClick={() => onClickHamburger()}
      src="./gfx/svg/hamburger.svg" 
      alt="hamburger button" />
    <div className="header-items">
      <button 
        className="button button-secondary"
        onClick={handeClickLogout}>Logout</button>
    </div>
  </header>
}

export default Header