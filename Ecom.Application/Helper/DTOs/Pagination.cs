using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.Application.Helper.DTOs
{
    public class Pagination<T> where T:class
    {
        public Pagination(int pageIndex, int pageSize,int count,  IReadOnlyList<T>? data)
        {
            this.PageIndex = pageIndex;
            this.Count = count;
            PageSize = pageSize;
            Data = data;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T>? Data { get; set; }
    }
}