// Libraries
import React from "react"

// Css
import "./index.css"
import NavCard from "../../components/Navcard"
import navRoutes from "../../configuration/navRoutes"
import { useSelector } from "react-redux"
import { RootState } from "../../redux/store"

const MainPage: React.FC = () => {
  const reduxState: RootState = useSelector<RootState, RootState>(x => x)

  return <div className="page-container">
    <div className="page-caption-container">
      <div className="page-caption-row">
        <h1 className="page-caption">Witaj {reduxState.auth.user?.login} !</h1>
        <input className="input page-caption-input" placeholder="Wyszukaj..."/>
      </div>
      <hr/>
    </div>
    <div className="navcards-container">
      {
        navRoutes.map((x, index) => {
          return <NavCard
            key={index}
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