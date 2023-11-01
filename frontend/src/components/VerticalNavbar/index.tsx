// Components
import NavLink from "../NavLink"

// Css
import "./index.css"

const VerticalNavbar: React.FC = () => {
return <nav className="verticalnavbar-nav ">
    <ul className="verticalnavbar-nav-ul">
      <NavLink to="/main" text="Home" />
      <NavLink to="/employees" text="Employees" />
      <NavLink to="/projects" text="Projects" />
      <NavLink to="/tasks" text="Tasks" />
      <NavLink to="/reports" text="Summaries and reports">
        <NavLink to="/settings/test" text="test"/>
      </NavLink>
      <NavLink to="/settings" text="Settings">
        <NavLink to="/settings/test" text="test"/>
      </NavLink>
    </ul>
  </nav>
}

export default VerticalNavbar