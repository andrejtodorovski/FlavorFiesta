import { Component } from '@angular/core';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent {
  contact:any = {};
  // constructor(private contactService: ContactService) {}

  onSubmit() {
    // this.contactService.sendEmail(this.formData).subscribe({
    //   next: (response) => {
    //     console.log(response);
    //   },
    //   error: (error) => {
    //     console.log(error);
    //   }
    // });
  }
}
