import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { LeadService } from '../../services/lead.service';
import { LeadAccepted } from '../../models/lead.model';
import { LeadCardComponent } from '../lead-card/lead-card';

@Component({
  selector: 'app-accepted-leads',
  imports: [CommonModule, HttpClientModule, LeadCardComponent],
  templateUrl: './accepted-leads.html',
  styleUrl: './accepted-leads.css'
})
export class AcceptedLeadsComponent implements OnInit {
  leads: LeadAccepted[] = [];
  loading = false;
  error: string | null = null;

  constructor(private leadService: LeadService) {}

  ngOnInit() {
    this.loadAcceptedLeads();
  }

  loadAcceptedLeads() {
    this.loading = true;
    this.error = null;
    
    this.leadService.getAcceptedLeads().subscribe({
      next: (leads) => {
        this.leads = leads;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading accepted leads:', error);
        this.error = 'Failed to load accepted leads. Please try again.';
        this.loading = false;
      }
    });
  }

  trackByLeadId(index: number, lead: LeadAccepted): number {
    return lead.id;
  }
}

