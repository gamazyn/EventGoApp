import { Component, OnInit, TemplateRef } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Evento } from '@app/models/Evento';
import { Lote } from '@app/models/Lote';
import { EventoService } from '@app/services/evento.service';
import { LoteService } from '@app/services/lote.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-detalhe-evento',
  templateUrl: './detalhe-evento.component.html',
  styleUrls: ['./detalhe-evento.component.scss'],
})
export class DetalheEventoComponent implements OnInit {
  eventoId!: number;

  evento = {} as Evento;

  modalRef: BsModalRef;

  form!: FormGroup;

  saveState: any = 'post';

  currentLote = { id: 0, nome: '', index: 0 };

  get editmode(): boolean {
    return this.saveState === 'put';
  }

  get lotes(): FormArray {
    return this.form.get('lotes') as FormArray;
  }

  get f(): any {
    return this.form.controls;
  }

  get bsconfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }

  get bsconfigLote(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activatedRouter: ActivatedRoute,
    private eventoService: EventoService,
    private loteService: LoteService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private router: Router
  ) {
    this.localeService.use('pt-br');
  }

  public loadEvent(): void {
    this.eventoId = +this.activatedRouter.snapshot.paramMap.get('id');

    if (this.eventoId !== null || this.eventoId === 0) {
      this.saveState = 'put';

      this.spinner.show();

      this.eventoService
        .getEventoById(this.eventoId)
        .subscribe({
          next: (evento: Evento) => {
            this.evento = { ...evento };
            this.form.patchValue(this.evento);
            this.evento.lotes.forEach((lote) => {
              this.lotes.push(this.createLote(lote));
            });
          },
          error: (error: any) => {
            console.error(error);
            this.toastr.error('Erro ao tentar carregar evento.', 'Erro!');
          },
        })
        .add(() => this.spinner.hide());
    }
  }

  ngOnInit(): void {
    this.loadEvent();
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      local: ['', [Validators.required, Validators.minLength(4)]],
      dataEvento: ['', Validators.required],
      tema: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50),
        ],
      ],
      qtdPessoas: ['', [Validators.required, Validators.max(10000)]],
      imagemURL: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      lotes: this.fb.array([]),
    });
  }

  addLote(): void {
    this.lotes.push(this.createLote({ id: 0 } as Lote));
  }

  createLote(lote: Lote): FormGroup {
    return this.fb.group({
      id: [lote.id],
      nome: [lote.nome, Validators.required],
      quantidade: [lote.quantidade, Validators.required],
      preco: [lote.preco, Validators.required],
      dataInicio: [lote.dataInicio],
      dataFim: [lote.dataFim],
    });
  }

  removeLote(index: number, template: TemplateRef<any>): void {
    this.currentLote.id = this.lotes.get(index + '.id').value;
    this.currentLote.nome = this.lotes.get(index + '.nome').value;
    this.currentLote.index = index;

    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirmDeleteLote(): void {
    this.modalRef.hide();
    this.spinner.show();

    this.loteService
      .deleteLote(this.eventoId, this.currentLote.id)
      .subscribe({
        next: () => {
          this.toastr.success('Lote deletado com sucesso!', 'Sucesso!');
          this.lotes.removeAt(this.currentLote.index);
        },
        error: (err: any) => {
          this.toastr.error(
            `Erro ao tentar deletar lote ${this.currentLote.id}`,
            'Erro!'
          );
          console.error(err);
        },
      })
      .add(() => this.spinner.hide());
  }

  declineDeleteLote(): void {
    this.modalRef.hide();
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(fieldName: FormControl | AbstractControl | null): any {
    return {
      'is-invalid': fieldName?.errors && fieldName?.touched,
    };
  }

  public saveEvento(): void {
    this.spinner.show();
    if (this.form.valid) {
      this.evento =
        this.saveState === 'post'
          ? { ...this.form.value }
          : { id: this.evento.id, ...this.form.value };

      this.eventoService[this.saveState](this.evento)
        .subscribe({
          next: (returnedEvent: Evento) => {
            this.toastr.success('Evento salvo com sucesso!', 'Evento Salvo!');
            this.router.navigate([`eventos/detalhe/${returnedEvent.id}`]);
          },
          error: (err: any) => {
            this.toastr.error('Erro ao tentar salvar evento.', 'Erro!');
            console.error(err);
          },
        })
        .add(() => this.spinner.hide());
    }
  }

  public saveLotes(): void {
    this.spinner.show();

    if (this.form.controls['lotes'].valid) {
      this.loteService
        .saveLote(this.eventoId, this.form.value.lotes)
        .subscribe({
          next: () => {
            this.toastr.success('Lotes salvos com sucesso!', 'Salvo!');
            this.lotes.reset();
          },
          error: (err: any) => {
            this.toastr.error('Erro ao tentar salvar lotes.', 'Erro!');
            console.error(err);
          },
        })
        .add(() => this.spinner.hide());
    }
  }
}
