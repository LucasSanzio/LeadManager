import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Lead, LeadAccepted, LeadCreateRequest } from '../models/lead.model';

@Injectable({
  providedIn: 'root'
})
export class LeadService {
  private readonly apiUrl = 'http://localhost:5001/api/leads';

  constructor(private http: HttpClient) { }

  getInvitedLeads(): Observable<Lead[]> {
    return this.http.get<Lead[]>(`${this.apiUrl}/invited`);
  }

  getAcceptedLeads(): Observable<LeadAccepted[]> {
    return this.http.get<LeadAccepted[]>(`${this.apiUrl}/accepted`);
  }

  createLead(payload: LeadCreateRequest): Observable<Lead> {
    return this.http.post<Lead>(`${this.apiUrl}`, payload);
  }

  acceptLead(id: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/accept`, {});
  }

  declineLead(id: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/decline`, {});
  }
}

