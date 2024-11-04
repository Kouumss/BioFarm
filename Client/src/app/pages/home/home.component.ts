import { RouterLink } from "@angular/router";
import { CarouselVideoComponent } from "../../components/carousel-videos/carousel-video.component";
import { ProductBannerComponent } from "../../components/product-banner/product-banner.component";
import { FarmerBannerComponent } from "../../components/farmer-banner/farmer-banner.component";
import { CommitmentsComponent } from "../../components/commitments/commitments.component";
import { Component} from "@angular/core";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CarouselVideoComponent,
    ProductBannerComponent,
    RouterLink,
    FarmerBannerComponent,
    CommitmentsComponent
],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {


}
