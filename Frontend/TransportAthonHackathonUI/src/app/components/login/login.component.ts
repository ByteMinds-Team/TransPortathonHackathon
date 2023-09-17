import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { Router, RouterModule } from "@angular/router";
import { ToastrService } from 'ngx-toastr';
import { filter, tap } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { ValidationService } from 'src/app/utilities/validation-service.service';

const { required } = Validators

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    FormsModule,
    HttpClientModule
  ]
})
export class LoginComponent {
  private readonly formBuilder = inject(FormBuilder)
  private readonly authService = inject(AuthService)
  private readonly router = inject(Router)
  private readonly toastr = inject(ToastrService)
  private readonly validatonService = inject(ValidationService)

  loginForm = this.formBuilder.nonNullable.group({
    email: ["", [required, Validators.email]],
    password: ["", [required]]
  })
  errors: string[] = [];

  get email(): string {
    return this.loginForm.controls.email.value;
  }

  get password(): string {
    return this.loginForm.controls.password.value;
  }

  checkValid(key: string) {
    this.errors.push(
      this.validatonService.validate(
        this.loginForm.get(key)?.errors ?? { ok: true },
        key
      )
    );
  }


  login() {
    if (!this.loginForm.valid) {
      var formKeys = Object.keys(this.loginForm.value).filter(
        (key) => this.loginForm.get(key)?.errors !== null
      );
      formKeys.forEach((key) => this.checkValid(key));
      // this.toastr.error("Form is not valid", "Error")
      this.toastr.error(this.errors[0]);
      return;
    }
    
    this.authService.login({ email: this.email, password: this.password })
      .subscribe({
        next: _ => {
          this.router.navigateByUrl('')
        },
        error: err => {
          this.toastr.error("Email or password is wrong", "Error")
        }
      })
  }
}
