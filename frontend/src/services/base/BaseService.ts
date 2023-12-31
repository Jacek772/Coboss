import AuthApi from "../../api/AuthApi";
import IResponse from "../../api/types/IResponse";
import IRefreshTokenCommand from "../../types/Commands/IRefreshTokenCommand";
import ILoginResultDTO from "../../types/DTO/ILoginResultDTO";
import TokenService from "../TokenService";

abstract class BaseService {
  private readonly _authApi: AuthApi
  protected readonly _tokenService: TokenService

  protected constructor()
  {
    this._authApi = new AuthApi()
    this._tokenService = TokenService.getInstance()
  }

  public async executeRequestAsync<T>(requestFunction: () => Promise<IResponse>): Promise<T>
  {
    let response: IResponse = await requestFunction()
    while(!response.ok)
    {
      if(response.tokenExpired)
      {
        const refreshResult: boolean = await this.refreshTokenAsync()
        if(refreshResult)
        {
          response = await requestFunction()
          continue
        }
      }
      throw new Error(response.data?.message)
    }

    return response?.data as T
  }

  private async refreshTokenAsync(): Promise<boolean> {
    const refreshTokenCommand: IRefreshTokenCommand = {
      token: this._tokenService.getToken(),
      refreshtoken: this._tokenService.getRefreshToken()
    }

    const response: IResponse = await this._authApi.refreshToken(refreshTokenCommand)
    if(response.ok)
    {
      const data = response.data as ILoginResultDTO
      this._tokenService.setToken(data.token)
      this._tokenService.setRefreshToken(data.refreshToken)
      return true
    }
    return false
  }
}

export default BaseService