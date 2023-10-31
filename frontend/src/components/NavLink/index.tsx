// React
import { Link } from "react-router-dom"
import { useCallback, useState } from "react"

// Types
import INavLinkPorps from "./types/INavLinkPorps"
import INavLinkState from "./types/INavLinkState"

// Css
import "./index.css"

const NavLink: React.FC<INavLinkPorps> = ({ to, text, children  }: INavLinkPorps) => {
  const [state, setState] = useState<INavLinkState>({ open: false })

  const handleClickOpen = useCallback(() => {
    setState({
      ...state,
      open: !state.open
    })
  }, [state])

  return <li className="navlink-li">
    <div className="navlink-li-container">
      <Link to={to}>{text}</Link>
      {
        children ?
        <>
        {
           state.open ?
           <img src="./gfx/arrow-up.svg" alt="arrow-up" onClick={handleClickOpen}/>
           :
           <img src="./gfx/arrow-down.svg" alt="arrow-down" onClick={handleClickOpen}/>
        }
        </>
        :
        null
      }
    </div>
    {
      state.open && children ?
      <ul>
        {children}
      </ul>
      :
      null
    }
  </li> 

}

export default NavLink