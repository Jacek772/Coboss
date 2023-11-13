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
import DataFormRow from "../../components/DataForm/types/DataFormRow"
import DataFormFieldType from "../../components/DataFormField/types/enums/DataFormFieldType"

const data = [
  {
    login: "joe",
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
]

const EmployeesPage: React.FC = () => {
  const [rowsData, setRowsData] = useState([...data])
  const [employeData, setEmployeData] = useState(
    {
      login: "joedoe",
      code: "00001",
      name:"Joe",
      surname: "Doe",
      age: 24
    }
  )

  const filterBarItems: IFiltersBarItemData[] = [
    { name:"period", type: FiltersBarItemType.DatePeriod, label: "Period"  }
  ]

  const handleRowClick = useCallback((index: number, rowsData: any) => {
    
  }, [])

  const handleScrollEnd = useCallback((lastRow: any) => {
    console.log(lastRow)
  }, [])

  const handleSaveForm = useCallback(() => {

  }, [])

  const rows: DataFormRow[] = [
    {
      caption:"User",
      items: [
        { label:"Code", name: "code", type: DataFormFieldType.String },
        { label:"Login", name: "login", type: DataFormFieldType.String }
      ]
    },
    {
      caption:"Employe",
      items: [
        { label:"Name", name: "name", type: DataFormFieldType.String },
        { label:"Surname", name: "surname", type: DataFormFieldType.String },
        { label:"Age", name: "age", type: DataFormFieldType.Number }
      ]
    }
  ]

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

    <DataForm
      caption={`${employeData.name} ${employeData.surname} (${employeData.code})`}
      data={employeData}
      onSave={handleSaveForm}
      rows={rows}/>
  </div>
}

export default EmployeesPage