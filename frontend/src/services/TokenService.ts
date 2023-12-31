class TokenService {
  private static instance: TokenService
  private static readonly TOKEN_KEY_NAME = "COBOSS_TOKEN"
  private static readonly REFRESH_TOKEN_KEY_NAME = "COBOSS_REFRESH_TOKEN"

  private constructor() { }

  public setToken(token: string): void {
    localStorage.setItem(TokenService.TOKEN_KEY_NAME, token)
  }

  public setRefreshToken(token: string): void {
    localStorage.setItem(TokenService.REFRESH_TOKEN_KEY_NAME, token)
  }

  public getToken(): string {
    return localStorage.getItem(TokenService.TOKEN_KEY_NAME)
  }

  public getRefreshToken(): string {
    return localStorage.getItem(TokenService.REFRESH_TOKEN_KEY_NAME)
  }

  public removeToken(): void {
    localStorage.removeItem(TokenService.TOKEN_KEY_NAME)
  }

  public removeRefreshToken(): void {
    localStorage.removeItem(TokenService.REFRESH_TOKEN_KEY_NAME)
  }

  public static getInstance(): TokenService {
    if (!TokenService.instance) {
      TokenService.instance = new TokenService();
    }
    return TokenService.instance;
  }
}

export default TokenService