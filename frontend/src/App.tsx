// React
import React, { Dispatch, useCallback } from "react";
import {
  createBrowserRouter,
  redirect,
  RouterProvider,
} from "react-router-dom";
import { RootState } from "./redux/store";
import { useDispatch, useSelector } from "react-redux";

// Pages
import LoginPage from "./pages/LoginPage";
import MainPage from "./pages/MainPage";
import EmployeesPage from "./pages/EmployeesPage";
import ProjectsPage from "./pages/ProjectsPage";
import TasksPage from "./pages/TasksPage";
import ReportsPage from "./pages/ReportsPage";
import SettingsPage from "./pages/SettingsPage";
import TokenService from "./services/TokenService";
import { AnyAction } from "@reduxjs/toolkit";
import { setUnlogged } from "./redux/slices/authSlice";
import MainLayout from "./layouts/MainLayout";

const App: React.FC = () => {
  const dispatch: Dispatch<AnyAction> = useDispatch()
  const reduxState = useSelector<RootState, RootState>(x => x)

  const authLoader = useCallback(() => {
    // if(!reduxState.auth.logged)
    // {
    //   return redirect("/")
    // }
    return null 
  }, [reduxState.auth])


  const logoutLoader = useCallback(() => {
    const tokenService: TokenService = TokenService.getInstance()
    tokenService.removeToken()
    dispatch(setUnlogged())
    return redirect("/")
  }, [dispatch])

  const router: any = createBrowserRouter([
    {
      path: "/",
      Component: LoginPage
    },
    {
      Component: MainLayout,
      children:[
        {
          path: "/main",
          Component: MainPage,
          loader: authLoader
        },
        {
          path: "/employees",
          Component: EmployeesPage,
          loader: authLoader
        },
        {
          path: "/projects",
          Component: ProjectsPage,
          loader: authLoader
        },
        {
          path: "/tasks",
          Component: TasksPage,
          loader: authLoader
        },
        {
          path: "/reports",
          Component: ReportsPage,
          loader: authLoader
        },
        {
          path: "/settings",
          Component: SettingsPage,
          loader: authLoader
        },
        {
          path: "/logout",
          loader: logoutLoader
        }
      ]
    }
  ]);

  return (
    <RouterProvider 
      router={router}
      fallbackElement={<p>Initial Load...</p>} />
  );
}

export default App;
