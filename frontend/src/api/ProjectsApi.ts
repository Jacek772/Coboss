import Api from "./base/Api"

// Config
import config from "../config"

// Types
import GetProjectsQuery from "../types/Query/GetProjectsQuery"
import CreateProjectCommand from "../types/Commands/CreateProjectCommand"
import UpdateProjectCommand from "../types/Commands/UpdateProjectCommand"

class ProjectsApi extends Api {
  static baseRoute = "projects"

  public async getAllAsync(token: string, query?: GetProjectsQuery) {
    return await super.get(this.getBasePath(), query, null, token)
  }

  public async createAsync(token: string, command: CreateProjectCommand) {
    const headers: any = {
      "Content-Type":"application/json"
    }
    return await super.post(this.getBasePath(), command, true, headers, token)
  }

  public async updateAsync(token: string, command: UpdateProjectCommand) {
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
    return `${config.API_URL}/${ProjectsApi.baseRoute}`
  }
}

export default ProjectsApi