import { Component } from '@angular/core';
import { first } from 'rxjs/operators';
import { SelectItem, MessageService } from 'primeng/api';
import { Lodging } from 'src/app/models/lodging';
import { LodgingsService } from 'src/app/services/lodgings.service';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'lodgings-admin',
  templateUrl: 'lodgings.component.html',
  styleUrls: ['lodgings.component.css']
})

export class LodgingsComponent {
  lodgings: Lodging[];
  lodgingDialog: boolean;
  lodging: Lodging;
  selectedLodgings: Lodging[];
  submitted: boolean;
  statuses: any[];
  lodgingsLoaded: boolean = false;

  constructor(private lodgingsService: LodgingsService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService) {
    this.lodgings = [];
  }

  clonedLodgings: { [s: string]: Lodging; } = {};


  ngOnInit() {
    this.getLodgings();
    this.statuses = [
      { label: 'DISPONIBLE', value: 'true' },
      { label: 'NO DISPONIBLE', value: 'false' },
    ];
  }

  openNew() {
    this.submitted = false;
    this.lodging = { ...this.lodging };
    this.lodgingDialog = true;
  }

  deleteSelectedLodging() {
    this.lodgings = this.lodgings.filter(val => !this.selectedLodgings.includes(val));
    this.selectedLodgings = null;
    this.lodgingsService.deleteLodging(this.lodging).subscribe(data => { this.getLodgings(); });
  }

  editLodging(lodging: Lodging) {
    this.lodging = { ...lodging };
    this.lodgingDialog = true;
  }

  deleteLodging(lodging: Lodging) {
    this.lodgingsService.deleteLodging(lodging).subscribe(data => { this.getLodgings(); });
  }

  hideDialog() {
    this.lodgingDialog = false;
    this.submitted = false;
  }

  saveLodging() {
    this.submitted = true;
    this.lodgingsService.deleteLodging(this.lodging).subscribe(data => { this.getLodgings(); });
    this.lodgingsService.addLodgings(this.lodging).subscribe(data => { this.getLodgings(); });
    this.lodgingDialog = false;
  }

  findIndexById(id: string): number {
    let index = -1;
    for (let i = 0; i < this.lodgings.length; i++) {
      if (this.lodgings[i].id === id) {
        index = i;
        break;
      }
    }

    return index;
  }

  getLodgings() {
    this.lodgingsService.getLodgings().subscribe(data => {
      this.lodgings = this.lodgingsService.lodgings;
      this.lodgingsLoaded = true;
    }, error => {
    });
  }

}

