import {Component} from '@angular/core';
import {HttpStatus} from "./shared-module/services/request.interceptor";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent{
  public isLoading: boolean = true;

  constructor(private httpStatus: HttpStatus) {
    this.httpStatus.getHttpStatus().subscribe((status: boolean) => {
      this.isLoading = status;
    });
  }
}
