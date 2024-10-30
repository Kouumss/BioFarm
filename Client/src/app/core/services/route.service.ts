import { Injectable } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RouteService {
  private isHomePageSubject = new BehaviorSubject<boolean>(false);
  isHomePage$ = this.isHomePageSubject.asObservable();

  constructor(private router: Router) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.isHomePageSubject.next(['/home', '/'].includes(this.router.url));
      }
    });
  }
}
