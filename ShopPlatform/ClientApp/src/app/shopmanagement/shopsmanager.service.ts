import {Injectable, OnInit} from '@angular/core';
import {Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ShopsmanagerService {
  public selectedShopId: number;
  constructor(private router: Router) {
    this.selectedShopId = 0;
  }
}

