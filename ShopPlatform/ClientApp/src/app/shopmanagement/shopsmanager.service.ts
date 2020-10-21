import {Injectable, OnInit} from '@angular/core';
import {Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ShopsmanagerService {
  private selectedShopId: number;
  constructor(private router: Router) {
  }
}

