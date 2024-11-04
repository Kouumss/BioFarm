import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { map, Observable } from 'rxjs';
import { Product } from '../../shared/models/product';
import { ShopService } from '../../core/services/shop.service';
import { ShopParams } from '../../shared/models/shopParams';

@Component({
  selector: 'app-product-banner',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './product-banner.component.html',
  styleUrl: './product-banner.component.scss'
})
export class ProductBannerComponent {

  title = "Discover Authentic Freshness of Fruits";

  tagline = "Carefully selected, our fruit crates bring a taste of freshness to every bite.";

  fruitProducts$!: Observable<Product[]>;

  shopParams: ShopParams = {
    brands: [],
    types: [],
    sort: '',
    search: 'caisse', // Filtre pour "fruits"
    pageSize: 10,
    pageNumber: 1
  };

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.fruitProducts$ = this.shopService.getProducts(this.shopParams).pipe(
      map(response => response.data)  // Assure que seules les données sont utilisées, en supposant que l'API renvoie un champ `data`
    );
  }
}
