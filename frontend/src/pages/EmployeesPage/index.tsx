// React
import { useCallback, useState } from "react"

// Components
import Grid from "../../components/Grid"
import FiltersBar from "../../components/FiltersBar"
import ActionButtonsBar from "../../components/ActionButtonsBar"

// Types
import FiltersBarItemType from "../../components/FiltersBarItem/types/enums/FiltersBarItemType"
import IFiltersBarItemData from "../../components/FiltersBar/types/IFiltersBarItemData"

// Configuration
import gridColDefs from "./configuration/gridColDefs"
import actionButtonDefs from "./configuration/actionButtonDefs"

// Css
import "./index.css"
import DataForm from "../../components/DataForm"

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


  const filterBarItems: IFiltersBarItemData[] = [
    { name:"period", type: FiltersBarItemType.DatePeriod, label: "Period"  }
  ]

  const handleRowClick = useCallback((index: number, rowsData: any) => {
    
  }, [])

  const handleScrollEnd = useCallback((lastRow: any) => {
    console.log(lastRow)
  }, [])

  return <div className="page-container">
    <div className="page-caption-container">
      <div className="page-caption-row">
        <h1 className="page-caption">Employees</h1>
        <input className="input page-caption-input" placeholder="Wyszukaj..."/>
      </div>
      <hr/>
    </div>
    <div className="employeespage-filtersbar-container">
      <FiltersBar items={filterBarItems} />
    </div>
    <div className="employeespage-actionbuttonsbar-container">
      <ActionButtonsBar buttonsData={actionButtonDefs} />
    </div>
    <div className="employeespage-gird-container">
      <Grid
        colDefs={gridColDefs}
        rowsData={rowsData}
        onScrollEnd={handleScrollEnd}
        onRowClick={handleRowClick}
      />
    </div>

    <DataForm/>
  </div>
}

export default EmployeesPage