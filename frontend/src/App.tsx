import React from "react";
import LoginPage from "./pages/LoginPage";

import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import MainPage from "./pages/MainPage";

const App: React.FC = () => {
  const router: any = createBrowserRouter([
    {
      id:"root",
      path: "/",
      Component: LoginPage,
      // loader()
      // {

      // }
    },
    {
      path: "/main",
      Component: MainPage,
    }
  ]);

  return (
    <RouterProvider 
      router={router}
      fallbackElement={<p>Initial Load...</p>} />
  );
}

export default App;
