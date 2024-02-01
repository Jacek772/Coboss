import BusinnessTaskRealisationsApi from "../api/BusinnessTaskRealisationsApi";
import BaseService from "./base/BaseService";

class BusinnessTaskRealisationsService extends BaseService {
  private static instance: BusinnessTaskRealisationsService
  private readonly _businnessTaskRealisationsApi: BusinnessTaskRealisationsApi

  private constructor() 
  { 
    super()
    this._businnessTaskRealisationsApi = new BusinnessTaskRealisationsApi();
  }

  public async deleteAsync(ids: number[]): Promise<void> {
    return await super.executeRequestAsync<void>(
      () => this._businnessTaskRealisationsApi.deleteAsync(this._tokenService.getToken(), ids)
    )
  } 
  
  public static getInstance(): BusinnessTaskRealisationsService {
    if (!BusinnessTaskRealisationsService.instance) {
      BusinnessTaskRealisationsService.instance = new BusinnessTaskRealisationsService();
    }
    return BusinnessTaskRealisationsService.instance;
  }
}

export default BusinnessTaskRealisationsService