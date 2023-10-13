// Libraries
import qs from 'qs'

class Api {
  static getBaseUrl() {
    return "https://localhost:7252/api"
}

    static async get(url, query = {}, headers = {}, token = "") {
        if (query && JSON.stringify(query) !== "{}") {
            url += `?${qs.stringify(query)}`
        }

        if (!headers) {
            headers = {}
        }

        if (token) {
            headers.Authorization = `Bearer ${token}`
        }

        let res
        try {
            res = await fetch(url, {
                method: "GET",
                headers: { ...headers, "App-Request": "App-Request" },
            })
        }
        catch (error) {
            throw new Error(`Błąd fetch GET\n${url}\nSkontaktuj się z administratorem`)
        }


        if (!res.ok)
            return {
                ok: res.ok,
                status: res.status,
                message: `Error: ${res.status}`
            }

        let data
        try {
            data = await res.json()
        } catch {
            data = {}
        }

        return {
            ok: res.ok,
            status: res.status,
            message: res.status,
            data
        }
    }

    static async post(url, body = {}, json = true, headers = {}, token = "") {
        if (!headers) {
            headers = {}
        }

        if (token) {
            headers.Authorization = `Bearer ${token}`
        }

        let res
        try {
            res = await fetch(url, {
                headers: { ...headers, "App-Request": "App-Request" },
                method: "POST",
                body: json ? JSON.stringify(body) : body
            })
        }
        catch (error) {
            throw new Error(`Błąd fetch POST\n${url}\nSkontaktuj się z administratorem`)
        }


        let data
        try {
            data = await res.json()
        }
        catch {
            data = {}
        }

        if (!res.ok)
            return {
                ok: res.ok,
                status: res.status,
                message: `Error: ${res.status}`,
                data
            }

        return {
            ok: res.ok,
            status: res.status,
            message: res.status,
            data
        }
    }

    static async put(url, body = {}, json = true, headers = {}, token = "") {
        if (!headers) {
            headers = {}
        }

        if (token) {
            headers.Authorization = `Bearer ${token}`
        }

        let res
        try {
            res = await fetch(url, {
                headers: { ...headers, "App-Request": "App-Request" },
                method: "PUT",
                body: json ? JSON.stringify(body) : body
            })
        }
        catch (error) {
            throw new Error(`Błąd fetch PUT\n${url}\nSkontaktuj się z administratorem`)
        }

        let data
        try {
            data = await res.json()
        }
        catch {
            data = {}
        }

        if (!res.ok)
            return {
                ok: res.ok,
                status: res.status,
                message: `Error: ${res.status}`,
                data
            }

        return {
            ok: res.ok,
            status: res.status,
            message: res.status,
            data
        }
    }

    static async delete(url, body = {}, json = true, headers = {}, token = "") {
        if (!headers) {
            headers = {}
        }

        if (token) {
            headers.Authorization = `Bearer ${token}`
        }

        let res
        try {
            res = await fetch(url, {
                headers: { ...headers, "App-Request": "App-Request" },
                method: "DELETE",
                body: json ? JSON.stringify(body) : body
            })
        }
        catch (error) {
            throw new Error(`Błąd fetch DELETE\n${url}\nSkontaktuj się z administratorem`)
        }

        let data
        try {
            data = await res.json()
        }
        catch {
            data = {}
        }

        if (!res.ok)
            return {
                ok: res.ok,
                status: res.status,
                message: `Error: ${res.status}`,
                data
            }

        return {
            ok: res.ok,
            status: res.status,
            message: res.status,
            data
        }
    }
}

export default Api