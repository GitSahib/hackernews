import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HumanizePipe } from './pipes/humanize.pipe';
import { PagedDataComponent } from './news/paged-data.component';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let nav: NavMenuComponent;
  let navFixture: ComponentFixture<NavMenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule, RouterModule.forRoot([])],
      declarations: [AppComponent, NavMenuComponent, PagedDataComponent,  HumanizePipe]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    navFixture = TestBed.createComponent(NavMenuComponent);
    component = fixture.componentInstance;
    nav = navFixture.componentInstance;
    navFixture.detectChanges();
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it("should have menue", () => {
    expect(nav).toBeTruthy();
  });
});
