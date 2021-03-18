import { Component, OnInit } from '@angular/core';
import {MenuItem} from 'primeng/api';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavBarComponent implements OnInit {

  items: MenuItem[];

  constructor() { }

  ngOnInit() {
    this.loadNavItems();
}

  loadNavItems(){
    this.items = [];
    this.items.push({label: "HOME"});
  }
}
