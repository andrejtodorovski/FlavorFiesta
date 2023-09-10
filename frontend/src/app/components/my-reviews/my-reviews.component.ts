import { Component, OnInit } from '@angular/core';
import { ReviewInfo } from 'src/app/models/reviewInfo';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-my-reviews',
  templateUrl: './my-reviews.component.html',
  styleUrls: ['./my-reviews.component.css']
})
export class MyReviewsComponent implements OnInit {
  reviews: ReviewInfo[] = [];
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.loadReviews();
  }

  loadReviews() {
    this.userService.getUserReviews().subscribe({
      next: (response: any) => {
        this.reviews = response;
      }
    });
  }

}
