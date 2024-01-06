// React
import { AnyAction } from "@reduxjs/toolkit"
import { Dispatch, useCallback, useEffect } from "react"
import { useDispatch, useSelector } from "react-redux"

// Redux
import { RootState } from "../../redux/store"
import AuthService from "../../services/AuthService"
import { setLogged, setUnlogged, setUser } from "../../redux/slices/authSlice"

// Services
import UsersService from "../../services/UsersService"

// Types
import UserDTO from "../../types/DTO/UserDTO"
import IAuthDataInitializerProps from "./types/IMainInitializerProps"

const AuthDataInitializer: React.FC<IAuthDataInitializerProps> = ({ children }: IAuthDataInitializerProps) => {
  const dispatch: Dispatch<AnyAction> = useDispatch()
  const reduxState = useSelector<RootState, RootState>(x => x)

  useEffect(() => {
    // initialize()
  }, [])

  const initialize = useCallback(async () => {
    const authService: AuthService = AuthService.getInstance()
    const usersService: UsersService = UsersService.getInstance()

    const logged: boolean = await authService.checkIsLogged()
    if(logged)
    {
      const user: UserDTO = await usersService.getCurrentAsync()
      dispatch(setLogged())
      dispatch(setUser(user))
    }
    else
    {
      dispatch(setUnlogged())
    }
  }, [reduxState])

  return <>
    {children}
  </>
}

export default AuthDataInitializer