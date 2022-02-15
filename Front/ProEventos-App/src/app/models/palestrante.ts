import { Evento } from './evento';
import { UserUpdate } from './identity/user-update';
import { RedeSocial } from './rede-social';

export interface Palestrante {
  id: number;
  miniCurriculo: string;
  user: UserUpdate
  redeSociais: RedeSocial[];
  palestrantesEventos: Evento[];
}
