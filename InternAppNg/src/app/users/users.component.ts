import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AddUserModalComponent } from './add-user-modal/add-user-modal.component';
import { User } from '../models/user.model';
import { SelectionComponent } from '../selection/selection.component';
import { UserService } from './user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  users: User[]
  showSpinner: boolean = true

  constructor(private user: UserService, private snackBar: MatSnackBar, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(){
    this.user.getUsers().subscribe((response) => {
      this.users = response.data;
      this.showSpinner = false;
    })
  }

  onClickDelete(id: string){
    this.user.deleteUser(id)
    .subscribe((response) => {
      console.log(response)
      this.snackBar.open('User deleted succesfully', 'Close', { duration: 5000 })
      this.getUsers();
    })
  }

  openDialog() : void {
    const dialogRef = this.dialog.open(AddUserModalComponent);
  }


}
