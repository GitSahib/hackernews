export class Pageable<T> {
  content: T[];
  page: number;
  totalCount: number;
  pageSize: number;
  pageCount: number;
}

export class Story {
  id: number;
  title: number;
  link: string;
}
