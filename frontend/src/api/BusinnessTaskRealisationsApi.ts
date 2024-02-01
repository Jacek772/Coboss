import Api from "./base/Api"

// Config
import config from "../config"

class BusinnessTaskRealisationsApi extends Api {
  static baseRoute = "taskrealisations"

  public async deleteAsync(token: string, ids: number[]) {
    const headers: any = {
      "Content-Type":"application/json"
    }
    return await super.delete(`${this.getBasePath()}`, { ids }, true, headers, token) 
  }

  private getBasePath(): string {
    return `${config.API_URL}/${BusinnessTaskRealisationsApi.baseRoute}`
  }
}

export default BusinnessTaskRealisationsApi