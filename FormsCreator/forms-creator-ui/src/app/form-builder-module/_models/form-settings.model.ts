
export class FormSettings {
  public formId:string;
  public pages: Array<FormPage> = [];
  public isWizard: boolean = false;
  public components: Array<FormComponent> = [];
  public groupedDictionaries: Array<GroupedDictionary> = [];
}

export class FormPage {
  public title: string;
  public components: Array<FormComponent> = [];
  public index: number;
  public removable: boolean;
}

export class GroupedDictionary{
  public groupName:string;
  public groupAddedByControl:string;
  public section:string;
}

export class EnumHelpers {

  public static keys(enumType: object) {
    const members = Object.keys(enumType);
    return members.filter((x) => Number.isNaN(parseInt(x, 10)))
  }

  public static toKeyValueArray(enumType: object) {
    return EnumHelpers.keys(enumType)
      .map((key) => {
        return {key, value: enumType[key]};
      });
  }
}

export class FormComponent {
  public id: string;
  public name: string;
  public label: string;
  public isLabelVisible: boolean = true;
  public countingMultipleSectionForPayment:boolean = false;
  public hidden: boolean = false;
  public hiddenValue?:any;
  public defaultValue?:any;
  public defaultValueVisible:boolean = false;
  public isLayout: boolean = false;
  public isMultiple: boolean = false;
  public placeholder: string;
  public type: string;
  public src: string;
  public alt: string;
  public desc: string;
  public tooltip: string;
  public formula: string;
  public css: string;
  public htmlContent: string;
  public columns: Array<Column> = [];
  public parent:string;

  // builder part
  public builderDesc: string;
  public builderIcon: string;

  public validation: Validation = new Validation();
  public conditional: Conditional = new Conditional();
  public styles: FormComponentStyles = new FormComponentStyles();

  public values: Array<Values> = [];
  public childrens: Array<FormComponent> = [];

  constructor(builderDesc?: string, builderIcon?: string, type?: string, label?: string, isLayout?: boolean) {
    this.builderDesc = builderDesc;
    this.builderIcon = builderIcon;
    this.type = type;
    this.label = label;
    this.isLayout = isLayout;
  }
}

export class Conditional {
  public isVisible?: boolean;
  public relatedToName: string;
  public relatedToValue: string;

  public static isAnySet(conditional: Conditional): boolean {
    return !!((conditional.isVisible !== null && conditional.isVisible !== undefined) || conditional.relatedToName || conditional.relatedToValue);
  }
}

export class FormComponentStyles {
  public labelClasses: string;
  public inputClasses: string;
  public componentClasses: string;
}

export class Column {
  public id: string;
  public width: number;
  public offset: number;
  public childrens: Array<FormComponent> = [];
}

export class Validation {
  public required: boolean;
  public isNipValidate:boolean;
  public isNIPeselValidate:boolean;
  public isPeselValidate:boolean;
  public minLength?: number;
  public maxLength?: number;
  public pattern?: string;
  public min?: number;
  public max?: number;
  public groupByNameCheckbox:boolean;
  public groupByName:string;

  public custom: string;
}

export class FieldTypes {
  public static text = "text";
  public static number = "number";
  public static textArea = "textArea";
  public static date = "date";
  public static checkBox = "checkBox";
  public static radio = "radio";
  public static select = "select";
  public static email = "email";
  public static img = "img";
  public static section = "section";
  public static columns = "columns";
  public static table = "table";
  public static html = "html";
  public static filePicker = "filePicker";
  public static sectionMultiple = "sectionMultiple";
}

export class Address{
  public componentName:string;
  public voivodeshipId:string = '0';
  public districtId:string = '0';
  public communeId:string = '0';
  public cityId:string='0';
  public street:string = '0';
  public cantFindStreet:boolean;
  public country:string = 'Polska';
  public postCode:string;
}

export class Values {
  public display: string;
  public value: string;
}

export class DictionaryInfo {
  public externalId: number;
  public display: string;
  public value: string;
}

export class DictionaryName {
  public name:string;
}
