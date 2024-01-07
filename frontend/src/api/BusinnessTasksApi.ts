import Api from "./base/Api"

// Config
import config from "../config"

// Types
import GetBusinnessTasksQuery from "../types/Query/GetBusinnessTasksQuery"
import CreateBusinnessTaskCommand from "../types/Commands/CreateBusinnessTaskCommand"
import UpdateBusinnessTaskCommand from "../types/Commands/UpdateBusinnessTaskCommand"

class BusinnessTasksApi extends Api {
  static baseRoute = "tasks"

  public async getAllAsync(token: string, query: GetBusinnessTasksQuery) {
    return await super.get(this.getBasePath(), query, null, token)
  }

  public async createAsync(token: string, command: CreateBusinnessTaskCommand) {
    const headers: any = {
      "Content-Type":"application/json"
    }
    return await super.post(this.getBasePath(), command, true, headers, token)
  }

  public async updateAsync(token: string, command: UpdateBusinnessTaskCommand) {
    const headers: any = {
      "Content-Type":"application/json"
    }
    return await super.put(this.getBasePath(), command, true, headers, token)
  }

  public async deleteAsync(token: string, ids: number[]) {
    const headers: any = {
      "Content-Type":"application/json"
    }
    return await super.delete(`${this.getBasePath()}`, { ids }, true, headers, token) 
  }

  private getBasePath(): string {
    return `${config.API_URL}/${BusinnessTasksApi.baseRoute}`
  }
}

export default BusinnessTasksApi