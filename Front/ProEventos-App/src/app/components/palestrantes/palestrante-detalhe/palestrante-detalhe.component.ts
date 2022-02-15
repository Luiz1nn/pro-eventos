import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { debounceTime, map, tap } from 'rxjs/operators';

import { Palestrante } from '@app/models/palestrante';
import { PalestranteService } from '@app/services/palestrante.service';

@Component({
  selector: 'app-palestrante-detalhe',
  templateUrl: './palestrante-detalhe.component.html',
  styleUrls: ['./palestrante-detalhe.component.scss']
})
export class PalestranteDetalheComponent implements OnInit {

  public form!: FormGroup;
  public situacaoDoForm = '';
  public corDaDescricao = '';

  constructor(
    private fb: FormBuilder,
    public palestranteService: PalestranteService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.validation();
    this.verificaForm();
    this.carregarPalestrante();
  }

  private validation(): void {
    this.form = this.fb.group({
      miniCurriculo: [''],
    });
  }

  private carregarPalestrante(): void {
    this.spinner.show();

    this.palestranteService
      .getPalestrante()
      .subscribe(
        (palestrante: Palestrante) => {
          this.form.patchValue(palestrante);
        },
        (error: any) => {
          this.toastr.error('Erro ao carregar o palestrante', 'Erro')
        }
      );
  }

  public get f(): any {
    return this.form.controls;
  }

  private verificaForm(): void {
    this.form.valueChanges
      .pipe(
        map(() => {
          this.situacaoDoForm = 'Minicurrículo está sendo atualizado!';
          this.corDaDescricao = 'text-warning';
        }),
        debounceTime(1000),
        tap(() => this.spinner.show())
      )
      .subscribe(() => {
        this.palestranteService
          .put({ ...this.form.value })
          .subscribe(
            () => {
              this.situacaoDoForm = 'Minicurrículo foi atualizado!';
              this.corDaDescricao = 'text-success';

              setTimeout(() => {
                this.situacaoDoForm = 'Minicurriculo foi carregado!';
                this.corDaDescricao = 'text-muted';
              }, 2000);
            },
            () => {
              this.toastr.error('Erro ao tentar atualizar Palestrante', 'Erro');
            }
          ).add(() => this.spinner.hide());
      });
  }
}
