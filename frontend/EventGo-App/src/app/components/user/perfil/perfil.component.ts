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
import { UserUpdate } from '@app/models/identity/UserUpdate';
import { AccountService } from '@app/services/account.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss'],
})
export class PerfilComponent implements OnInit {
  userUpdate = {} as UserUpdate;

  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  onSubmit(): void {
    this.updateUser();
  }

  public updateUser() {
    this.userUpdate = { ...this.form.value };
    this.spinner.show();

    this.accountService
      .updateUser(this.userUpdate)
      .subscribe({
        next: () =>
          this.toastr.success('Usuário atualizdo com sucesso!', 'Atualizado'),
        error: (err: any) => {
          this.toastr.error(err.error);
          console.error(err);
        },
      })
      .add(() => this.spinner.hide());
  }

  constructor(
    public fb: FormBuilder,
    public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit() {
    this.validation();
    this.loadUser();
  }

  private loadUser(): void {
    this.spinner.show();
    this.accountService
      .getUser()
      .subscribe({
        next: (returnedUser: UserUpdate) => {
          console.log(returnedUser);
          this.userUpdate = returnedUser;
          this.form.patchValue(this.userUpdate);
          this.toastr.success('Usuário Carregado', 'Sucesso');
        },
        error: (err: any) => {
          console.error(err);
          this.toastr.error('Usuário não carregado', 'Erro');
          this.router.navigate(['/dashboard']);
        },
      })
      .add(() => this.spinner.hide());
  }

  private validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: FieldValidator.MustMatch('password', 'confirmPassword'),
    };

    this.form = this.fb.group(
      {
        userName: [''],
        title: ['NaoInformado', Validators.required],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        phoneNumber: ['', Validators.required],
        function: ['NaoInformado', Validators.required],
        description: ['', [Validators.required, Validators.maxLength(300)]],
        password: [
          '',
          [
            Validators.nullValidator,
            Validators.minLength(4),
            Validators.maxLength(20),
          ],
        ],
        confirmPassword: ['', Validators.nullValidator],
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
}
