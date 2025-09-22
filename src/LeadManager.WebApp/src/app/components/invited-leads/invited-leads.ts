import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { LeadService } from '../../services/lead.service';
import { Lead } from '../../models/lead.model';
import { LeadCardComponent } from '../lead-card/lead-card';

@Component({
  selector: 'app-invited-leads',
  standalone: true,
  imports: [CommonModule, HttpClientModule, LeadCardComponent],
  templateUrl: './invited-leads.html',
  styleUrls: ['./invited-leads.css']
})
export class InvitedLeadsComponent implements OnInit {
  leads: Lead[] = [];
  loading = false;
  error: string | null = null;

  constructor(private readonly leadService: LeadService) {}

  ngOnInit(): void {
    this.loadInvitedLeads();
  }

  loadInvitedLeads(): void {
    this.loading = true;
    this.error = null;

    this.leadService.getInvitedLeads().subscribe({
      next: (leads) => {
        this.leads = leads;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading invited leads:', error);
        this.error = 'Failed to load invited leads. Please try again.';
        this.loading = false;
      }
    });
  }

  onAcceptLead(leadId: number): void {
    this.leadService.acceptLead(leadId).subscribe({
      next: () => {
        this.leads = this.leads.filter((lead) => lead.id !== leadId);
      },
      error: (error) => {
        console.error('Error accepting lead:', error);
        this.error = 'Failed to accept lead. Please try again.';
      }
    });
  }

  onDeclineLead(leadId: number): void {
    this.leadService.declineLead(leadId).subscribe({
      next: () => {
        this.leads = this.leads.filter((lead) => lead.id !== leadId);
      },
      error: (error) => {
        console.error('Error declining lead:', error);
        this.error = 'Failed to decline lead. Please try again.';
      }
    });
  }

  trackByLeadId(index: number, lead: Lead): number {
    return lead.id;
  }
}
