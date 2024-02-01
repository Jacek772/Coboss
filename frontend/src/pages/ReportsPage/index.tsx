import PageBar from "../../components/PageBar"

// Css
import styles from "./index.module.css"

const ReportsPage: React.FC = () => {
    return <div className={styles.pageContainer}>
    <PageBar
          caption="Summaries and reports"
          searchVisible={false}
    />
  </div>
}

export default ReportsPage