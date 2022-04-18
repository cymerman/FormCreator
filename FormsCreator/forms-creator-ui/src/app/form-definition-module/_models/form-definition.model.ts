export class FormDefinition {
  public id: number = 0;
  public description: string = "";
  public title: string = "";
  public name: string = "";
  public form: string = "{\"components\": [], \"pages\": [], \"isWizard\": false}";
  public status: FormDefinitionStatus = FormDefinitionStatus.WorkCopy;
  public isFileCountLimited: boolean = false;
  public fileCount: number = 0;
  public children: Array<FormDefinition> = [];

  public childrenIds: Array<number> = [];
  public formUid?:string;

  public createdBy?: string;
  public modifiedBy?: string;
  public createdOn?: Date;
  public modifiedOn?: Date;
}

export enum FormDefinitionStatus {
  WorkCopy,
  Confirmed,
  Published,
  NotActive
}

export class FormDefinitionDict {
  public static statuses: Array<any> = [
    {key: FormDefinitionStatus.WorkCopy, value: "Kopia robocza"},
    {key: FormDefinitionStatus.Confirmed, value: "Potwierdzony"},
    {key: FormDefinitionStatus.Published, value: "Opublikowany"},
    {key: FormDefinitionStatus.NotActive, value: "Nieaktywny"}
  ];


  public static getStatusText(key: FormDefinitionStatus) {
    return this.findValueByKey(key, this.statuses);
  }

  private static findValueByKey(key: any, arr: Array<any>) {
    for (let i = 0; i < arr.length; i++) {
      if (arr[i].key == key) {
        return arr[i].value;
      }
    }

    return "";
  }
}
