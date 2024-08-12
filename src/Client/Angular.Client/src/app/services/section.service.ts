import { Injectable } from '@angular/core';
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SectionService {
  private sectionSource = new BehaviorSubject<string>('');
  currentSection = this.sectionSource.asObservable();

  changeSection(section: string) {
    this.sectionSource.next(section);
  }
}
