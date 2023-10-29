// Api
import UsersApi from "../api/UsersApi"

// Config
import { config } from "process";
import TokenService from "./TokenService";
import IUserDTO from "../types/DTO/IUserDTO";
import IResponse from "../api/types/IResponse";

class UsersService {
  private static instance: UsersService
  private readonly _usersApi: UsersApi
  private readonly _tokenService: TokenService

  private constructor() 
  { 
    this._usersApi = new UsersApi();
    this._tokenService = TokenService.getInstance()
  }

  async getCurrent(): Promise<IUserDTO> {
    const response: IResponse = await this._usersApi.getCurrent(this._tokenService.getToken())
    if(!response.ok)
    {
      throw new Error(response.data?.message)
    }
    
    return response?.data
  }

  public static getInstance(): UsersService {
    if (!UsersService.instance) {
      UsersService.instance = new UsersService();
    }
    return UsersService.instance;
  }
}

export default UsersService