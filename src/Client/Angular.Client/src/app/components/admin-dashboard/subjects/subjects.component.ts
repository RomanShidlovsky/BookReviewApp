import {Component} from '@angular/core';
import {SubjectResponseDto} from "../../../models/subject/subjectResponseDto";
import {SubjectsService} from "../../../api/subjects.service";
import {SubjectLineComponent} from "./subject-line/subject-line.component";
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-subjects',
  standalone: true,
  imports: [
    SubjectLineComponent,
    NgForOf
  ],
  templateUrl: './subjects.component.html',
  styleUrl: './subjects.component.css'
})
export class SubjectsComponent {
  subjects!: SubjectResponseDto[]
  constructor(private subjectsService: SubjectsService) {
  }

  ngOnInit(): void {
    this.subjectsService.apiSubjectsGet().subscribe({
      next: value => {
        this.subjects = value;
      },
      error: err => {
        console.log(err)
      }
    });
  }

  onSubjectDeleted(id: number) {
    this.subjects = this.subjects.filter(subjects => subjects.id != id);
  }
}
