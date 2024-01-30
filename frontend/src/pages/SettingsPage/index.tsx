import { useForm } from "react-hook-form"
import PageBar from "../../components/PageBar"

// Css
import styles from "./index.module.css"

const SettingsPage: React.FC = () => {
  const {
    register,
    handleSubmit,
    setError,
  } = useForm({})


  return <div className={styles.pageContainer}>
    <PageBar
          caption="Settings page"
          searchVisible={false}
    />
    {/* <div style={{ width: 200 }}>
      <form>
      <div className="form-item">
        <input className="password" type="email" placeholder="email" {...register("email", { required: true })} />
      </div>
      </form>
    </div> */}
  </div>
  
  
  
}

export default SettingsPage