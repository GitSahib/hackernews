export class Pageable<T> {
  content: T[];
  page: number;
  filtered: number;
  totalCount: number;
  pageSize: number;
  pageCount: number;
}

export class Request {
  type: string;
  page: number;
  pageSize: number;
  search: string
}
export class Story {
  id: number;
  title: number;
  link: string;
}
