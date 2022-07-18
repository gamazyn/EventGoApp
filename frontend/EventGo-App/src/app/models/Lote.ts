// eslint-disable-next-line import/no-cycle
import { Evento } from './Evento';

export interface Lote {
  id: number;
  nome: string;
  preco: number;
  dataInicio?: Date;
  dataFim?: Date;
  quantidade: number;
  eventoId: number;
  evento: Evento;
}
