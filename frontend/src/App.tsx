import React from "react";
import LoginPage from "./pages/LoginPage";

import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";

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

    }
  ]);

  return (
    <RouterProvider 
      router={router}
      fallbackElement={<p>Initial Load...</p>} />
  );
}

export default App;
