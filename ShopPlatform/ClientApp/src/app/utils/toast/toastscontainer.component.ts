import {Component, OnInit, TemplateRef} from "@angular/core";
import {ToastsService} from "./toasts.service";

@Component({
  selector: 'app-toast',
  template: `
    <ngb-toast
      *ngFor="let toast of toastService.toasts"
      [class]="toast"
      [autohide]="true"
      [delay]="toast.options.delay || 5000"
      (hide)="toastService.remove(toast)"
    >
      <ng-template [ngIf]="isTemplate(toast)" [ngIfElse]="text">
        <ng-template [ngTemplateOutlet]="toast.textOrCmp"></ng-template>
      </ng-template>

      <ng-template #text>{{ toast.textOrCmp }}</ng-template>
    </ngb-toast>
  `,
  host: {'[class.ngb-toasts]': 'true'}
})
export class ToastscontainerComponent implements OnInit{
  constructor(public toastService: ToastsService) {}

  isTemplate(toast) { return toast.textOrTpl instanceof TemplateRef; }

  ngOnInit(): void {
  }
}
