export class Token {
  accessToken: string;
  refreshToken: string;
  expiresIn: number;
}

export class XsrfToken{
  readonly headerName: string = "X-XSRF-TOKEN";
  value: string;

  constructor(value: string){
    this.value = value;
  }
}
