import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent {
  contact:any = {
    name: '',
    email: '',
    subject: '',
    message: ''
  };
  constructor(private userService: UserService, private toastrService: ToastrService) {}

  onSubmit() {    
    this.userService.sendEmail(this.contact).subscribe({
      next: (response) => {
        this.toastrService.success('Email sent successfully');
      },
      error: (error) => {
        this.toastrService.error('Email not sent');
      }
    });
  }
}
