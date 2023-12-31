// Libraries
import qs from 'qs'
import IResponse from '../types/IResponse'

abstract class Api {
    protected async get(url: string, query: any = {}, headers: any = {}, token:string = ""): Promise<IResponse>  {
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

        let data
        try {
            data = await res.json()
        } catch {
            data = {}
        }

        if (!res.ok)
        {
            return {
                ok: res.ok,
                tokenExpired: this.isTokenExpired(res),
                status: res.status,
                message: `Error: ${res.status}`,
                data
            }
        }

        return {
            ok: res.ok,
            tokenExpired: false,
            status: res.status,
            message: res.status,
            data
        }
    }

    protected async post(url:string, body:any = {}, json: boolean = true, headers: any = {}, token:string = ""): Promise<IResponse>  {
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
        {
            return {
                ok: res.ok,
                tokenExpired: this.isTokenExpired(res),
                status: res.status,
                message: `Error: ${res.status}`,
                data
            }
        }

        return {
            ok: res.ok,
            tokenExpired: false,
            status: res.status,
            message: res.status,
            data
        }
    }

    protected async put(url:string, body:any = {}, json:boolean = true, headers:any = {}, token:string = ""): Promise<IResponse>  {
        if (!headers) {
            headers = {}
        }

        if (token) {
            headers.Authorization = `Bearer ${token}`
        }

        let res: Response
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
        {
            return {
                ok: res.ok,
                tokenExpired: this.isTokenExpired(res),
                status: res.status,
                message: `Error: ${res.status}`,
                data
            }
        }

        return {
            ok: res.ok,
            tokenExpired: false,
            status: res.status,
            message: res.status.toString(),
            data
        }
    }

    protected async delete(url: string, body: any = {}, json: boolean = true, headers: any = {}, token: string = ""): Promise<IResponse> {
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
        {
            return {
                ok: res.ok,
                tokenExpired: this.isTokenExpired(res),
                status: res.status,
                message: `Error: ${res.status}`,
                data
            }
        }

        return {
            ok: res.ok,
            tokenExpired: false,
            status: res.status,
            message: res.status,
            data
        }
    }

    private isTokenExpired(res: Response): boolean
    {
        const tokenExpiredHeader: string = res.headers.get("Token-Expired")
        return tokenExpiredHeader?.toLowerCase() === "true" ? true : false
    }
}

export default Api