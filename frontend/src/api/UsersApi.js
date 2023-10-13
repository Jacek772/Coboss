import Api from "./base/Api"

class UsersApi extends Api {
    static baseRoute = "users"

    static async getAll(token) {
        return await this.get(`${this.getBaseUrl()}/${this.baseRoute}`, null, null, token)
    }

}

export default UsersApi