import { useEffect } from "react"
import { useState } from "react"
import UsersService from "../../services/UsersService"

const LoginPage = () => {
  const [users, setUsers] = useState([])

  useEffect(() => {
    const getData = async () => {
      const users = await UsersService.getAll()
      setUsers(users)
    }

    getData()
  }, [])

  return <div>
    <h1>Login page</h1>
    <ul>
      {
        users.map((x, index) => {
          return <li key={index}>{x.email}; {x.login}; {x.name}; {x.surname}</li>
        })
      }
    </ul>
  </div>
}

export default LoginPage