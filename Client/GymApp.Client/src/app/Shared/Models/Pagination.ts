export type Pagination<T> = {
  pagenumber: number;
  pagesize: number;
  total: number;
  source: T[];
};
