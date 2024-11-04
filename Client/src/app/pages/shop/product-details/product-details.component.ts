import { Component, inject, OnInit, OnChanges, Input, Output, EventEmitter, SimpleChanges, ViewEncapsulation } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { Product } from '../../../shared/models/product';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatDivider } from '@angular/material/divider';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [
    CurrencyPipe,
    MatIcon,
    MatButton,
    MatFormField,
    MatInput,
    MatLabel,
    MatDivider,
    FormsModule,
    CommonModule
  ],
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ProductDetailsComponent implements OnInit, OnChanges {
  private shopService = inject(ShopService);
  private activatedRoute = inject(ActivatedRoute);
  @Input() product?: Product;
  @Output() close = new EventEmitter<void>();
  quantity: number = 1;

  ngOnInit(): void {
    this.loadProduct();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['product'] && this.product) {
      this.quantity = this.product.quantityInStock > 0 ? 1 : 0;
    }
  }

  increaseQuantity(): void {
    if (this.product && this.quantity < this.product.quantityInStock) {
      this.quantity++;
    }
  }

  decreaseQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  closeModal(): void {
    this.close.emit();
  }

  loadProduct(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.shopService.getProduct(id).subscribe({
        next: product => this.product = product,
        error: error => console.log(error)
      });
    }
  }
}
