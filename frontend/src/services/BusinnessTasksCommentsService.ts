import BusinnessTasksCommentsApi from "../api/BusinnessTasksCommentsApi";
import CreateBusinnessTaskCommentCommand from "../types/Commands/CreateBusinnessTaskCommentCommand";
import UpdateBusinnessTaskCommentCommand from "../types/Commands/UpdateBusinnessTaskCommentCommand";
import BaseService from "./base/BaseService";

class BusinnessTasksCommentsService extends BaseService {
  private static _instance: BusinnessTasksCommentsService
  private readonly _businnessTasksCommentsApi: BusinnessTasksCommentsApi

  private constructor() 
  { 
    super()
    this._businnessTasksCommentsApi = new BusinnessTasksCommentsApi();
  }

  public async createAsync(command: CreateBusinnessTaskCommentCommand): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._businnessTasksCommentsApi.createAsync(this._tokenService.getToken(), command)
    )
  }

  public async updateAsync(command: UpdateBusinnessTaskCommentCommand): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._businnessTasksCommentsApi.updateAsync(this._tokenService.getToken(), command)
    )
  }

  public async deleteAsync(ids: number[]): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._businnessTasksCommentsApi.deleteAsync(this._tokenService.getToken(), ids)
    )
  } 

  public static getInstance(): BusinnessTasksCommentsService {
    if (!BusinnessTasksCommentsService._instance) {
      BusinnessTasksCommentsService._instance = new BusinnessTasksCommentsService();
    }
    return BusinnessTasksCommentsService._instance;
  }
}

export default BusinnessTasksCommentsService