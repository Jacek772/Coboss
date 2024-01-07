import ProjectsApi from "../api/ProjectsApi"
import CreateProjectCommand from "../types/Commands/CreateProjectCommand";
import UpdateProjectCommand from "../types/Commands/UpdateProjectCommand";
import ProjectDTO from "../types/DTO/ProjectDTO";
import GetProjectsQuery from "../types/Query/GetProjectsQuery";
import BaseService from "./base/BaseService";

class ProjectsService extends BaseService {
  private static _instance: ProjectsService
  private readonly _projectsApi: ProjectsApi

  private constructor() 
  { 
    super()
    this._projectsApi = new ProjectsApi();
  }

  public async getAllAsync(query?: GetProjectsQuery): Promise<ProjectDTO[]> {
    return await super.executeRequestAsync<ProjectDTO[]>(
      () => this._projectsApi.getAllAsync(this._tokenService.getToken(), query)
    )
  }

  public async createAsync(command: CreateProjectCommand): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._projectsApi.createAsync(this._tokenService.getToken(), command)
    )
  }

  public async updateAsync(command: UpdateProjectCommand): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._projectsApi.updateAsync(this._tokenService.getToken(), command)
    )
  }

  public async deleteAsync(ids: number[]): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._projectsApi.deleteAsync(this._tokenService.getToken(), ids)
    )
  } 

  public static getInstance(): ProjectsService {
    if (!ProjectsService._instance) {
      ProjectsService._instance = new ProjectsService();
    }
    return ProjectsService._instance;
  }
}

export default ProjectsService