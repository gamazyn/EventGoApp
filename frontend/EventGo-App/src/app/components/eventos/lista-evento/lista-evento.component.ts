import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { environment } from '@environments/environment';

@Component({
  selector: 'app-lista-evento',
  templateUrl: './lista-evento.component.html',
  styleUrls: ['./lista-evento.component.scss'],
})
export class ListaEventoComponent implements OnInit {
  public eventosFiltrados: Evento[] = [];

  public eventos: Evento[] = [];

  public eventoId: number = 0;

  public imgWidth: number = 150;

  public imgMargin: number = 2;

  public displayImg: boolean = true;

  private filteredList: string = '';

  modalRef?: BsModalRef;

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  public get filterList(): string {
    return this.filteredList;
  }

  public set filterList(value: string) {
    this.filteredList = value;
    this.eventosFiltrados = this.filteredList
      ? this.filterEvents(this.filterList)
      : this.eventos;
  }

  public filterEvents(filterBy: string): Evento[] {
    // eslint-disable-next-line no-param-reassign
    filterBy = filterBy.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: any) =>
        evento.tema.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  ngOnInit(): void {
    this.spinner.show();
    this.getEventos();
  }

  public changeVisualization(): void {
    this.displayImg = !this.displayImg;
  }

  public showImage(imageURL: string): string {
    return imageURL !== ''
      ? `${environment.apiURL}resources/images/${imageURL}`
      : 'assets/no-image.png';
  }

  public getEventos(): void {
    this.eventoService
      .getEventos()
      .subscribe({
        next: (eventos: Evento[]) => {
          this.eventos = eventos;
          this.eventosFiltrados = this.eventos;
        },
        error: () => {
          this.toastr.error('Erro ao carregar eventos', 'Erro!');
        },
      })
      .add(() => this.spinner.hide());
  }

  openModal(event: any, template: TemplateRef<any>, eventoId: number): void {
    event.stopPropagation();
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.eventoService
      .deleteEvento(this.eventoId)
      .subscribe({
        next: (result: any) => {
          if (result.message === 'Evento deletado') {
            this.toastr.success(
              'O Evento foi deletado com sucesso!',
              'Deletado'
            );
            this.getEventos();
          }
        },
        error: (error: any) => {
          console.error(error);
          this.toastr.error(
            `Erro ao tentar deletar o evento ${this.eventoId}`,
            'Erro!'
          );
        },
      })
      .add(() => this.spinner.hide());
  }

  decline(): void {
    this.modalRef?.hide();
  }

  detalheEvento(id: number): void {
    this.router.navigate([`eventos/detalhe/${id}`]);
  }
}
