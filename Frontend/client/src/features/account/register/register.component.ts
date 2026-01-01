import { Component, inject, input, output } from '@angular/core';
import { RegisterCreds, User } from '../../../types/User';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../../core/account.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService);
  cancelRegister = output<boolean>();

  protected creds = {} as RegisterCreds;

  register() {
    this.accountService.register(this.creds).subscribe({
      next: (response) => {
        console.log(response);
        this.cancel();
      },
      error: (error) => console.error(error)
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
