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
    this.http.post<Pageable<Story>>(this.baseUrl + 'story', this.request).subscribe((result) => {
      this.data = result;
    }, error => console.error(error), () => this.loadingData = false);
  }
  public lengthMenu: string[] = ["10", "25", "50"];
  public selectedSize: string = "10";
  public search: string;
  public data: Pageable<Story>;
  public loadingData: boolean;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private activatedRoute: ActivatedRoute, router: Router) {
    this.http = http;
    this.baseUrl = baseUrl;
    router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.request = {
          type: this.activatedRoute.snapshot.paramMap.get('type'),
          pageSize: this.request && this.request['pageSize'] || 10,
          search: this.search
        };
        this.fetchData();
      }
    })
  }

  public searchChanged() {
    this.request['search'] = this.search;
    this.fetchData();
  }
  public loadPage(page: number, selectedSize: string): void {
    if (this.data.page == page && this.request['pageSize'] == selectedSize) return;
    else if (page >= this.data.pageCount) {
      page = this.data.pageCount - 1;
    } else if (page < 0) {
      page = 0;
    }
    this.request['page'] = page;
    if (selectedSize) { 
      this.request['pageSize'] = +selectedSize;
    }

    this.fetchData();
  }
}

