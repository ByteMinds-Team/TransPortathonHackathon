import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    FormsModule,
    HttpClientModule
  ]
})
export class RegisterComponent {
  private readonly formBuilder = inject(FormBuilder)
  private readonly authService = inject(AuthService)
  private readonly router = inject(Router)
  private readonly toastr = inject(ToastrService)
  selectedFile: File | null = null;
  errors : string[] = [];
  registerForm = this.formBuilder.nonNullable.group({
    email: ["", [Validators.required, Validators.email]],
    password: ["", [Validators.required]],
    fullName: ["", [Validators.required]],
  })

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  register() {
    if (!this.registerForm.valid) {
      return;
    }
    if(this.selectedFile){

      this.authService.register(this.registerForm.value,this.selectedFile).subscribe({
        next: _ => {
          this.router.navigate(['/login'])
        }
      })

      
    }

   //TODO: Add register logic
      
  }
}
