import {Injectable, OnDestroy} from "@angular/core";
import {Observable, ReplaySubject} from "rxjs";
import {Router} from "@angular/router";
import {AuthHttpService} from "./auth-http.service";

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {
  public signInSuccess: ReplaySubject<{}>;
  public logInRequired: ReplaySubject<{}>;
  private checkAuthInterval: any;

  constructor(private router: Router,
              private authHttpService: AuthHttpService) {
    this.signInSuccess = new ReplaySubject<{}>(1);
    this.logInRequired = new ReplaySubject<{}>(1);

    this.checkAuthInterval = setInterval(async () => {

      if (!await this.isAuthenticated()) {
        this.redirectToLoginPage();
      }
    }, 60000);
  }

  public ngOnDestroy() {
    if (this.checkAuthInterval) {
      clearInterval(this.checkAuthInterval);
    }
  }

  public loginRequired() {
    this.logInRequired.next({});
  }

  public redirectToLoginPage() {
    this.router.navigate(["/"]);
  }

  public redirectToAfterLoginPage() {
    this.router.navigate(["/dashboard"]);
  }

  public redirectToAfterLogoutPage() {
    this.router.navigate(["/"]);
  }

  public signIn(login: string, password: string): Observable<{}> {
    return Observable.create((observer: { next: () => void; complete: () => void; error: () => void; }) => {
      this.authHttpService.signIn(login, password)
        .subscribe(token => {
            observer.next();
            observer.complete();
            this.signInSuccess.next({});
          },
          error => {
            observer.error();
          });
    });
  }

  public async isAuthenticated():Promise<boolean> {
    let result:boolean
    try{
      result = await this.authHttpService.isAuthenticated();
    }
    catch (err){
      result = false;
    }

    return result;
  }

  public logout() {
    this.authHttpService.logout().subscribe(x => {
      localStorage.clear();
      this.redirectToAfterLogoutPage();
    });
  }
}
