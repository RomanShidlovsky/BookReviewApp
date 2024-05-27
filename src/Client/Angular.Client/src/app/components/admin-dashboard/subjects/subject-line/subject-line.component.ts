import {Component, EventEmitter, Input, Output} from '@angular/core';
import {SubjectsService} from "../../../../api/subjects.service";
import {SubjectResponseDto} from "../../../../models/subject/subjectResponseDto";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-subject-line',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './subject-line.component.html',
  styleUrl: './subject-line.component.css'
})
export class SubjectLineComponent {
  @Input() subject!: SubjectResponseDto;
  @Output() onSubjectDeleted = new EventEmitter<number>();

  constructor(private subjectsService: SubjectsService) {
  }

  confirmDeletion() {
    const confirmed = confirm('Are you sure that you want to delete this item?');

    if (confirmed) {
      this.deleteLanguage();
    }
  }

  deleteLanguage() {
    this.subjectsService.apiSubjectsIdDelete(this.subject.id).subscribe({
      next: deleted => {
        if (deleted === true) {
          this.onSubjectDeleted.emit(this.subject.id);
        }
      }
    });
  }
}
