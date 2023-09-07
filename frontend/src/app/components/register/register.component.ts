import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterDTO } from 'src/app/models/registerDTO';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  registerDTO: RegisterDTO = {
    userName: '',
    password: '',
    emailAddress: '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
    address: '',
  };
  error: string = '';
  constructor(private accountService: AccountService, private router: Router) {}
  ngOnInit(): void {}
  register() {
    this.accountService.register(this.registerDTO).subscribe({
      next: () => {
        this.router.navigateByUrl('/login');
      }
    });
  }
}
