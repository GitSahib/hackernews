import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { Pageable, Story } from './../models/models';
@Component({
  selector: 'paged-data',
  templateUrl: './paged-data.component.html'
})
export class PagedDataComponent {
  private request: Object;
  private http: HttpClient;
  private baseUrl: string;
  private fetchData(): void {
    this.loadingData = true;
    this.request = {
      type: this.activatedRoute.snapshot.paramMap.get('type'),
      pageSize: +this.selectedSize || this.request && this.request['pageSize'] || 10,
      search: this.search,
      page: this.request['page']
    };
    this.http.post<Pageable<Story>>(this.baseUrl + 'story', this.request).subscribe((result) => {
      this.data = result;
      this.calculatePageCounts();
    }, error => console.error(error), () => this.loadingData = false);
  }
  private calculatePageCounts() {
    this.maxRows = this.data.page * this.data.pageSize + this.data.pageSize;
    this.maxRows = this.maxRows > this.data.filtered ? this.data.filtered : this.maxRows;
    this.fromRow = this.data.page * this.data.pageSize + 1;
  }

  public lengthMenu: string[] = ["10", "25", "50"];
  public selectedSize: string = "10";
  public search: string;
  public data: Pageable<Story>;
  public loadingData: boolean;
  public maxRows: number;
  public fromRow: number;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private activatedRoute: ActivatedRoute, router: Router) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.request = {
      page: 0
    };
    router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.request['page'] = 0;
        this.fetchData();
      }
    })
  }

  public searchChanged() {
    this.request['search'] = this.search;
    this.request['page'] = 0;
    this.fetchData();
  }
  public loadPage(page: number, selectedSize: string): void {
    if (this.data.page == page && this.request['pageSize'] == selectedSize)
      return;
    else if ((selectedSize && this.request['pageSize'] != selectedSize) || page < 0) {
      page = 0;
    }
    this.request['page'] = page;
    this.fetchData();
  }
}

