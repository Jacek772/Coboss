class TokenService {
  private static instance: TokenService
  private static readonly TOKEN_KEY_NAME = "TOKEN"

  private constructor() { }

  public setToken(token: string): void {
    localStorage.setItem(TokenService.TOKEN_KEY_NAME, token)
  }

  public getToken(): string {
    return localStorage.getItem(TokenService.TOKEN_KEY_NAME)
  }

  public removeToken(): void {
    localStorage.removeItem(TokenService.TOKEN_KEY_NAME)
  }

  public static getInstance(): TokenService {
    if (!TokenService.instance) {
      TokenService.instance = new TokenService();
    }
    return TokenService.instance;
  }
}

export default TokenService