import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-carousel',
  standalone: true,
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.scss']
})
export class CarouselComponent {
  @ViewChild('myVideo') myVideo!: ElementRef<HTMLVideoElement>;

  ngAfterViewInit(): void {
    this.myVideo.nativeElement.playbackRate = 0.5; // Ralentir par 3
  }

  videos = [
    'assets/videos/video-farm.mp4',
    'assets/videos/video-farm2.mp4',
    'assets/videos/video-farm3.mp4',
    'assets/videos/video-farm4.mp4'
  ];
}
