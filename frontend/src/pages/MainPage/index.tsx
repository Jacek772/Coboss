// Libraries
import React from "react"

// Css
import "./index.css"
import NavCard from "../../components/Navcard"
import navRoutes from "../../configuration/navRoutes"

const MainPage: React.FC = () => {
  return <div className="page-container">
    <div className="navcards-container">
      {
        navRoutes.map((x, index) => {
          return <NavCard
            to={x.to}
            title={x.title}
            iconPath={x.iconPath}
            text={x.text}
          />
          })
      }
    </div>
  </div>
}

export default MainPage