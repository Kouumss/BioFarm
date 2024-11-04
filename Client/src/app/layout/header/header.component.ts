import { CommonModule } from '@angular/common';
import { Component, HostListener, inject } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { RouteService } from '../../core/services/route.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    MatIcon,
    MatButton,
    MatBadge,
    CommonModule,
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'] // Corrigé de `styleUrl` à `styleUrls`
})
export class HeaderComponent {
  
  isMenuOpen = false;
  isHomePage: boolean = false;
  isScrolled: boolean = false;

  private routeService = inject(RouteService);

  ngOnInit(): void {
    this.checkIfHomePage();
    this.handleScroll(); // On peut garder cette méthode pour gérer `isScrolled`
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  checkIfHomePage(): void {
    this.routeService.isHomePage$.subscribe(isHome => {
      this.isHomePage = isHome;
    });
  }

  handleScroll(): void {
    window.addEventListener('scroll', () => {
      this.isScrolled = window.scrollY > 10;
    });
  }
}
