// React
import { useCallback, useEffect, useRef, useState } from "react"

// Types
import IGridProps from "./types/IGridProps"
import IColDefState from "./types/IColDefState"
import SortDirection from "./types/enums/SortDirection"
import IGridState from "./types/IGridState"
import IRowData from "./types/IRowData"

// Css
import "./index.css"

const Grid: React.FC<IGridProps> = ({ colDefs, rowsData, onRowClick }) => {
  const [state, setState] = useState<IGridState>({
    colDefs: [],
    rowsData: []
  })

  const divHeadRef = useRef<HTMLDivElement>()
  const divBodyRef = useRef<HTMLDivElement>()

  useEffect(() => {
    initializeState()
  }, [colDefs, rowsData])

  const initializeState = useCallback((): void => {
    const colDefsState: IColDefState[] = colDefs.map(x => {
      return { ...x, checked: false, sortDirection: SortDirection.Asc }
    })

    const rowDataState: IRowData[] = rowsData.map((x, index) => {
      return { data: x, checked: false }
    })

    setState({
      ...state,
      colDefs: [...colDefsState],
      rowsData: [...rowDataState]
    })
  }, [colDefs, rowsData])

  const handleClickSort = useCallback((index: number, colDef: IColDefState) => {
    const colDefsState = [...state.colDefs]
    if(colDef.sortDirection === SortDirection.Asc) {
      colDefsState[index].sortDirection = SortDirection.Desc
    }
    else
    {
      colDefsState[index].sortDirection = SortDirection.Asc
    }

    setState({
      ...state,
      colDefs: [...colDefsState]
    })
  }, [state.rowsData])

  const handleChangeCheck = useCallback((index: number, rowData: IRowData) => {
    const rowsDataState = [...state.rowsData]
    rowsDataState[index].checked = !rowsDataState[index].checked
    setState({
      ...state,
      rowsData: [...rowsDataState]
    })
  }, [state])

  const handleClickCheckAll = useCallback((e) => {
    const rowsDataState = [...state.rowsData].map(x => {
      return { ...x, checked: e.target.checked }
    })
    setState({
      ...state,
      rowsData: [...rowsDataState]
    })
  }, [state])

  const handleScrollHead = useCallback((e) => {
    divBodyRef.current.scrollLeft = e.target.scrollLeft
  }, [divBodyRef])

  const handleScrollBody = useCallback((e) => {
    divHeadRef.current.scrollLeft = e.target.scrollLeft
  }, [divHeadRef])

  return  <div className="grid-container">
    <div id="gridHeadContainer" ref={divHeadRef} className="grid-head-container" onScroll={handleScrollHead}>
      <table>
        <thead className="grid-head">
          <tr className="grid-tr">
            <th className="grid-head-th-checkbox">
              <div className="grid-head-th-checkbox-container">
                <input 
                  className="input input-checkbox" 
                  type="checkbox"
                  onChange={handleClickCheckAll}/>
              </div>
            </th>
            {
              state.colDefs.map((colDef, index) => {
                return <th 
                  key={index}>
                    <div className="grid-head-th-container"  style={{ width: colDef.width }}>
                      <p>
                      {colDef.caption}
                      </p>
                      <div onClick={() => handleClickSort(index, colDef)}>
                        {
                          colDef.sortDirection === SortDirection.Asc ?
                          <img src="gfx/svg/arrow-down-black.svg" alt="arrow-down-black" className="grid-head-th-imgarrow" />
                          :
                          <img src="gfx/svg/arrow-up-black.svg" alt="arrow-up-black" className="grid-head-th-imgarrow" />
                        }
                      </div>
                    </div>
                </th>
              })
            }
            <th></th>
          </tr>
        </thead>
      </table>
    </div>
    <div ref={divBodyRef} className="grid-body-container" onScroll={handleScrollBody}>
      <table>
        <tbody>
          {
            state.rowsData.map((rowData, a) => {
              return <tr className="grid-body-tr" key={a} onClick={() => onRowClick ? onRowClick(a, rowData) : () => {}}>
                <td className="grid-body-td-checkbox">
                  <div className="grid-body-td-checkbox-container">
                    <input 
                      className="input input-checkbox" 
                      type="checkbox"
                      onChange={() => handleChangeCheck(a, rowData)}
                      checked={rowData.checked}
                      value={rowData.checked ? 1 : 0} />
                  </div>
                </td>
                {
                  state.colDefs.map((colDef, b) => {
                    return <td key={b} >
                      <div style={{ width: colDef.width }}>
                        {rowData.data[colDef.field]}
                      </div>
                    </td>
                  })  
                }
                <td></td>
              </tr>
            })
          }
        </tbody>
      </table>
    </div>
  </div>
}

export default Grid