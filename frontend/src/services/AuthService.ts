import AuthApi from "../api/AuthApi";
import ILoginCommand from "../types/Commands/ILoginCommand";

// Types
import IResponse from "../api/types/IResponse";
import LoginResultDTO from "../types/DTO/LoginResultDTO";
import TokenService from "./TokenService";


class AuthService {
  private static instance: AuthService
  private readonly _authApi: AuthApi

  private constructor() 
  { 
    this._authApi = new AuthApi();
  }

  public async login(loginCommand: ILoginCommand): Promise<LoginResultDTO>
  {
    const response: IResponse = await this._authApi.login(loginCommand)
    return {
      ok: response.ok,
      message: response.data?.message,
      token: response.ok ? response.data.token : "",
      refreshToken: response.ok ? response.data.refreshToken : ""
    }
  }

  public async checkIsLogged(): Promise<boolean>
  {
    const tokenService: TokenService = await TokenService.getInstance()
    const response: IResponse = await this._authApi.checkIsLogged(tokenService.getToken())
    return response.ok
  }

  public static getInstance(): AuthService {
    if (!AuthService.instance) {
      AuthService.instance = new AuthService();
    }
    return AuthService.instance;
  }
}

export default AuthService