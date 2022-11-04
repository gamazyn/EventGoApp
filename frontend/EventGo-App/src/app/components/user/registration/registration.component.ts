import { Component, OnInit } from '@angular/core';
import {
  AbstractControlOptions,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { FieldValidator } from '@app/helpers/FieldValidator';
import { User } from '@app/models/identity/User';
import { AccountService } from '@app/services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent implements OnInit {
  user = {} as User;

  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.validation();
  }

  private validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: FieldValidator.MustMatch('password', 'confirmPassword'),
    };

    this.form = this.fb.group(
      {
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        userName: [
          '',
          [
            Validators.required,
            Validators.minLength(4),
            Validators.maxLength(30),
          ],
        ],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(20),
          ],
        ],
        confirmPassword: ['', Validators.required],
      },
      formOptions
    );
  }

  // eslint-disable-next-line class-methods-use-this
  public cssValidator(fieldName: FormControl): any {
    return {
      'is-invalid': fieldName.errors && fieldName.touched,
    };
  }

  public register(): void {
    this.user = { ...this.form.value };
    this.accountService.register(this.user).subscribe({
      next: () => this.router.navigateByUrl('/dashboard'),
      error: (err: any) => console.error(err),
    });
  }
}
