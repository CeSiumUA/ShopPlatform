import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-itemview',
  template: `
    <div class="card">
      <div class="container-fliud">
        <div class="wrapper row">
          <div class="preview col-md-6">

            <div class="preview-pic tab-content">
              <div class="tab-pane" *ngFor="let prodImg of Product.images; let i = index" id="pic-{{i}}"><img src="cdn/files/icons/{{prodImg}}" /></div>
<!--              <div class="tab-pane active" id="pic-1"><img src="http://placekitten.com/400/252" /></div>-->
<!--              <div class="tab-pane" id="pic-2"><img src="http://placekitten.com/400/252" /></div>-->
<!--              <div class="tab-pane" id="pic-3"><img src="http://placekitten.com/400/252" /></div>-->
<!--              <div class="tab-pane" id="pic-4"><img src="http://placekitten.com/400/252" /></div>-->
<!--              <div class="tab-pane" id="pic-5"><img src="http://placekitten.com/400/252" /></div>-->
              <div class="tab-pane active" id="pic-{{Product.images.length}}"><img src="cdn/files/icons/{{Product.photoUrl}}"></div>
            </div>
            <ul class="preview-thumbnail nav nav-tabs">
              <li class="active"><a data-target="#pic-{{Product.images.length}}" data-toggle="tab"><img src="cdn/files/icons/{{Product.photoUrl}}" /></a></li>
              <li *ngFor="let image of Product.images; let i = index">
                <a data-target="#pic-{{i}}" data-toggle="tab">
                  <img src="cdn/files/icons/{{image}}" />
                </a>
              </li>
              <!--<li><a data-target="#pic-2" data-toggle="tab"><img src="http://placekitten.com/200/126" /></a></li>
              <li><a data-target="#pic-3" data-toggle="tab"><img src="http://placekitten.com/200/126" /></a></li>
              <li><a data-target="#pic-4" data-toggle="tab"><img src="http://placekitten.com/200/126" /></a></li>
              <li><a data-target="#pic-5" data-toggle="tab"><img src="http://placekitten.com/200/126" /></a></li>-->
            </ul>

          </div>
          <div class="details col-md-6">
            <h3 class="product-title">{{Product.name}}</h3>
            <div class="rating">
              <ng-template #t let-fill="fill">
              <span class="star" [class.full]="fill === 100">
                <span class="half" [style.width.%]="fill">&#9733;</span>&#9733;
              </span>
              </ng-template>
              <ngb-rating [(rate)]="Product.rating" [starTemplate]="t" [readonly]="true" [max]="5"></ngb-rating>
            </div>
            <p class="product-description">{{Product.description}}</p>
            <h4 class="price">Current price: <span>{{(Product.currency == '0')?'$':'₪'}}{{Product.price}}</span></h4>
            <p class="vote"><strong>0%</strong> of buyers enjoyed this product! <strong>(0 votes)</strong></p>
            <div class="action">
              <button class="btn btn-primary" type="button">Add to cart</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  `,
  styleUrls: ['./itemview.component.css']
})
export class ItemviewComponent implements OnInit{
  private id: string;
  public Product: any;
  constructor(private routing: Router, private activatedRoute: ActivatedRoute, private httpClient: HttpClient) {
  }
  ngOnInit() {
    this.id = this.activatedRoute.snapshot.params.id;
    this.httpClient.get(`api/items/${this.id}`).subscribe((data:any) => this.Product = data.payload);
  }
}
