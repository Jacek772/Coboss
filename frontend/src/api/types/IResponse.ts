interface IResponse {
  ok: boolean,
  tokenExpired: boolean,
  status: number,
  message: string,
  data: any
}

export default IResponse