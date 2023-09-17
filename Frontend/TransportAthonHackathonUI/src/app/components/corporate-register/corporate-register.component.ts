import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule, FormBuilder, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-corporate-register',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    FormsModule,
    HttpClientModule
  ],
  templateUrl: './corporate-register.component.html',
  styleUrls: ['./corporate-register.component.scss']
})
export class CorporateRegisterComponent {
  private readonly formBuilder = inject(FormBuilder)
  private readonly authService = inject(AuthService)
  private readonly router = inject(Router)
  private readonly toastr = inject(ToastrService)
  selectedFile: File | null = null;
  errors: string[] = [];
  registerForm = this.formBuilder.nonNullable.group({
    email: ["", [Validators.required, Validators.email]],
    password: ["", [Validators.required]],
    fullName: ["", [Validators.required]],
    companyName: ["", [Validators.required]]
  })

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  register() {
    if (!this.registerForm.valid) {
      return;
    }
    if (this.selectedFile) {

      this.authService.corporateRegister(this.registerForm.value, this.selectedFile).subscribe({
        next: _ => {
          this.router.navigate(['/login'])
        }
      })
    }
  }
}
