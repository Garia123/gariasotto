import { Component } from '@angular/core';
import { SelectItem, MessageService } from 'primeng/api';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';


@Component({
  selector: 'users-admin',
  templateUrl: 'users.component.html',
  styleUrls: ['users.component.css'],
})
export class UsersComponent {
  statuses: SelectItem[];
  users: User[];

  clonedUsers: { [s: string]: User; } = {};

  constructor(private productService: UserService, private messageService: MessageService) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.productService.getUsers().subscribe(data => {
      this.users = this.productService.users;
    }, error => {
    });
  }

  onRowEditInit(user: User) {
    this.clonedUsers[user.email] = { ...user };
  }

  onRowEditSave(user: User) {
    delete this.clonedUsers[user.email];
    this.productService.updateUser(user).subscribe(data => { this.getUsers();});
    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'User is updated' });
  }

  onRowEditCancel(user: User, index: number) {
    delete this.clonedUsers.users[user.email];
  }
}
