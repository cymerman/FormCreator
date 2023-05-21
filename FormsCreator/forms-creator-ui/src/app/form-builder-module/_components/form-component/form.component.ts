import {AfterViewInit, Component, OnInit, QueryList, ViewChildren} from "@angular/core";
import {FormHttpService} from "../../../shared-module/services/form-http.service";
import {ActivatedRoute} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {FormRendererComponent} from "../form-renderer-component/form-renderer.component";

@Component({
  selector: "form-component",
  templateUrl: "./form.component.html",
  styleUrls: ["./form.component.scss"]

})
export class FormComponent implements OnInit,AfterViewInit {

  public formModel: any = {components: [], pages: []};
  public done = false;

  @ViewChildren(FormRendererComponent)
  public formRenderers:QueryList<FormRendererComponent>

  public formRenderer: FormRendererComponent;

  public sended:boolean = false;

  private formId:string;
  constructor(private formHttpService: FormHttpService,
              private route: ActivatedRoute,
              private toastrService:ToastrService,) {

  }

  public ngAfterViewInit(): void {
    this.formRenderers.changes.subscribe((comps: QueryList <FormRendererComponent>) =>
    {
      this.formRenderer = comps.first;
    });
  }
  public ngOnInit(): void {
    this.formId = this.route.snapshot.params.id;

    this.formHttpService.getForm(this.formId).subscribe(res => {
      this.formModel = JSON.parse(res.form);
      this.done = true;

    }, err => {
      this.toastrService.error("Definicja formularza nie jest w tym momencie dostepna")
    })
  }

  public send(){
    if (!this.formRenderer.areAllPageFormsValid()) {
      this.toastrService.warning("Nie prawidlowa wypelniony formularz!")
      return;
    }

    let formData = this.formRenderer.getFormValues();
    this.formHttpService.sendForm(this.formId,JSON.stringify(formData)).subscribe(res => {
      this.toastrService.success("Udalo sie wyslac formularz");
      this.sended = true;
    }, err => {
      this.toastrService.error("Blad podczas wysylki formularza");
    })
  }
}
