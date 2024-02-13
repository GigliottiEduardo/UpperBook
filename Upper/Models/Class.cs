using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UpperSLN.Models
{
    [Keyless]
    [Table("Pager")]
    public class Pager
    {
        [Column("TotalItems")]
        [Display(Name = "TotalItems")]
        public int TotalItems { get; set; }

        [Column("CurrentPage")]
        [Display(Name = "CurrentPage")]
        public int CurrentPage { get; set; }

        [Column("PageSize")]
        [Display(Name = "PageSize")]
        public int PageSize { get; set; }

        [Column("TotalPages")]
        [Display(Name = "TotalPages")]
        public int TotalPages { get; set; }

        [Column("StartPage")]
        [Display(Name = "StartPage")]
        public int StartPage { get; set; }

        [Column("EndPage")]
        [Display(Name = "EndPage")]
        public int EndPage { get; set; }

        public Pager() { }
        public Pager(int totalItems, int page, int pageSize = 10)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;

        }
    }
}