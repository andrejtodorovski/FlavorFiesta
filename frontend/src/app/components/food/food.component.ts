import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Food } from 'src/app/models/food';
import { FoodService } from 'src/app/services/food.service';

@Component({
  selector: 'app-food',
  templateUrl: './food.component.html',
  styleUrls: ['./food.component.css']
})
export class FoodComponent implements OnInit {
  food: Food | null = null
  id: any;
  constructor(private foodService: FoodService, private route: ActivatedRoute, private router: Router) {}
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id = params.get('id');
      if(this.id!=null){
        this.getFoodById(parseInt(this.id));
      }
    });
  }
  getFoodById(id: number) {
    this.foodService.getFoodById(id).subscribe({
      next: (response: Food) => {
        this.food = response;
      },
      error: (error) => {
        if(error)
          this.router.navigateByUrl('/not-found');
      }
    });
  }

}
