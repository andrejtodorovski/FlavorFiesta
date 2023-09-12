import { Component, OnInit } from '@angular/core';
import { UserInfo } from 'src/app/models/userInfo';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  users: UserInfo[] = [];
  selectedFile: File | null = null;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.selectedFile = null;
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getAllUsers().subscribe({
      next: (response: UserInfo[]) => {
        this.users = response;
      }
    });
  }
  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0] as File;
  }

  importUsers(): void {
    if (!this.selectedFile) {
      console.log('No file selected');
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.userService.importUsers(formData).subscribe({
      next: () => {
        this.loadUsers();
        this.selectedFile = null;
      }
    });
  }
    
}
