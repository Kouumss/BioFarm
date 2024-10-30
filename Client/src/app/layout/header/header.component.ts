import { CommonModule } from '@angular/common';
import { Component, HostListener, inject } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon'
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
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

  isMenuOpen = false;
  isHomePage: boolean = false;
  isScrolled: boolean = false;

  private routeService = inject(RouteService);

  ngOnInit(): void {
    this.checkIfHomePage();
    this.handleScroll();
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  checkIfHomePage(): void {
    this.routeService.isHomePage$.subscribe(isHome => {
      this.isHomePage = isHome;
    });
  }

  handleScroll() {
    window.addEventListener('scroll', () => {
        this.isScrolled = window.scrollY > 10;
    });
  }
}


  // isMenuOpen = false;
  // isHomePage: boolean = false;
  // isScrolled: boolean = false;

  // toggleMenu() {
  //   this.isMenuOpen = !this.isMenuOpen;
  // }

  // @HostListener('window:scroll', [])
  // onWindowScroll() {
  //   this.isScrolled = window.scrollY > 0; // Vérifie si la page a été défilée
  // }

  // handleScroll() {
  //   window.addEventListener('scroll', () => {
  //     if (!this.isHomePage) {
  //       this.isScrolled = window.scrollY > 10;
  //     } else {
  //       this.isScrolled = false;
  //     }
  //   });
  // }

