import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AccountService } from '../../core/account.service';
import { Router, RouterLink, RouterLinkActive } from "@angular/router";
import { ToastService } from '../../core/services/toast.service';
import { BusyService } from '../../core/services/busy.service';

@Component({
  selector: 'app-nav',
  imports: [CommonModule, FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  protected busyService = inject(BusyService);
  protected accountService = inject(AccountService);
  private router = inject(Router);
  private toast = inject(ToastService);

  protected credentials: any = {};

  login() {
    this.accountService.login(this.credentials).subscribe({
      next: () => {
        this.router.navigateByUrl('/members');
        this.credentials = {};
        this.toast.success('Logged in Successfully');
      },
      error: error => {
        this.toast.error(error.error);
      }
    });
  }

  logout() {
    this.router.navigateByUrl('/');
    this.accountService.logout();
  }
}
