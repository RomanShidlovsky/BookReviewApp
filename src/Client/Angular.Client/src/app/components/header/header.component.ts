import {AfterViewInit, Component, OnInit} from '@angular/core';
import {RouterLink, RouterLinkActive} from "@angular/router";
import {NgIf, NgOptimizedImage} from "@angular/common";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive,
    NgIf,
    NgOptimizedImage
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  isLogged: boolean;
  constructor(private authService: AuthService) {
    this.isLogged = authService.isLogged();
  }

  logOut() {
    this.authService.logOut();
    this.isLogged = false;
  }

  isLoggedIn() {
    return this.authService.isLogged();
  }

  getUserId() {
    return this.authService.getUserId();
  }
}
