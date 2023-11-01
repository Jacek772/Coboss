// Css
import { useNavigate } from "react-router-dom"
import "./index.css"
import INavCardProps from "./types/INavCardProps"
import { useCallback } from "react"

const NavCard: React.FC<INavCardProps> = ({ to, title, iconPath, text }) => {
  const navigate = useNavigate()

  const handleClick = useCallback((): void => {
    navigate(to)
  }, [navigate])

  return <div className="navcard-container" onClick={handleClick}>
    <div className="navcard-title-container">
      <img src={iconPath} alt="document" className="navcard-title-img" />
      <h2 className="navcard-title">
        {title}
      </h2>
    </div>
    <p className="navcard-description">{text}</p>
  </div>
}

export default NavCard