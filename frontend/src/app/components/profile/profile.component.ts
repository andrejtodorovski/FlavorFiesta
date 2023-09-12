import { Component, OnInit } from '@angular/core';
import { UserInfo } from 'src/app/models/userInfo';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit{
  user: UserInfo | undefined;
  mostOrderedFoods : any;
  selectedTab: string = 'tab1'; // Default selected tab
  constructor(private userService: UserService) { }
  ngOnInit(): void {
    this.getUserInfo();
  }

  getUserInfo(): void {
    this.userService.getUserInfo().subscribe(
      (response: UserInfo) => {
        this.user = response;        
      }
    );
  }

  selectTab(tabName: string): void {
    this.selectedTab = tabName;
  }

  isStarFilledRating(rating: number, starNumber: number): boolean {
    return starNumber <= rating;
  }

}
