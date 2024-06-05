import { Component } from '@angular/core';
import {NgForOf} from "@angular/common";

interface FAQ {
  question: string;
  answer: string;
}

@Component({
  selector: 'app-faq',
  standalone: true,
  imports: [
    NgForOf
  ],
  templateUrl: './faq.component.html',
  styleUrl: './faq.component.css'
})
export class FaqComponent {
  faqs: FAQ[] = [
    {
      question: 'How do I submit a book review?',
      answer: 'To submit a book review, click on the "Submit Review" button on the book\'s page and fill out the form.'
    },
    {
      question: 'Can I edit my review after submitting it?',
      answer: 'Yes, you can edit your review by navigating to your profile, finding the review, and clicking the "Edit" button.'
    },
    {
      question: 'How do I delete my review?',
      answer: 'To delete a review, go to your profile, find the review you want to delete, and click the "Delete" button.'
    },
    {
      question: 'What should I include in my review?',
      answer: 'Your review should include your thoughts on the book\'s content, writing style, and your overall impression. Avoid spoilers.'
    },
    {
      question: 'How are reviews moderated?',
      answer: 'Reviews are moderated by our team to ensure they follow our community guidelines and do not contain inappropriate content.'
    }
  ];
}
