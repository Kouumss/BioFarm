export type Pagination<T> =

{
  pageIndex: number;
  size: number,
  pageCount: number;
  data: T[];
}
