import { CommonModule } from '@angular/common';
import { Component, HostListener } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon'
import { NavigationEnd, Router, RouterLink, RouterLinkActive } from '@angular/router';

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

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.checkRoute();
    this.handleScroll();
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  checkRoute() {
    this.router.events.subscribe(() => {
      this.isHomePage = this.router.url === '/home' || this.router.url === '/';
    });
  }

  handleScroll() {
    window.addEventListener('scroll', () => {
      if (!this.isHomePage) {
        this.isScrolled = window.scrollY > 10;
      } else {
        this.isScrolled = false;
      }
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

