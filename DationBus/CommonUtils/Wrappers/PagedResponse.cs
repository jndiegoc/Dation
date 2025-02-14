﻿namespace CommonUtils.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? TotalPages { get; set; }
        public int? TotalRecords { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize)
            : base(data)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 1 ? pageSize : 1;
        }
    }
}
