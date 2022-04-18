import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from "@angular/common/http";
import {
  BehaviorSubject,
  Observable,
  throwError
} from "rxjs";
import {catchError, debounceTime, filter, finalize, map} from "rxjs/operators";
import {LoginRequiredService} from "./login-required.service";
import {ActivatedRoute, NavigationEnd, Router} from "@angular/router";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class HttpStatus {
  private requestsPending: any;

  constructor() {
    this.requestsPending = new BehaviorSubject(false);
  }

  setHttpStatus(requestsPending: boolean) {
    this.requestsPending.next(requestsPending);
  }

  getHttpStatus(): Observable<boolean> {
    return this.requestsPending.asObservable().pipe(debounceTime(50));
  }
}

@Injectable({
  providedIn: 'root'
})
export class RequestInterceptorService implements HttpInterceptor {
  private lastRoute = "";
  private requests: HttpRequest<any>[] = [];

  private urlsWithoutSpinner: Array<string> = [
    "/Auth/is-authenticated"];

  constructor(private loginReqService: LoginRequiredService,private status: HttpStatus, private router: Router, private activatedRoute: ActivatedRoute) {
    this.router.events.pipe(
      filter(evt => evt instanceof NavigationEnd),
      map(() => {
        let child = this.activatedRoute.firstChild;
        while (child) {
          if (child.firstChild) {
            child = child.firstChild;
          } else if (child.snapshot.data && child.snapshot.data['title']) {
            return child.snapshot.data["title"];
          } else {
            return "";
          }
        }

        return "";
      })
    ).subscribe(next => {
      this.lastRoute = next
    });
  }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.addRequest(req);
    this.status.setHttpStatus(this.requests.length > 0);

    return next.handle(this.addParams(req))
      .pipe(catchError((error: any) => {
        console.debug(error);
        if (error instanceof HttpErrorResponse) {
          if (error.status === 401) {
            this.loginReqService.emit();
          }
        }

        return throwError(error);
      }),
      finalize(() => {
        this.removeRequest(req);
      }))
  }

  private addRequest(req: HttpRequest<any>){
    for(let noSpinnerUrl of this.urlsWithoutSpinner){
      if(req.url.includes(noSpinnerUrl)){
        return;
      }
    }

      this.requests.push(req);
  }

  private addParams(req: HttpRequest<any>): HttpRequest<any> {
    return req.clone({
      withCredentials: true,
      setHeaders: {
        "Cache-control": "no-cache",
        "Expires": "0",
        "Pragma": "no-cache",
        "X-LastRoute": encodeURIComponent(this.lastRoute)
      }
    });
  }

  private removeRequest(req: HttpRequest<any>) {
    const i = this.requests.indexOf(req);
    this.requests.splice(i, 1);
    this.status.setHttpStatus(this.requests.length > 0);
  }
}
