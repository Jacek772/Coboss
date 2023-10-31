import Api from "./base/Api";

// Config
import config from "../config"

// Types
import ILoginCommand from "../types/Commands/ILoginCommand";
import IResponse from "./types/IResponse";

class AuthApi extends Api {
  static baseRoute = "auth"

  public async login(loginCommand: ILoginCommand): Promise<IResponse>{
    const headers: any = {
      "Content-Type":"application/json"
    }
   return this.post(`${config.API_URL}/${AuthApi.baseRoute}/login`, loginCommand, true, headers)
  }

  public async checkIsLogged(token: string): Promise<IResponse> {
    return this.get(`${config.API_URL}/${AuthApi.baseRoute}/logged`, null, null, token)
  }
}

export default AuthApi