import { Component, OnInit } from '@angular/core';
import { LoginDTO } from '../../models/loginDTO';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginDTO: LoginDTO = {
    userName: '',
    password: '',
  };
  error: string = '';

  constructor(private accountService: AccountService) {}
  ngOnInit(): void {}

  login() {
    this.accountService.login(this.loginDTO).subscribe({
      next: () => {
        window.location.href="/"
      },
      error: (error) => {
        this.error = error.error;
      },
    });
  }
}
