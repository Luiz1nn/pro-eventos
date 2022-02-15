import { TestBed } from '@angular/core/testing';

import { PalestranteService } from './palestrante.service';

describe('PalestranteService', () => {
  let service: PalestranteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PalestranteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
