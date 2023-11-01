// Libraries
import React from "react"
import { Outlet } from "react-router-dom"

// Components
import Header from "../../components/Header"
import VerticalNavbar from "../../components/VerticalNavbar"

// Css
import "./index.css"

const MainLayout: React.FC = () => {
  return <div className="mainlayout-container">
    <Header />
    <main className="mainlayout-main">
      <VerticalNavbar/>
      <section className="section">
        <Outlet/>
      </section>
    </main>
  </div>
}

export default MainLayout