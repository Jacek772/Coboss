// Libraries
import React from "react"
import { Outlet } from "react-router-dom"

// Css
import "./index.css"
import NavLink from "../../components/NavLink"
import Header from "../../components/Header"

const MainLayout: React.FC = () => {
  return <div className="mainlayout-container">
    <Header />
    <main className="mainlayout-main">
      <nav className="mainlayout-nav">
        <ul className="mainlayout-nav-ul">
          <li>
            <NavLink to="/main" text="Home" />
          </li>
          <li>
            <NavLink to="/employees" text="Employees" />
          </li>
          <li>
            <NavLink to="/projects" text="Projects" />
          </li>
          <li>
            <NavLink to="/tasks" text="Tasks" />
          </li>
          <li>
            <NavLink to="/reports" text="Summaries and reports" />
          </li>
          <li>
            <NavLink to="/settings" text="Settings">
              <NavLink to="/settings/test" text="test"/>
            </NavLink>
          </li>
        </ul>
      </nav>
      <section>
        <Outlet/>
      </section>
    </main>
  </div>
}

export default MainLayout