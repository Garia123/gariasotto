import { TestBed } from '@angular/core/testing';

import { LodgingsService } from './lodgings.service';

describe('LodgingsService', () => {
  let service: LodgingsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LodgingsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
