import {Component, OnInit} from "@angular/core";
import {AuthService} from "../../shared-module/services/auth.service";


@Component({
  selector: "layout-app",
  templateUrl: "./layout-app.component.html"
})
export class LayoutAppComponent implements OnInit {
  public userName: string;
  public active = false;
  public collapsed: boolean[];

  constructor(private authService: AuthService) {
    this.userName = "";
    this.collapsed = [true, true, true, true, true];
  }

  public ngOnInit(): void {
  }

  public logout(){
    this.authService.logout();
  }

  public toggleMenu() {
    this.active = !this.active;
  }

  public toogleCollapsed(idx: number) {
    if (this.collapsed[idx]) {
      for (let i = 0; i < this.collapsed.length; i++) {
        this.collapsed[i] = true;
      }
    }

    this.collapsed[idx] = !this.collapsed[idx];
  }

}
