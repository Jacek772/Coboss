// Libraries
import React, { useCallback, useState } from "react"
import { Outlet } from "react-router-dom"

// Components
import Header from "../../components/Header"
import Navbar from "../../components/Navbar"

// Css
import "./index.css"

const MainLayout: React.FC = () => {
  const [state, setState] = useState({ visible: false })

  const handleClickHamburger = useCallback(() => {
    setState({
      ...state,
      visible: !state.visible
    })
  }, [state])

  return <div className="mainlayout-container">
    <Header onClickHamburger={handleClickHamburger} />
    <main className="mainlayout-main">
      <Navbar visible={state.visible} />
      <section className="section">
        <Outlet/>
      </section>
    </main>
  </div>
}

export default MainLayout