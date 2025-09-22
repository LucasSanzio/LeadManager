export interface Lead {
  id: number;
  contactFirstName: string;
  dateCreated: string;
  suburb: string;
  category: string;
  description: string;
  price: number;
}

export interface LeadAccepted extends Lead {
  contactLastName: string;
  contactEmail: string;
  contactPhone: string;
  contactFullName: string;
}

export interface LeadCreateRequest {
  contactFirstName: string;
  contactLastName: string;
  contactEmail: string;
  contactPhone: string;
  suburb: string;
  category: string;
  description: string;
  price: number;
}

