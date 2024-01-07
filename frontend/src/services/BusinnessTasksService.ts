// Api
import BusinnessTasksApi from "../api/BusinnessTasksApi";

// Services
import BaseService from "./base/BaseService";

// Types
import CreateBusinnessTaskCommand from "../types/Commands/CreateBusinnessTaskCommand";
import UpdateBusinnessTaskCommand from "../types/Commands/UpdateBusinnessTaskCommand";
import BusinnessTaskDTO from "../types/DTO/BusinnessTaskDTO";
import GetBusinnessTasksQuery from "../types/Query/GetBusinnessTasksQuery";

class BusinnessTasksService extends BaseService {
  private static _instance: BusinnessTasksService
  private readonly _businnessTasksApi: BusinnessTasksApi

  private constructor() 
  { 
    super()
    this._businnessTasksApi = new BusinnessTasksApi();
  }

  public async getAllAsync(query: GetBusinnessTasksQuery): Promise<BusinnessTaskDTO[]> {
    return await super.executeRequestAsync<BusinnessTaskDTO[]>(
      () => this._businnessTasksApi.getAllAsync(this._tokenService.getToken(), query)
    )
  }

  public async createAsync(command: CreateBusinnessTaskCommand): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._businnessTasksApi.createAsync(this._tokenService.getToken(), command)
    )
  }

  public async updateAsync(command: UpdateBusinnessTaskCommand): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._businnessTasksApi.updateAsync(this._tokenService.getToken(), command)
    )
  }

  public async deleteAsync(ids: number[]): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._businnessTasksApi.deleteAsync(this._tokenService.getToken(), ids)
    )
  } 

  public static getInstance(): BusinnessTasksService {
    if (!BusinnessTasksService._instance) {
      BusinnessTasksService._instance = new BusinnessTasksService();
    }
    return BusinnessTasksService._instance;
  }
}

export default BusinnessTasksService