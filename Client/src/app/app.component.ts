import { Component, inject, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { ShopComponent } from "./pages/shop/shop.component";
import { FooterComponent } from "./layout/footer/footer.component";
import { CommonModule } from '@angular/common';
import { RouteService } from './core/services/route.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, ShopComponent, FooterComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})

export class AppComponent implements OnInit {

  isHomePage: boolean = false;
  private routeService = inject(RouteService);

  ngOnInit(): void {
    this.checkIfHomePage();
  }

  checkIfHomePage(): void {
    this.routeService.isHomePage$.subscribe(isHome => {
      this.isHomePage = isHome;
    });
  }
}
