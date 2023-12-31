// Api
import UsersApi from "../api/UsersApi"

// Config
import IUserDTO from "../types/DTO/IUserDTO";
import BaseService from "./base/BaseService";

class UsersService extends BaseService {
  private static instance: UsersService
  private readonly _usersApi: UsersApi

  private constructor() 
  { 
    super()
    this._usersApi = new UsersApi();
  }

  async getCurrentAsync(): Promise<IUserDTO> {
    return await super.executeRequestAsync<IUserDTO>(
      () => this._usersApi.getCurrent(this._tokenService.getToken())
    )
  }

  public static getInstance(): UsersService {
    if (!UsersService.instance) {
      UsersService.instance = new UsersService();
    }
    return UsersService.instance;
  }
}

export default UsersService