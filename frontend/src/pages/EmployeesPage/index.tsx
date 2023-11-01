// React
import { useState } from "react"

// Components
import Grid from "../../components/Grid"

// Types
import IColDefProps from "../../components/Grid/types/IColDefProps"

// Css
import "./index.css"

const colDefs: IColDefProps[] = [
  {
    caption:"Name",
    field:"name",
    width: 200
  },
  {
    caption:"Surname",
    field:"surname",
    width: 200
  },
  {
    caption:"Age",
    field:"age",
    width: 200
  }
]

const EmployeesPage: React.FC = () => {
  const [rowsData, setRowsData] = useState([
    {
      name:"Joe",
      surname: "Doe",
      age: 24
    },
    {
      name:"Joe1",
      surname: "Doe1",
      age: 24
    },
    {
      name:"Joe2",
      surname: "Doe2",
      age: 24
    },
    {
      name:"Joe3",
      surname: "Doe3",
      age: 24
    },
    {
      name:"Joe",
      surname: "Doe",
      age: 24
    },
    {
      name:"Joe1",
      surname: "Doe1",
      age: 24
    },
    {
      name:"Joe2",
      surname: "Doe2",
      age: 24
    },
    {
      name:"Joe3",
      surname: "Doe3",
      age: 24
    },
    {
      name:"Joe",
      surname: "Doe",
      age: 24
    },
    {
      name:"Joe1",
      surname: "Doe1",
      age: 24
    },
    {
      name:"Joe2",
      surname: "Doe2",
      age: 24
    },
    {
      name:"Joe3",
      surname: "Doe3",
      age: 24
    },
    {
      name:"Joe",
      surname: "Doe",
      age: 24
    },
    {
      name:"Joe1",
      surname: "Doe1",
      age: 24
    },
    {
      name:"Joe2",
      surname: "Doe2",
      age: 24
    },
    {
      name:"Joe3",
      surname: "Doe3",
      age: 24
    },
    {
      name:"Joe",
      surname: "Doe",
      age: 24
    },
    {
      name:"Joe1",
      surname: "Doe1",
      age: 24
    },
    {
      name:"Joe2",
      surname: "Doe2",
      age: 24
    },
    {
      name:"Joe3",
      surname: "Doe3",
      age: 24
    }
  ])

  return <div className="page-container">

    <div className="employeespage-gird-container">
      <Grid
        colDefs={colDefs}
        rowsData={rowsData}
      />
    </div>
  </div>
}

export default EmployeesPage