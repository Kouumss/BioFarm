import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Product } from '../../../shared/models/product';
import { MatCard, MatCardActions, MatCardContent } from '@angular/material/card';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { ProductDetailsComponent } from '../product-details/product-details.component';


@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [
    MatCard,
    MatCardContent,
    CurrencyPipe,
    MatCardActions,
    MatButton,
    MatIcon,
    CommonModule,
    RouterLink,
    ProductDetailsComponent
  ],
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent {
  @Input() product?: Product;
  selectedProduct?: Product;

  openModal(event: Event) {
    event.preventDefault();
    if (this.product) {
      this.selectedProduct = this.product;
    }
  }
  closeModal() {
    this.selectedProduct = undefined;
  }
}
