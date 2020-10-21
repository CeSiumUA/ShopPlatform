import {Component, OnInit} from '@angular/core';
import {ShopsmanagerService} from './shopsmanager.service';

@Component({
  selector: 'app-newitem',
  template: ``
})
export class NewitemComponent implements OnInit{
  constructor(private shopsService: ShopsmanagerService) {
  }
  ngOnInit(): void {
  }
}
