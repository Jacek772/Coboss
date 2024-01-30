// Components
import { isMobile } from "react-device-detect"
import NavLink from "../NavLink"

// Css
import "./index.css"
import INavbarProps from "./types/INavbarProps"
import { useEffect, useState } from "react"

const Navbar: React.FC<INavbarProps> = ({ visible = false }: INavbarProps) => {

  return <nav className="navbar-nav " style={{display: (!isMobile ? "block" : (visible ? "block" : "none"))}}>
    <ul className="navbar-nav-ul">
      <NavLink to="/main" text="Home" />
      <NavLink to="/employees" text="Employees" />
      <NavLink to="/projects" text="Projects" />
      <NavLink to="/tasks" text="Tasks" />
      <NavLink to="/reports" text="Summaries and reports">
        <NavLink to="/settings/test" text="test"/>
      </NavLink>
      <NavLink to="/settings" text="Settings" />
    </ul>
  </nav>
}

export default Navbar