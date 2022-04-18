import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from "@angular/router";
import {Injectable} from "@angular/core";
import {AuthService} from "./auth.service";

@Injectable({
    providedIn: 'root'
  }
)
export class AuthenticatedGuardService implements CanActivate {
  constructor(private authService: AuthService) {
  }

  public async canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let canActivate = await this.authService.isAuthenticated();

    if(!canActivate){
      this.authService.redirectToLoginPage();
      return canActivate;
    }

    return canActivate;
  }
}


