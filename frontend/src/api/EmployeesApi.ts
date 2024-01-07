import Api from "./base/Api"

// Config
import config from "../config"

// Types
import GetEmployeesQuery from "../types/Query/GetEmployeesQuery"
import CreateEmployeeCommand from "../types/Commands/CreateEmployeeCommand"
import UpdateEmployeeCommand from "../types/Commands/UpdateEmployeeCommand"

class EmployeesApi extends Api {
  static baseRoute = "employees"

  public async getAsync(token: string, query?: GetEmployeesQuery) {
      return await super.get(this.getBasePath(), query, null, token)
  }

  public async createAsync(token: string, createEmployeeCommand: CreateEmployeeCommand) {
    const headers: any = {
      "Content-Type":"application/json"
    }
    return await super.post(this.getBasePath(), createEmployeeCommand, true, headers, token)
  }

  public async updateAsync(token: string, updateEmployeeCommand: UpdateEmployeeCommand) {
    const headers: any = {
      "Content-Type":"application/json"
    }
    return await super.put(this.getBasePath(), updateEmployeeCommand, true, headers, token)
  }
  
  public async deleteOneAsync(token: string, id: number) {
    return await super.delete(`${this.getBasePath()}/one/${id}`, null, false, null, token)
  }

  public async deleteAsync(token: string, ids: number[]) {
    const headers: any = {
      "Content-Type":"application/json"
    }
    return await super.delete(`${this.getBasePath()}/many`, { ids }, true, headers, token)
  }

  private getBasePath(): string {
    return `${config.API_URL}/${EmployeesApi.baseRoute}`
  }

}

export default EmployeesApi