import {Component} from '@angular/core';
import {NgForOf, NgIf} from "@angular/common";

interface Contact {
  name: string;
  role: string;
  email: string;
  social: {
    telegram?: string;
    linkedin?: string;
  };
}

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [
    NgForOf,
    NgIf
  ],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent {
  contacts: Contact[] = [
    {
      name: 'Shidlovsky Roman',
      role: 'Developer',
      email: 'shidlovskyroman@gmail.com',
      social: {
        telegram: 'https://t.me/romanshidlovsky',
        linkedin: 'https://www.linkedin.com/in/shidlovskyroman'
      }
    },
    {
      name: 'Alice Johnson',
      role: 'Reviewer',
      email: 'alice.johnson@example.com',
      social: {
        linkedin: 'https://linkedin.com/in/alicejohnson'
      }
    }
  ];
}
