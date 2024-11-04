import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { Farmer } from '../../shared/models/farmer';

@Component({
  selector: 'app-farmer-banner',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule
  ],
  templateUrl: './farmer-banner.component.html',
  styleUrl: './farmer-banner.component.scss'
})
export class FarmerBannerComponent implements OnInit, AfterViewInit {

  @ViewChild('farmerCardsContainer', { static: false }) farmerCardsContainer!: ElementRef;

  title = "Meet Our Farmers";
  farmers: Farmer[] = [];

  constructor(private http: HttpClient) { }
  ngAfterViewInit(): void {
    throw new Error('Method not implemented.');
  }


  ngOnInit() {
    this.http.get<Farmer[]>('assets/seeds/farmers.json').subscribe({
      next: (response: Farmer[]) => this.farmers = response,
      error: error => console.error(error)
    });
  }
}
