import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {switchMap} from "rxjs/operators";
import {ShopsmanagerService} from "./shopsmanager.service";

@Component({
  selector: 'app-shopselector',
  template: `
  <div class="row">
    <button class="btn btn-primary" i18n="Add item button" (click)="addNewItem()">Add item</button>
  </div>
  `
})
export class ShopselectorComponent implements OnInit{
  private id: number;
  constructor(private activateRoute: ActivatedRoute, private shopsService: ShopsmanagerService, private router: Router) {
  }
  addNewItem(): void{
    this.shopsService.selectedShopId = this.id;
    this.router.navigateByUrl('newitem');
  }
  ngOnInit() {
    this.activateRoute.paramMap.pipe(
      switchMap(params => params.getAll('id'))
    )
      .subscribe(data=> {
        this.id = +data;
      });
  }
}
