// Libraries
import React, { Dispatch } from "react"
import { Field, Form, Formik, FormikHelpers } from "formik"
import { NavigateFunction, useNavigate } from "react-router-dom";

// Services
import UsersService from "../../services/UsersService"
import TokenService from "../../services/TokenService";
import AuthService from "../../services/AuthService";

// Validation
import LoginFormValidationSchema from "./validation/LoginFormValidationSchema";

// Actions
import { setUser } from "../../redux/slices/authSlice";

// Types
import ILoginFormValues from "./types/ILoginFormValues";
import ILoginCommand from "../../types/Commands/ILoginCommand";
import ILoginResultDTO from "../../types/DTO/ILoginResultDTO";
import IUserDTO from "../../types/DTO/IUserDTO";

// Css
import "./index.css"
import { useDispatch } from "react-redux";
import { AnyAction } from "@reduxjs/toolkit";

const LoginPage: React.FC = () => {
  const navigate: NavigateFunction = useNavigate()
  const dispatch: Dispatch<AnyAction> = useDispatch()

  const handleSubmit = async (values: ILoginFormValues, formikHelpers: FormikHelpers<ILoginFormValues>): Promise<void> => {
    const authService: AuthService = AuthService.getInstance()
    const tokenService: TokenService = TokenService.getInstance()
    const usersService: UsersService = UsersService.getInstance()

    const loginCommand: ILoginCommand = {
      login: values.login,
      password: values.password
    }

    let loginResult: ILoginResultDTO;
    try
    {
      loginResult = await authService.login(loginCommand)
      if(loginResult.ok)
      {
        tokenService.setToken(loginResult.token)
        const user: IUserDTO = await usersService.getCurrent()
        dispatch(setUser(user))
        navigate("/main")
      }
      else
      {
        formikHelpers.setErrors({ formErrors: loginResult.message })
      }
    }
    catch(error: any)
    {
      console.log(error.message)
    }
  }

  return <div className="loginpage-container">
    <main className="main-conatiner">
      <img src="gfx/cobos_logo_1.png" alt="cobos_logo_1" className="img-logo"/>
    </main>
    <section className="form-login-container">
      <img src="gfx/cobos_logo_2.png" alt="cobos_logo_1" className="form-login-img-logo-mobile"/>
      <h2 className="form-login-hr-h1">Login</h2>
      <hr className="form-login-hr" />

      <Formik
        initialValues={{
          login: "",
          password: "",
          formErrors: "",
          remember: false,
        }}
        validationSchema={LoginFormValidationSchema}
        onSubmit={handleSubmit}
      >
        {({ errors, touched }) => (
          <Form className="form-login">
            <div className="form-item">
              <Field className="input" id="login" name="login" placeholder="login" />
              {errors.login && touched.login ? (
                <div className="form-item-alert form-item-alert-danger">
                {errors.password}
              </div>
              ) : null}
            </div>

            <div className="form-item">
              <Field className="input" type="password" id="password" name="password" placeholder="password" />
              {errors.password && touched.password ? (
                <div className="form-item-alert form-item-alert-danger">
                  {errors.password}
                </div>
              ) : null}
            </div>

            <div className="form-item form-item-row">
              <label className="form-item-label form-item-label-row form-login-item-label" htmlFor="remember">Remember</label>
              <Field className="input input-checkbox" type="checkbox" id="remember" name="remember" />
            </div>

            <div className="form-item">
              <button className="button button-primary" type="submit">Zaloguj</button>
            </div>
            {errors.formErrors ? (
              <div className="form-alert form-alert-danger">
                {errors.formErrors}
              </div>
              ) : null}
          </Form>
        )}
      </Formik>

    </section>
  </div>
}

export default LoginPage