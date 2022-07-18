/* eslint-disable import/no-cycle */
import { RedeSocial } from './RedeSocial';
import { Lote } from './Lote';
import { Organizador } from './Organizador';

export interface Evento {
  id: number;
  local: string;
  dataEvento?: Date;
  tema: string;
  qtdPessoas: number;
  imagemURL: string;
  telefone: string;
  email: string;
  lotes: Lote[];
  redeSociais: RedeSocial[];
  organizadoresEventos: Organizador[];
}
