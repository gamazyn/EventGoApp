// eslint-disable-next-line import/no-cycle
import { Evento } from './Evento';
import { RedeSocial } from './RedeSocial';

export interface Organizador {
  id: number;
  nome: string;
  miniBio: string;
  imagemURL: string;
  telefone: string;
  email: string;
  redeSociais: RedeSocial[];
  organizadoresEventos: Evento[];
}
