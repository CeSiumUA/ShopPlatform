import {Injectable, TemplateRef} from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class ToastsService{
  public toasts: Toast[] = [];
  constructor() {
  }
  public show(textOrCmp: string | TemplateRef<any>, options: any = {}): void {
    this.toasts.push({textOrCmp: textOrCmp, options: options});
  }
  public remove(toast){
    this.toasts = this.toasts.filter(t => t !== toast);
  }
}

export class Toast{
  textOrCmp: string | TemplateRef<any>;
  options: any;
}
