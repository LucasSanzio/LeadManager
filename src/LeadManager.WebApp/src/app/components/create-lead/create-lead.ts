import { Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LeadService } from '../../services/lead.service';
import { LeadCreateRequest } from '../../models/lead.model';

@Component({
  selector: 'app-create-lead',
  standalone: true,
  imports: [CommonModule, HttpClientModule, ReactiveFormsModule],
  templateUrl: './create-lead.html',
  styleUrls: ['./create-lead.css']
})
export class CreateLeadComponent implements OnDestroy {
  readonly leadForm: FormGroup;
  submitting = false;
  formError: string | null = null;
  formSuccess: string | null = null;

  private successMessageTimeout: ReturnType<typeof setTimeout> | null = null;

  constructor(private readonly fb: FormBuilder, private readonly leadService: LeadService) {
    this.leadForm = this.fb.group({
      contactFirstName: ['', [Validators.required, Validators.maxLength(100)]],
      contactLastName: ['', [Validators.required, Validators.maxLength(100)]],
      contactEmail: ['', [Validators.required, Validators.email, Validators.maxLength(200)]],
      contactPhone: ['', [Validators.required, Validators.maxLength(20)]],
      suburb: ['', [Validators.required, Validators.maxLength(100)]],
      category: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(1000)]],
      price: [null as number | null, [Validators.required, Validators.min(0)]]
    });
  }

  ngOnDestroy(): void {
    if (this.successMessageTimeout) {
      clearTimeout(this.successMessageTimeout);
      this.successMessageTimeout = null;
    }
  }

  onCreateLead(): void {
    if (this.leadForm.invalid) {
      this.leadForm.markAllAsTouched();
      return;
    }

    this.submitting = true;
    this.formError = null;

    const formValue = this.leadForm.getRawValue();

    const priceValue = typeof formValue.price === 'number'
      ? formValue.price
      : Number(formValue.price ?? 0);

    if (!Number.isFinite(priceValue)) {
      this.submitting = false;
      this.formError = 'Enter a valid price.';
      return;
    }

    const payload: LeadCreateRequest = {
      contactFirstName: (formValue.contactFirstName ?? '').trim(),
      contactLastName: (formValue.contactLastName ?? '').trim(),
      contactEmail: (formValue.contactEmail ?? '').trim(),
      contactPhone: (formValue.contactPhone ?? '').trim(),
      suburb: (formValue.suburb ?? '').trim(),
      category: (formValue.category ?? '').trim(),
      description: (formValue.description ?? '').trim(),
      price: Math.max(0, priceValue)
    };

    this.leadService.createLead(payload).subscribe({
      next: () => {
        this.leadForm.reset();
        this.leadForm.markAsPristine();
        this.leadForm.markAsUntouched();
        this.submitting = false;
        this.formError = null;
        this.formSuccess = 'Lead added successfully.';

        if (this.successMessageTimeout) {
          clearTimeout(this.successMessageTimeout);
        }

        this.successMessageTimeout = setTimeout(() => {
          this.formSuccess = null;
          this.successMessageTimeout = null;
        }, 4000);
      },
      error: (error) => {
        console.error('Error creating lead:', error);
        this.submitting = false;
        this.formError = 'Failed to create lead. Please try again.';
      }
    });
  }

  getControlInvalid(controlName: string): boolean {
    const control = this.leadForm.get(controlName);
    return !!control && control.touched && control.invalid;
  }

  getControlError(controlName: string, errorCode: string): boolean {
    const control = this.leadForm.get(controlName);
    return !!control && control.touched && control.hasError(errorCode);
  }
}
