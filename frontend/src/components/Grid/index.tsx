// React
import { useCallback, useEffect, useRef, useState } from "react"

// Types
import IGridProps from "./types/IGridProps"
import ColDefState from "./types/ColDefState"
import SortDirectionEnum from "./types/enums/SortDirectionEnum"
import IGridState from "./types/IGridState"
import IRowData from "./types/IRowData"

// Css
import "./index.css"
import DateUtils from "../../utils/DateUtils"
import GridColTypeEnum from "./types/enums/GridColTypeEnum"
import ObjectUtils from "../../utils/ObjectUtils"

const Grid: React.FC<IGridProps> = ({ colDefs, rowsData, onRowClick, onRowDoubleClick, onScrollEnd, onSelectionChanged, onSortChanged }) => {
  const [state, setState] = useState<IGridState>({
    colDefs: [],
    rowsData: []
  })

  const divHeadRef = useRef<HTMLDivElement>()
  const divBodyRef = useRef<HTMLDivElement>()

  useEffect(() => {
    const initializeState = () => {
      const rowDataState: IRowData[] = rowsData?.map((x, index) => {
        return { data: x, checked: false }
      }) ?? []
  
      setState(s => ({
        ...s,
        rowsData: [...rowDataState]
      }))
    }

    initializeState()
  }, [rowsData])

  useEffect(() => {
    const initializeState = () => {
      const colDefsState: ColDefState[] = colDefs?.map(x => {
        return { ...x, checked: false, sortDirection: SortDirectionEnum.Asc }
      }) ?? []
  
      setState(s => ({
        ...s,
        colDefs: [...colDefsState],
      }))
    }

    initializeState()
  }, [colDefs])


  const handleClickSort = useCallback((index: number, colDef: ColDefState) => {
    setState(s =>{
      const colDefsState = [...s.colDefs]
      if(colDef.sortDirection === SortDirectionEnum.Asc) {
        colDefsState[index].sortDirection = SortDirectionEnum.Desc
        onSortChanged?.(colDef.field, SortDirectionEnum.Desc)
      }
      else
      {
        colDefsState[index].sortDirection = SortDirectionEnum.Asc
        onSortChanged?.(colDef.field, SortDirectionEnum.Asc)
      }

      return {
        ...s,
        colDefs: [...colDefsState]
      }
    })
  }, [onSortChanged])

  const handleChangeCheck = useCallback((index: number, rowData: IRowData) => {
    const rowsDataState: IRowData[] = [...state.rowsData]
    rowsDataState[index].checked = !rowsDataState[index].checked

    const selectedRows: IRowData[] = rowsDataState.filter(x => x.checked)
    onSelectionChanged?.(selectedRows)

    setState({
        ...state,
        rowsData: [...rowsDataState]
    })
  }, [onSelectionChanged, state])

  const handleClickCheckAll = useCallback((e) => {
    const rowsDataState = [...state.rowsData].map(x => {
      return { ...x, checked: e.target.checked }
    })

    if(e.target.checked)
    {
      onSelectionChanged?.(rowsDataState)
    }
    else
    {
      onSelectionChanged?.([])
    }

    setState({
      ...state,
      rowsData: [...rowsDataState]
    })
  }, [onSelectionChanged, state])

  const handleScrollHead = useCallback((e: React.UIEvent<HTMLDivElement, UIEvent>) => {
    const div: HTMLDivElement = e.target as HTMLDivElement
    divBodyRef.current.scrollLeft = div.scrollLeft
  }, [divBodyRef])

  const handleScrollBody = useCallback((e: React.UIEvent<HTMLDivElement, UIEvent>) => {
    const div: HTMLDivElement = e.target as HTMLDivElement
    divHeadRef.current.scrollLeft = div.scrollLeft
    const maxScrollTopHeight: number = divBodyRef.current.scrollHeight - divBodyRef.current.clientHeight
    if(divBodyRef.current.scrollTop >= maxScrollTopHeight)
    {
      const lastRowData = state.rowsData[state.rowsData.length - 1]
      onScrollEnd?.(lastRowData)
    }
  }, [onScrollEnd, state.rowsData])

  const touchTimeRef: React.MutableRefObject<{ time: number, key: string }> = useRef<{ time: number, key: string }>({ time: 0, key: "" })

  const handleRowClick = useCallback((event: React.MouseEvent<HTMLTableRowElement, MouseEvent>, index: number, rowData:IRowData) => {
    const input: HTMLInputElement = event.target as HTMLInputElement
    if(input?.type === "checkbox")
    {
      return
    }

    const key: string = index.toString()

    if(touchTimeRef.current.time === 0)
    {
      touchTimeRef.current.key = key
      touchTimeRef.current.time = new Date().getTime()
    }
    else {
      if (((new Date().getTime()) - touchTimeRef.current.time) < 800 && touchTimeRef.current.key === key) {
          onRowDoubleClick?.(index, rowData)
          touchTimeRef.current.key = key
          touchTimeRef.current.time = 0;
      } else {
        touchTimeRef.current.key = key
        touchTimeRef.current.time = new Date().getTime();
      }
    }

    onRowClick?.(index, rowData)
  }, [onRowClick])

  const getFieldData = useCallback((colDef: ColDefState, rowData: IRowData) => {
    const value: string = ObjectUtils.getValueByPath(rowData.data, colDef.field)
    switch(colDef.type)
    {
      case GridColTypeEnum.Date:
        return DateUtils.parse(value)?.toLocaleDateString()
      case GridColTypeEnum.Number:
      case GridColTypeEnum.String:
      default:
        return value
    }
  }, [])

  const handleRowDoubleClick = useCallback((event: React.MouseEvent<HTMLTableRowElement, MouseEvent>, index: number, rowData:IRowData) => {
    const input: HTMLInputElement = event.target as HTMLInputElement
    if(input?.type === "checkbox")
    {
      return
    }

    onRowDoubleClick?.(index, rowData)
  }, [onRowDoubleClick])

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
                          colDef.sortDirection === SortDirectionEnum.Asc ?
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
              return <tr className="grid-body-tr" 
                key={a}
                onClick={(e) => handleRowClick(e, a, rowData)}
                // onDoubleClick={(e) => handleRowDoubleClick(e, a, rowData)}
                >
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
                        {getFieldData(colDef, rowData)}
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