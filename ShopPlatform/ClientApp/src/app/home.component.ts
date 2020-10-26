import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-home',
  template: `
  <div class="col-lg-9">
    <div class="row">
      <div *ngFor="let item of TopItems" class="col-lg-4 col-md-6 mb-4">
        <div class="card h-100">
          <a routerLink="/items/{{item.viewId}}">
            <img class="card-img-top" src="cdn/files/icons/{{item.photoUrl}}">
          </a>
          <div class="card-body">
            <h4 class="card-title">
              <a routerLink="/items/{{item.viewId}}">
                {{item.Name}}
              </a>
            </h4>
            <h5>{{(item.currency == '0')?'$':'₪'}} {{item.price}}</h5>
            <p class="card-text">
              {{item.description}}
            </p>
          </div>
          <div class="card-footer">
            <ng-template #t let-fill="fill">
              <span class="star" [class.full]="fill === 100">
                <span class="half" [style.width.%]="fill">&#9733;</span>&#9733;
              </span>
            </ng-template>
            <ngb-rating [(rate)]="item.rating" [starTemplate]="t" [readonly]="true" [max]="5"></ngb-rating>
          </div>
          <div class=""></div>
        </div>
      </div>
    </div>
  </div>`,
  styles: [`
    .star {
      position: relative;
      display: inline-block;
      font-size: 1.5rem;
      color: #d3d3d3;
    }

    .full {
      color: red;
    }

    .half {
      position: absolute;
      display: inline-block;
      overflow: hidden;
      color: #4a4848;
    }
  `]
})
export class HomeComponent implements OnInit{
  public TopItems: any;
  constructor(private httpClient: HttpClient) {
  }
  ngOnInit(): void {
    this.httpClient.get('api/items/top').subscribe((data:any) => this.TopItems = data.payload);
  }
}
