import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../services/company.service';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrl: './company.component.css'
})
export class CompanyComponent implements OnInit {

  companies: any;

  constructor(private compService: CompanyService) {}

  ngOnInit(): void {
    this.compService.getCompanyData().subscribe(response => {
      this.companies = response;
    }, error => {
      console.error('Data fetch error', error);
    });
  }
}