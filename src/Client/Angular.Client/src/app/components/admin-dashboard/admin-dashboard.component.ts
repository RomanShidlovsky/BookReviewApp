import {Component} from '@angular/core';
import {SideBarComponent} from "./sidebar/sidebar.component";
import {ContentComponent} from "./content/content.component";

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [
    SideBarComponent,
    SideBarComponent,
    ContentComponent
  ],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css'
})
export class AdminDashboardComponent {

}
