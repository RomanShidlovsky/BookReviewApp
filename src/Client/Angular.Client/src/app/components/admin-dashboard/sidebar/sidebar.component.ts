import { Component } from '@angular/core';
import {SectionService} from "../../../services/section.service";

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SideBarComponent {
  constructor(private sectionService: SectionService) {}

  selectSection(section: string) {
    this.sectionService.changeSection(section);
  }
}
