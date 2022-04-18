import {Directive, ElementRef, Input, OnInit} from "@angular/core";

@Directive({
  selector: '[imgSrc]'
})
export class ImgSrcDirective implements OnInit {

  @Input("imgSrc")
  public url: string = "";

  constructor(private el: ElementRef) {
  }

  public ngOnInit(): void {
    this.el.nativeElement.setAttribute("src", this.url);
  }
}
