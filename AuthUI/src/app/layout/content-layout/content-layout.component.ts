import { Component, OnInit } from '@angular/core';
import { AuthStateService } from '../../core/services/auth-state.service';

@Component({
  selector: 'app-content-layout',
  standalone: false,
  templateUrl: './content-layout.component.html',
  styleUrls: ['./content-layout.component.css']
})
export class ContentLayoutComponent implements OnInit {
  isAuthenticated = false;

  constructor(
    private authStateService: AuthStateService,
  ) { }

  ngOnInit(): void {
    this.authStateService.isAuthenticated$.subscribe(
      isAuthenticated => this.isAuthenticated = isAuthenticated
    );
  }
}