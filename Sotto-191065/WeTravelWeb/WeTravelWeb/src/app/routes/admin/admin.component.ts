import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { first } from 'rxjs/operators';


@Component({
  templateUrl: 'admin.component.html',
  styleUrls: ['admin.component.css'],
  providers: [MessageService]
})
export class AdminComponent {

}
