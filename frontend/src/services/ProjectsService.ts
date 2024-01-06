import ProjectsApi from "../api/ProjectsApi"
import ProjectDTO from "../types/DTO/ProjectDTO";
import BaseService from "./base/BaseService";

class ProjectsService extends BaseService {
  private static instance: ProjectsService
  private readonly _projectsApi: ProjectsApi

  private constructor() 
  { 
    super()
    this._projectsApi = new ProjectsApi();
  }

  public async getProjectsAsync(): Promise<ProjectDTO[]> {
    return await super.executeRequestAsync<ProjectDTO[]>(
      () => this._projectsApi.getProjectsAsync(this._tokenService.getToken())
    )
  }

  public static getInstance(): ProjectsService {
    if (!ProjectsService.instance) {
      ProjectsService.instance = new ProjectsService();
    }
    return ProjectsService.instance;
  }
}

export default ProjectsService