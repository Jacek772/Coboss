import AuthService from "../services/AuthService"
import Api from "./base/Api"

// Config
import config from "../config"

class UsersApi extends Api {
    static baseRoute = "users"

    async getAll(token: string) {
        return await this.get(`${config.API_URL}/`, null, null, token)
    }

}

export default UsersApi