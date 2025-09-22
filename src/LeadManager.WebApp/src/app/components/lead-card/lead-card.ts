import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Lead, LeadAccepted } from '../../models/lead.model';

@Component({
  selector: 'app-lead-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './lead-card.html',
  styleUrls: ['./lead-card.css']
})
export class LeadCardComponent {
  @Input() lead!: Lead | LeadAccepted;
  @Input() showActions: boolean = true;
  @Input() showContactDetails: boolean = false;
  
  @Output() accept = new EventEmitter<number>();
  @Output() decline = new EventEmitter<number>();

  onAccept() {
    this.accept.emit(this.lead.id);
  }

  onDecline() {
    this.decline.emit(this.lead.id);
  }

  isLeadAccepted(lead: Lead | LeadAccepted): lead is LeadAccepted {
    return 'contactLastName' in lead;
  }

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    if (Number.isNaN(date.getTime())) {
      return '';
    }

    const datePart = date.toLocaleDateString('en-US', {
      month: 'long',
      day: 'numeric'
    });

    const timePart = date
      .toLocaleTimeString('en-US', {
        hour: 'numeric',
        minute: '2-digit'
      })
      .replace('AM', 'am')
      .replace('PM', 'pm');

    const year = date.getFullYear();
    const currentYear = new Date().getFullYear();
    const withYear = year !== currentYear ? `, ${year}` : '';

    return `${datePart}${withYear} @ ${timePart}`;
  }

  formatPrice(price: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD'
    }).format(price);
  }
}
