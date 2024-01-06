import Api from "./base/Api"

// Config
import config from "../config"

class ProjectsApi extends Api {
  static baseRoute = "projects"

  public async getProjectsAsync(token: string) {
    return await this.get(this.getBasePath(), null, null, token)
  }

  private getBasePath(): string {
    return `${config.API_URL}/${ProjectsApi.baseRoute}`
  }
}

export default ProjectsApi