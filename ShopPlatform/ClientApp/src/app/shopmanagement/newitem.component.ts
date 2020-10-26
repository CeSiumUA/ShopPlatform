import {Component, OnInit} from '@angular/core';
import {ShopsmanagerService} from './shopsmanager.service';
import {Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {FileRef, FiletransferService} from "../http/filetransfer.service";
import {HttpClient} from "@angular/common/http";
import {ToastsService} from "../utils/toast/toasts.service";

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
      <textarea class="form-control" rows="5" [(ngModel)]="ItemDescription"></textarea>
    </div>
    <div class="mb-3">
      <label i18n="ItemPrice">Price</label>
      <input class="form-control" type="number" [(ngModel)]="ItemPrice" (input)="checkPriceValue($event)">
    </div>
    <div class="mb-3">
      <label i18n="CurrencyType">Currency</label>
      <form [formGroup]="form" class="form-control">
        <div class="form-check form-check-inline">
          <input class="form-check-input" type="radio" formControlName="currency" id="inlineRadio1" value="0" checked>
          <label class="form-check-label" for="inlineRadio1">USD</label>
        </div>
        <div class="form-check form-check-inline">
          <input class="form-check-input" type="radio" formControlName="currency" id="inlineRadio2" value="1">
          <label class="form-check-label" for="inlineRadio2">ILS</label>
        </div>
      </form>
      <div class="form-group">
        <label for="exampleFormControlFile1">Item photos</label>
        <input #files type="file" class="form-control-file" id="exampleFormControlFile1" multiple (change)="uploadFiles(files.files)">
      </div>
    </div>
    <button class="btn btn-primary" i18n="AddThisItemButton" (click)="addNewItem()" [disabled]="!readyToPush">Add this item</button>
    <button (click)="test()">Test</button>
  </div>
  `
})
export class NewitemComponent implements OnInit{
  public form: FormGroup;
  public ItemName: string;
  public ItemDescription: string;
  public ItemPrice: number;
  public fileIds: any;
  public readyToPush = true;
  constructor(private shopsService: ShopsmanagerService,
              private router: Router,
              private fb: FormBuilder,
              private fileTransfer: FiletransferService,
              private httpClient: HttpClient,
              private toastService: ToastsService
  ) {
    this.form = fb.group({
      currency: ['0', Validators.required]
    });
  }
  public uploadFiles(files: any): void{
    this.readyToPush = false;
    this.fileTransfer.uploadFiles(files, FileRef.Item).subscribe((data) =>{
      this.fileIds = data.payload;
      console.log(this.fileIds);
      this.readyToPush = true;
    });
  }
  public checkPriceValue(event: any): void{
    if(this.ItemPrice < 0){
      this.ItemPrice = Math.abs(this.ItemPrice);
    }
  }
  public addNewItem(): void{
    let currency = this.form.value.currency;
    if(currency != '') {
      let item = {
        Name: this.ItemName,
        Description: this.ItemDescription,
        PhotoUrl: this.fileIds[0],
        Images: this.fileIds,
        Price: this.ItemPrice,
        Currency: currency,
        ShopId: this.shopsService.selectedShopId
      };
      console.log(item);
      this.httpClient.post('api/items/additem', item).subscribe((data: any) =>{
        if(data.payload == "Succeeded!"){
          this.toastService.show("Item added Successfully!", { classname: 'bg-success text-light', delay: 5000 });
          this.router.navigateByUrl('/myshop');
        }
      });
    }
    else{

    }
  }
  test(): void{
    this.toastService.show("Item added Successfully!", { classname: 'bg-success text-light', delay: 5000 });
  }
  ngOnInit(): void {
    if(this.shopsService.selectedShopId == 0){
      this.router.navigateByUrl('myshop');
    }
    else{
      this.shopsService.selectedShopId = 0;
    }
  }
}
