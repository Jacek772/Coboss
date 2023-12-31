import Api from "./base/Api"

// Config
import config from "../config"
import IGetEmployeesQuery from "../types/Query/IGetEmployeesQuery"
import CreateEmployeeCommand from "../types/Commands/CreateEmployeeCommand"
import UpdateEmployeeCommand from "../types/Commands/UpdateEmployeeCommand"

class EmployeesApi extends Api {
  static baseRoute = "employees"

  public async getEmployeesAsync(token: string, query?: IGetEmployeesQuery) {
      return await this.get(this.getBasePath(), query, null, token)
  }

  public async deleteEmployeeAsync(token: string, id: number) {
    return await this.delete(`${this.getBasePath()}/one/${id}`, null, false, null, token)
  }

  public async deleteEmployeesAsync(token: string, ids: number[]) {
    const headers: any = {
      "Content-Type":"application/json"
    }
    return await this.delete(`${this.getBasePath()}/many`, { ids }, true, headers, token)
  }

  public async createEmployeesAsync(token: string, createEmployeeCommand: CreateEmployeeCommand) {
    const headers: any = {
      "Content-Type":"application/json"
    }
    return await this.post(this.getBasePath(), createEmployeeCommand, true, headers, token)
  }

  public async updateEmployeesAsync(token: string, updateEmployeeCommand: UpdateEmployeeCommand) {
    const headers: any = {
      "Content-Type":"application/json"
    }
    return await this.put(this.getBasePath(), updateEmployeeCommand, true, headers, token)
  }

  private getBasePath(): string {
    return `${config.API_URL}/${EmployeesApi.baseRoute}`
  }

}

export default EmployeesApi