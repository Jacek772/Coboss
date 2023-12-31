// Libraries
import React, { Dispatch, useCallback, useEffect } from "react"
import { NavigateFunction, useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { AnyAction } from "@reduxjs/toolkit";
import { RootState } from "../../redux/store";

// Services
import UsersService from "../../services/UsersService"
import TokenService from "../../services/TokenService";
import AuthService from "../../services/AuthService";

// Validation
import LoginFormValidationSchema from "./validation/LoginFormValidationSchema";

// Actions
import { setLogged, setUser } from "../../redux/slices/authSlice";
import { setGlobalModalData, setGlobalModalVisibility } from "../../redux/slices/globalModalSlice";

// Types
import ILoginFormValues from "./types/ILoginFormValues";
import ILoginCommand from "../../types/Commands/ILoginCommand";
import ILoginResultDTO from "../../types/DTO/ILoginResultDTO";
import IUserDTO from "../../types/DTO/IUserDTO";
import GlobalModalTypeEnum from "../../components/GlobalModal/types/GlobalModalTypeEnum";
import GlobalModalButtonsTypeEnum from "../../components/GlobalModal/types/GlobalModalButtonsTypeEnum";

// Css
import "./index.css"
import { SubmitHandler, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod"

const LoginPage: React.FC = () => {
  const navigate: NavigateFunction = useNavigate()
  const dispatch: Dispatch<AnyAction> = useDispatch()
  const reduxState: RootState = useSelector<RootState, RootState>(x => x)

  useEffect(() => {
    if(reduxState.auth.logged)
    {
      navigate("/main")
    }

  }, [navigate])

  const {
    register,
    handleSubmit,
    setError,
    formState: { errors },
  } = useForm<ILoginFormValues>({
    resolver: zodResolver(LoginFormValidationSchema)
  })

  const onSubmit: SubmitHandler<ILoginFormValues> = async (data: ILoginFormValues) => {
    const authService: AuthService = AuthService.getInstance()
    const tokenService: TokenService = TokenService.getInstance()
    const usersService: UsersService = UsersService.getInstance()

    const loginCommand: ILoginCommand = {
      email: data.email,
      password: data.password
    }

    try
    {
      const loginResult: ILoginResultDTO = await authService.login(loginCommand)
      if(loginResult.ok)
      {
        tokenService.setToken(loginResult.token)
        tokenService.setRefreshToken(loginResult.refreshToken)
        const user: IUserDTO = await usersService.getCurrentAsync()
        dispatch(setLogged())
        dispatch(setUser(user))
      }
      else
      {
        setError("formErrors", { message: loginResult.message }, { shouldFocus: true })
      }
    }
    catch(error: any)
    {
      const title: string = "Error"
      const text: string = error.message
      showGlobalModal(title, text)
      console.error(error.message)
    }
  }

  const showGlobalModal = useCallback(( title, text) => {
    const globalModalDala = {
      key: "",
      title,
      text,
      modalType: GlobalModalTypeEnum.Danger,
      buttonsType: GlobalModalButtonsTypeEnum.Ok
    }

    dispatch(setGlobalModalData(globalModalDala))
    dispatch(setGlobalModalVisibility(true))
  }, [dispatch])


  return <div className="loginpage-container">
    <main className="main-conatiner">
      <img src="gfx/png/cobos_logo_1.png" alt="cobos_logo_1" className="img-logo"/>
    </main>
    <section className="form-login-container">
      <img src="gfx/png/cobos_logo_2.png" alt="cobos_logo_1" className="form-login-img-logo-mobile"/>
      <h2 className="form-login-hr-h1">Login</h2>
      <hr className="form-login-hr" />

      <form onSubmit={handleSubmit(onSubmit)} className="form-login">
        <div className="form-item">
          <input className="input" type="email" placeholder="email" {...register("email", { required: true })} />
          {errors.email ? (
            <div className="form-item-alert form-item-alert-danger">
            {errors.email.message}
          </div>
          ) : null}
        </div>

        <div className="form-item">
          <input className="input" type="password" placeholder="password" {...register("password", { required: true })} />
          {errors.password ? (
            <div className="form-item-alert form-item-alert-danger">
              {errors.password.message}
            </div>
          ) : null}
        </div>

        <div className="form-item form-item-row">
          <label className="form-item-label form-item-label-row form-login-item-label" htmlFor="remember">Remember</label>
          <input className="input input-checkbox" type="checkbox" {...register("remember")} />
        </div>

        <div className="form-item">
          <button className="button button-primary" type="submit">Zaloguj</button>
        </div>
        {errors.formErrors ? (
          <div className="form-alert form-alert-danger">
            {errors.formErrors.message}
          </div>
          ) : null}
      </form>
    </section>
  </div>
}

export default LoginPage