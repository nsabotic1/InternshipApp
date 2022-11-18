import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { UserService } from '../user.service';
import { UsersComponent } from '../users.component';

@Component({
  selector: 'app-add-user-modal',
  templateUrl: './add-user-modal.component.html',
  styleUrls: ['./add-user-modal.component.css']
})
export class AddUserModalComponent implements OnInit {

  constructor(private user: UserService, public dialogRef: MatDialogRef<UsersComponent>,) { }

  ngOnInit(): void {
  }

  addUser(user: { username: string, email: string, password: string }) {
    this.user.addUser(user);
    this.dialogRef.close();
  }

}
