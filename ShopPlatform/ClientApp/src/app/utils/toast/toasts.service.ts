import {Injectable, TemplateRef} from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class ToastsService{
  public toasts: any[] = [];
  constructor() {
  }
  public show(textOrCmp: string | TemplateRef<any>, options: any = {}): void {
    this.toasts.push({textOrCmp, options});
  }
  public remove(toast){
    this.toasts = this.toasts.filter(t => t !== toast);
  }
}
