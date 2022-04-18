import {
  Component,
  AfterViewInit,
  EventEmitter,
  OnDestroy,
  Input,
  Output
} from '@angular/core';

declare var tinymce: any;

@Component({
  selector: 'tinymce',
  template: '<textarea id="{{elementId}}"></textarea>'
})
export class TinyMCEComponent implements AfterViewInit, OnDestroy {
  @Input() elementId: string = '';
  @Input() value: any;
  @Output() onEditorContentChange = new EventEmitter();

  editor:any;

  ngAfterViewInit() {
    tinymce.baseURL = "/assets/tinymce";
    tinymce.init({
      selector: '#' + this.elementId,
      height: '40rem',
      language: 'pl',
      plugins: ['link', 'table', 'image', 'code'],
      toolbar: "undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | forecolor backcolor | fontselect | link | fontsizeselect",
      fontsize_formats: '2pt 4pt 6pt 8pt 10pt 12pt 14pt 18pt 24pt 36pt',
      setup: (editor: { on: (arg0: string, arg1: () => void) => void; getContent: () => any; }) => {
        this.editor = editor;
        editor.on('keyup change', () => {
          const content = editor.getContent();
          this.onEditorContentChange.emit(content);
        });
      },
      init_instance_callback: (editor: any) => {
        editor && this.value && this.editor.setContent(this.value);
      }
    });
  }

  ngOnDestroy() {
    if(tinymce == null){
      return;
    }

    tinymce.remove(this.editor);
  }
}
