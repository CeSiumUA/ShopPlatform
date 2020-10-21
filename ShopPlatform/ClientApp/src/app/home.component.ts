import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-home',
  template: `
  <div class="col-lg-9">
    <div class="row">
      <div *ngFor="let item of TopItems" class="col-lg-4 col-md-6 mb-4">
        <div class="card h-100">
          <a routerLink="/items/{{item.id}}">
            <img class="card-img-top" src="item.photoUrl">
          </a>
          <div class="card-body">
            <h4 class="card-title">
              <a routerLink="/items/{{item.id}}">
                {{item.Name}}
              </a>
            </h4>
            <h5>{{item.currency}} {{item.price}}</h5>
            <p class="card-text">
              {{item.description}}
            </p>
          </div>
          <div class="card-footer">
            <small *ngFor="let item of [].constructor(5); let i = index" class="text-muted">
              ★
            </small>
          </div>
          <div class=""></div>
        </div>
      </div>
    </div>
  </div>`
})
export class HomeComponent implements OnInit{
  public TopItems: any;
  constructor(private httpClient: HttpClient) {
  }
  ngOnInit(): void {
  }
}
