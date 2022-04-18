import {Component, OnInit} from "@angular/core";

import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {FormDefinition, FormDefinitionDict, FormDefinitionStatus} from "../../_models/form-definition.model";
import {FormDefinitionService} from "../../_services/form-definition.service";
import {Observable, of} from "rxjs";
import {ToastrService} from "ngx-toastr";
import {FormSettings} from "../../../form-builder-module/_models/form-settings.model";

@Component({
  selector: "form-definition-details",
  templateUrl: "form-definition-details.component.html",
})
export class FormDefinitionDetailsComponent implements OnInit {

  public formModel: any = {components: [], pages: []};
  public formPreview: any = {components: [], pages: []};

  public form: FormGroup;
  public submitted: boolean = false;
  formDefinition: FormDefinition = new FormDefinition();
  public isNew: boolean = false;
  public isPreview: boolean = false;
  public selectedFormDefinition: FormDefinition = new FormDefinition;

  public formTitle: string;

  public formDefinitions$: Observable<FormDefinition>;
  public formUrl:string;

  constructor(private formDefinitionService: FormDefinitionService,
              private route: ActivatedRoute,
              private toastr: ToastrService,
              private router: Router,
              private fb: FormBuilder) {
  }

  public ngOnInit(): void {
    this.formDefinitions$ = Observable.create((observer: any) => {
      let token = this.formTitle;
      let excludedIds = [];

      for (let i = 0; i < this.formDefinition.children?.length; i++) {
        excludedIds.push(this.formDefinition.children[i].id);
      }

      if (!token) {
        return of([]);
      }

      return this.formDefinitionService
        .searchByTitle(token, excludedIds)
        .subscribe(res => {
          observer.next(res);
        })
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.load(Number.parseInt(id));
    } else {
      this.isNew = true;
      this.prepareFormOnStart();
      this.createForm(this.formDefinition);
    }
  }

  public cancel() {
    this.createForm(this.formDefinition);
  }

  public close() {
    this.router.navigate([ "form-definition", "list"]);
  }

  public save() {
    if (!this.validate()) {
      return;
    }

    if (this.isNew) {
      this.add();
    } else {
      this.update();
    }
  }

  public getStatusText(status: FormDefinitionStatus) {
    return FormDefinitionDict.getStatusText(status);
  }

  public changeStatus(status: FormDefinitionStatus) {
    this.formDefinitionService.updateStatus(this.formDefinition.id, status).subscribe(res => {
      this.toastr.success("Aktualizacja statusu definicji formularza", "Aktualizacja statusu definicji formularza powiodła się.");
      this.load(this.formDefinition.id);
    }, error => {
      if (error && error.status === 404) {
        this.toastr.error("Aktualizacja statusu definicji formularza", "Nie znaleziono definicji formularza" + this.formDefinition.id);
      } else if (error && error.status === 400 && error.error && error.error.code === "0033") {
        this.toastr.error("Aktualizacja statusu definicji formularza", "Nieprawidłowa zmiana statusu");
      }
      else {
        this.toastr.error("Aktualizacja statusu definicji formularza", "Wystąpił błąd");
      }
    })
  }

  public previewSelected() {
    let preview = new FormSettings();
    Object.assign(preview, this.formModel);
    this.formPreview = preview;
    setTimeout(() => {
      this.isPreview = true;
    }, 1)
  }

  public deploySelected(){
    this.isPreview = false;
    let baseUrl = window.location.origin;

    this.formUrl = baseUrl + "/s/" + this.formDefinition.formUid;
  }

  public isDeployActive(){
    return this.formDefinition.status === FormDefinitionStatus.Published;
  }

  public resetSelection($event:any) {
    this.selectedFormDefinition = null;
  }

  public select($event:any) {
    this.selectedFormDefinition = $event.item;
  }

  changePreviewState(state:boolean){
    console.log(state)
    this.isPreview = state;
  }

  public print() {
    this.formDefinitionService.printApplicationDefinition(this.formDefinition.id, this.formDefinition.title);
  }

  private validate() {
    this.submitted = true;

    return this.form.valid;
  }

  private load(id: number) {
    this.isNew = false;
    this.formDefinitionService.get(id)
      .subscribe(res => {
        this.formDefinition = res;

        this.prepareFormOnStart();
        this.createForm(this.formDefinition);
      });
  }

  private prepareFormOnStart() {
    this.formModel = JSON.parse(this.formDefinition.form);
  }

  private add(): void {
    let params = this.prepareAddParams();

    this.formDefinitionService.add(params)
      .subscribe((id: number) => {
        this.toastr.success("Dodanie definicji formularza", "Dodanie definicji formularza powiodło się.");
        this.router.navigate(["/", "form-definition", "details", id]);
      }, error => {
        this.toastr.error("Dodanie definicji formularza", "Wystąpił błąd");
      });
  }

  private prepareAddParams(): FormDefinition {
    const formModel = this.form.value;
    const params: FormDefinition = new FormDefinition();
    params.name = formModel.name as string;
    params.description = formModel.description as string;
    params.title = formModel.title as string;
    params.form = JSON.stringify(this.formModel);
    params.isFileCountLimited = formModel.isFileCountLimited as boolean;
    params.fileCount = formModel.fileCount as number;
    for (let i = 0; i < this.formDefinition.children?.length; i++) {
      params.childrenIds.push(this.formDefinition.children[i].id);
    }

    return params;
  }

  private update(): void {
    let params = this.prepareUpdateParams();

    this.formDefinitionService.update(this.formDefinition.id, params)
      .subscribe(() => {
        this.toastr.success("Aktualizacja definicji formularza", "Aktualizacja definicji formularza powiodła się.");
        this.load(this.formDefinition.id);
      }, error => {
        if (error && error.status === 404) {
          this.toastr.error("Aktualizacja definicji formularza", "Nie znaleziono definicji formularza ID=" + this.formDefinition.id);
        } else if (error && error.status === 400 && error.error && error.error.code === "0033") {
          this.toastr.error("Aktualizacja definicji formularza", "Nieprawidłowa zmiana statusu");
        }
        else if (error && error.status === 400 && error.error && error.error.code === "0034") {
          this.toastr.error("Aktualizacja definicji formularza", "Nie można edytować wniosku w tym statusie");
        }
        else {
          this.toastr.error("Aktualizacja definicji formularza", "Wystąpił błąd");
        }
      });
  }

  private prepareUpdateParams(): FormDefinition {
    const formModel = this.form.value;
    const params: FormDefinition = new FormDefinition();
    params.name = formModel.name as string;
    params.description = formModel.description as string;
    params.title = formModel.title as string;
    params.form = JSON.stringify(this.formModel, null, 4);
    params.id = this.formDefinition.id;
    params.status = this.formDefinition.status;

    params.isFileCountLimited = formModel.isFileCountLimited as boolean;
    params.fileCount = formModel.fileCount as number;
    for (let i = 0; i < this.formDefinition.children?.length; i++) {
      params.childrenIds.push(this.formDefinition.children[i].id);
    }
    return params;
  }


  private createForm(formDefinition: FormDefinition) {
    this.form = this.fb.group({
      name: [formDefinition.name, [Validators.maxLength(100)]],
      description: [formDefinition.description, [Validators.maxLength(200)]],
      title: [formDefinition.title, [Validators.required, Validators.maxLength(200)]],
      isFileCountLimited: [formDefinition.isFileCountLimited, []],
      fileCount: [formDefinition.fileCount || 0, [Validators.min(0)]]
    });

    console.log(this.form)
  }
}

