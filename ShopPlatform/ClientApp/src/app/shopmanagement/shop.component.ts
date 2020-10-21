import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-shop',
  template:
  `<div *ngIf="shops?.length > 0">
      <h2 class="border-bottom border-gray pb-2 mb-0" i18n="ShopsListHeaderLabel|Label for shops list header">Your shops</h2>
      <div *ngFor="let shop of shops" class="card mb-3">
        <img class="card-img-top" [src]="shop.iconUrl" alt="Shop icon" width="1576" height="180">
        <div class="card-body">
          <h5 class="card-title"><a routerLink="/shops/{{shop.id}}">{{shop.shopName}}</a></h5>
          <p class="card-text">{{shop.shopDescription}}</p>
          <p class="card-text">
            <ng-template #t let-fill="fill">
              <span class="star" [class.full]="fill === 100">
                <span class="half" [style.width.%]="fill">&#9733;</span>&#9733;
              </span>
            </ng-template>
            <ngb-rating [(rate)]="shop.rating" [starTemplate]="t" [readonly]="true" [max]="5"></ngb-rating>
          </p>
        </div>
      </div>
    </div>
    <div *ngIf="shops?.length == 0">
      <section class="jumbotron text-center">
        <div class="container">
          <h1 i18n="StartTradingLabel|Label for start trading promotion menu">Start trading, adding your shop!</h1>
          <p class="lead text-muted" i18n="ShopAddDescription| Label for shopadddescription">You can add a shop, if you are an owner, or join an existing shop as an employee</p>
          <p>
            <a routerLink="/newshop" class="btn btn-primary my-2">Add new shop</a>
          </p>
        </div>
      </section>
    </div>`,
  styles: [`
    .star {
      position: relative;
      display: inline-block;
      font-size: 20px;
      color: #d3d3d3;
    }
    .full {
      color: #9fa0a0;
    }
    .half {
      position: absolute;
      display: inline-block;
      overflow: hidden;
      color: #9fa0a0;
    }
  `]
})
export class ShopComponent implements OnInit{
  public shops: any;
  constructor(private httpClient: HttpClient) {
  }
  ngOnInit(): void {
    this.httpClient.get('api/shop/myshops').subscribe((data: any) => {
      if (data.error == null){
        this.shops = data.payload;
      }
      else{

      }
    });
  }
}
