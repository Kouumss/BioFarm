export type Pagination<T> =

{
  pageIndex: number;
  size: number,
  count: number;
  data: T[];
}
