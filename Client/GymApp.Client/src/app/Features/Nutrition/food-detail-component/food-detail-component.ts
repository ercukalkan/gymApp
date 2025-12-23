import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { FoodService } from '../../../Core/Services/food-service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-food-detail-component',
  imports: [],
  templateUrl: './food-detail-component.html',
  styleUrl: './food-detail-component.css',
})
  
export class FoodDetailComponent implements OnInit {
  private foodService = inject(FoodService);
  private route = inject(ActivatedRoute);
  product?: any;
  cdr = inject(ChangeDetectorRef);

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    this.foodService.getFoodById(id).subscribe(data => {
      this.product = data;
      this.cdr.detectChanges();
    });
  }
}
