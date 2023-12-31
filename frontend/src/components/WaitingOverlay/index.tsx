import { useEffect, useRef, useState } from "react"
import styles from "./styles.module.css"

export interface IWaitingOverlayState {
  dots: string
} 

const WaitingOverlay: React.FC = () => {
  const [state, setState] = useState<IWaitingOverlayState>({ dots: "." })

  useEffect(() => {
    const interval: NodeJS.Timeout = setInterval(() => {

      setState(s => {
        let dots: string = s.dots
        if(dots.length === 3)
        {
          dots = "."
        }
        else
        {
          dots += "."
        }
        return {...s, dots }
      })
    }, 1000)


    return () => clearInterval(interval)
  })

  return <div className={styles.container}>
    <p className={styles.waitingText}>Please wait{state.dots}</p>
  </div>
}

export default WaitingOverlay