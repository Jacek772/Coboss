// Api
import EmployeesApi from "../api/EmployeesApi"

// Types
import CreateEmployeeCommand from "../types/Commands/CreateEmployeeCommand";
import UpdateEmployeeCommand from "../types/Commands/UpdateEmployeeCommand";
import EmployeeDTO from "../types/DTO/EmployeeDTO";
import GetEmployeesQuery from "../types/Query/GetEmployeesQuery";
import BaseService from "./base/BaseService";

class EmployeesService extends BaseService {
  private static instance: EmployeesService
  private readonly _employeesApi: EmployeesApi

  private constructor()
  { 
    super()
    this._employeesApi = new EmployeesApi();
  }

  public async getAllAsync(query?: GetEmployeesQuery): Promise<EmployeeDTO[]>{
    return await super.executeRequestAsync<EmployeeDTO[]>(
      () => this._employeesApi.getAsync(this._tokenService.getToken(), query)
    )
  }

  public async createAsync(createEmployeeCommand: CreateEmployeeCommand): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._employeesApi.createAsync(this._tokenService.getToken(), createEmployeeCommand)
    )
  }

  public async updateAsync(updateEmployeeCommand: UpdateEmployeeCommand): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._employeesApi.updateAsync(this._tokenService.getToken(), updateEmployeeCommand)
    )
  }
  
  public async deleteOneAsync(id: number): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._employeesApi.deleteOneAsync(this._tokenService.getToken(), id)
    )
  }

  public async deleteAsync(ids: number[]): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._employeesApi.deleteAsync(this._tokenService.getToken(), ids)
    )
  }

  public static getInstance(): EmployeesService {
    if (!EmployeesService.instance) {
      EmployeesService.instance = new EmployeesService();
    }
    return EmployeesService.instance;
  }
}

export default EmployeesService