import config from "../config";
import CreateBusinnessTaskCommentCommand from "../types/Commands/CreateBusinnessTaskCommentCommand";
import UpdateBusinnessTaskCommentCommand from "../types/Commands/UpdateBusinnessTaskCommentCommand";
import Api from "./base/Api";

class BusinnessTasksCommentsApi extends Api {
  static baseRoute = "taskcomments"

  public async createAsync(token: string, command: CreateBusinnessTaskCommentCommand)
  {
    const headers: any = {
      "Content-Type":"application/json"
    }
    return await super.post(this.getBasePath(), command, true, headers, token)
  }

  public async updateAsync(token: string, command: UpdateBusinnessTaskCommentCommand) {
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
    return `${config.API_URL}/${BusinnessTasksCommentsApi.baseRoute}`
  }
}

export default BusinnessTasksCommentsApi