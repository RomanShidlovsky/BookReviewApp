import { Component } from '@angular/core';
import {SectionService} from "../../../services/section.service";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SideBarComponent {
  constructor(private sectionService: SectionService) {}

  selectSection(section: string) {
    this.sectionService.changeSection(section);
  }
}
