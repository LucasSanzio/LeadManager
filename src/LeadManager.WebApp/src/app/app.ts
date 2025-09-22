import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { InvitedLeadsComponent } from './components/invited-leads/invited-leads';
import { AcceptedLeadsComponent } from './components/accepted-leads/accepted-leads';
import { CreateLeadComponent } from './components/create-lead/create-lead';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    HttpClientModule,
    CommonModule,
    InvitedLeadsComponent,
    AcceptedLeadsComponent,
    CreateLeadComponent
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Lead Manager');
  protected readonly activeTab = signal<'invited' | 'accepted' | 'create'>('invited');

  setActiveTab(tab: 'invited' | 'accepted' | 'create') {
    this.activeTab.set(tab);
  }
}
