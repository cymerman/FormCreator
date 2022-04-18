import {AfterViewInit, Component, ElementRef, Input, OnInit, QueryList, ViewChild, ViewChildren} from "@angular/core";
import {FormSettings} from "../../_models/form-settings.model";
import {FormRendererPageComponent} from "../form-renderer-page-component/form-renderer-page.component";

@Component({
  selector: "form-renderer",
  templateUrl: "form-renderer.component.html",
})
export class FormRendererComponent implements OnInit, AfterViewInit {

  @Input()
  public formSettings: FormSettings = new FormSettings;

  @Input()
  public formValues: object | undefined;

  @ViewChildren(FormRendererPageComponent)
  public formPages: QueryList<FormRendererPageComponent> | undefined;

  @ViewChild("mainDiv")
  public mainDiv: ElementRef | undefined;

  public currentPage: number = 1;
  public lastPage: number = 1;

  constructor() {
  }

  public ngOnInit(): void {
    if (this.formSettings) {
      this.lastPage = this.formSettings.pages.length;
    }
  }

  public ngAfterViewInit(): void {
    const style = document.createElement('style');
    style.type = 'text/css';
    // style.appendChild(document.createTextNode(this.styles));
    // this.mainDiv.nativeElement.appendChild(style);
  }

  public getFormValues() {
    let resultModel = {};
    for (let formpage of this.formPages.toArray()) {
      Object.assign(resultModel, formpage.getFormValues());
    }

    return resultModel;
  }

  public changePage(val:any) {
    if (val > 0 && !this.isCurrentPageFormValid()) {
      return;
    }

    this.currentPage += val;
    if (this.currentPage > this.lastPage) {
      this.currentPage = this.lastPage;
    }

    if (this.currentPage < 1) {
      this.currentPage = 1;
    }
  }

  public isCurrentPageFormValid() {
    const currentPageComponent = this.formPages.toArray()[this.currentPage - 1];

    return currentPageComponent.validateForm();
  }

  public areAllPageFormsValid() {
    for (let formpage of this.formPages.toArray()) {
      if (!formpage.isFormValid()) {
        return false;
      }
    }

    return true;
  }

  public reloadFormValues(formData:any) {
    this.formValues = formData;
    for (let formpage of this.formPages.toArray()) {
      formpage.reloadFormValues(formData);
    }
  }

}
