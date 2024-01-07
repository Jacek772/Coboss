import Api from "./base/Api"

// Config
import config from "../config"

class UsersApi extends Api {
    static baseRoute = "users"

    public async getCurrent(token: string) {
        return await super.get(`${config.API_URL}/${UsersApi.baseRoute}/current`, null, null, token)
    }
}

export default UsersApi