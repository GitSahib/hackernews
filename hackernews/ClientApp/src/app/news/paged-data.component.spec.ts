import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PagedDataComponent } from './paged-data.component';
import { HumanizePipe } from '../pipes/humanize.pipe';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

describe('PagedDataComponent', () => {
  let component: PagedDataComponent;
  let fixture: ComponentFixture<PagedDataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule, HttpClientModule, RouterModule.forRoot([])],
      declarations: [PagedDataComponent, HumanizePipe],
      providers: [{ provide: 'BASE_URL', useFactory: () => { return document.getElementsByTagName('base')[0].href;} }]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PagedDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
