import Api from "./base/Api";

// Config
import config from "../config"

// Types
import ILoginCommand from "../types/Commands/ILoginCommand";

class AuthApi extends Api {
  static baseRoute = "auth"

  public async login(loginCommand: ILoginCommand ){
    const headers: any = {
      "Content-Type":"application/json"
    }
   return this.post(`${config.API_URL}/${AuthApi.baseRoute}/login`, loginCommand, true, headers)
  }
}

export default AuthApi