
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-carousel-video',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './carousel-video.component.html',
  styleUrls: ['./carousel-video.component.scss']
})
export class CarouselVideoComponent {
  @ViewChild('myVideo') myVideo!: ElementRef<HTMLVideoElement>;

  ngAfterViewInit(): void {
    this.handleVideo();
  }

  videos = [
    'assets/videos/video-farm.mp4',
    'assets/videos/video-farm2.mp4',
    'assets/videos/video-farm3.mp4',
    'assets/videos/video-farm4.mp4'
  ];

  handleVideo() {
    const video = this.myVideo.nativeElement;
    video.muted = true;
    video.autoplay = true;
    video.loop = true;

    // Lance la vidéo au cas où autoplay a besoin d'être forcé
    video.play().catch(error => {
      console.error('Autoplay failed:', error);
    });

    this.myVideo.nativeElement.playbackRate = 0.5; // Ralentir par 3
  }
}
