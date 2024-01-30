import { useCallback, useState } from "react"
import PageBarProps from "./types/PageBarProps"

import style from "./index.module.css"

const PageBar: React.FC<PageBarProps> = ({ caption, onChangeInput, searchVisible  }) => {
  const [text, setText] = useState("")

  const handleChange = useCallback((e) => {
    setText(e.target.value)
    onChangeInput?.(e.target.value)
  }, [onChangeInput])

  return <div className={style.container}>
      <div className={style.containerRow}>
        <h1 className={style.caption}>{caption}</h1>
        {
          searchVisible ?
          <input 
            className={`input page-caption-input ${style.inputSearch}`} 
            placeholder="Search..."
            value={text}
            onChange={handleChange}
          />
          :
          null
        }
      
      </div>
      <hr/>
  </div>
}

export default PageBar