import AuthApi from "../api/AuthApi";
import ILoginCommand from "../types/Commands/ILoginCommand";

// Types
import IResponse from "../api/types/IResponse";
import ILoginResultDTO from "../types/DTO/ILoginResultDTO";


class AuthService {
  private readonly _authApi: AuthApi
  private static instance: AuthService

  private constructor() 
  { 
    this._authApi = new AuthApi();
  }

  public async login(loginCommand: ILoginCommand): Promise<ILoginResultDTO>
  {
    const response: IResponse = await this._authApi.login(loginCommand)
    return {
      ok: response.ok,
      message: response.data?.message,
      token: response.ok ? response.data.token : "" 
    }
  }

  public static getInstance(): AuthService {
    if (!AuthService.instance) {
      AuthService.instance = new AuthService();
    }
    return AuthService.instance;
  }
}

export default AuthService