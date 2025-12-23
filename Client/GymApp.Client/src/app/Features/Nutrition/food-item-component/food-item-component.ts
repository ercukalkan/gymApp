import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-food-item-component',
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './food-item-component.html',
  styleUrl: './food-item-component.css',
})
export class FoodItemComponent {
  @Input() food: any;
}
