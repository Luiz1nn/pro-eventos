import { TestBed } from '@angular/core/testing';

import { RedeSocialService } from './rede-social.service';

describe('RedeSocialService', () => {
  let service: RedeSocialService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RedeSocialService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
