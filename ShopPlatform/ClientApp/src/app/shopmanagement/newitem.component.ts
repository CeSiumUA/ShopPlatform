import {Component, OnInit} from '@angular/core';
import {ShopsmanagerService} from './shopsmanager.service';
import {Router} from "@angular/router";

@Component({
  selector: 'app-newitem',
  template: `
  <div class="col-md-8 order-md-1">
    <h4 class="mb-3" i18n="ItemInfo">New Item Info</h4>
    <div class="mb-3">
      <label i18n="ItemName">Name</label>
      <input class="form-control" type="text" [(ngModel)]="ItemName">
    </div>
    <div class="mb-3">
      <label i18n="ItemDescription">Description</label>
      <input class="form-control" type="text" [(ngModel)]="ItemDescription">
    </div>
    <div class="mb-3">
      <label i18n="ItemPrice">Price</label>
      <input class="form-control" type="number" [(ngModel)]="ItemPrice" (input)="checkPriceValue($event)">
    </div>
    <div class="mb-3">
      <label i18n="CurrencyType">Currency</label>
      <div class="form-check form-check-inline">
        <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio1" value="option1">
        <label class="form-check-label" for="inlineRadio1">1</label>
      </div>
      <div class="form-check form-check-inline">
        <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio2" value="option2">
        <label class="form-check-label" for="inlineRadio2">2</label>
      </div>
    </div>
  </div>
  `
})
export class NewitemComponent implements OnInit{
  public ItemName: string;
  public ItemDescription: string;
  public ItemPrice: number;
  public ItemCurrency: number;
  constructor(private shopsService: ShopsmanagerService, private router: Router) {

  }
  public checkPriceValue(event: any): void{
    if(this.ItemPrice < 0){
      this.ItemPrice = Math.abs(this.ItemPrice);
    }
  }
  ngOnInit(): void {
    console.log(this.shopsService.selectedShopId);
    if(this.shopsService.selectedShopId == 0){
      this.router.navigateByUrl('myshop');
    }
    else{
      this.shopsService.selectedShopId = 0;
    }
  }
}
