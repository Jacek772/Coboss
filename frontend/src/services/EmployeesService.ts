// Api
import EmployeesApi from "../api/EmployeesApi"

// Types
import CreateEmployeeCommand from "../types/Commands/CreateEmployeeCommand";
import UpdateEmployeeCommand from "../types/Commands/UpdateEmployeeCommand";
import EmployeeDTO from "../types/DTO/EmployeeDTO";
import IGetEmployeesQuery from "../types/Query/IGetEmployeesQuery";
import BaseService from "./base/BaseService";

class EmployeesService extends BaseService {
  private static instance: EmployeesService
  private readonly _employeesApi: EmployeesApi

  private constructor()
  { 
    super()
    this._employeesApi = new EmployeesApi();
  }

  public async getEmployeesAsync(query?: IGetEmployeesQuery): Promise<EmployeeDTO[]>{
    return await super.executeRequestAsync<EmployeeDTO[]>(
      () => this._employeesApi.getEmployeesAsync(this._tokenService.getToken(), query)
    )
  }

  public async deleteEmployeeAsync(id: number): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._employeesApi.deleteEmployeeAsync(this._tokenService.getToken(), id)
    )
  }

  public async deleteEmployeesAsync(ids: number[]): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._employeesApi.deleteEmployeesAsync(this._tokenService.getToken(), ids)
    )
  }

  public async createEmployeesAsync(createEmployeeCommand: CreateEmployeeCommand): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._employeesApi.createEmployeesAsync(this._tokenService.getToken(), createEmployeeCommand)
    )
  }

  public async updateEmployeesAsync(updateEmployeeCommand: UpdateEmployeeCommand): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._employeesApi.updateEmployeesAsync(this._tokenService.getToken(), updateEmployeeCommand)
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