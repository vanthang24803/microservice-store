using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Product.Context;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/product/export")]
    public class ExportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public ActionResult ExportExcel()
        {
            var _productData = GetProductData();

            var _categoryData = GetCategoryData();

            var _optionData = GetOptionData();

            var _imageData = GetImageData();

            using XLWorkbook wb = new();

            var sheet1 = wb.AddWorksheet(_productData, "Products Management");

            var sheet2 = wb.AddWorksheet(_categoryData, "Categories Management");

            var sheet3 = wb.AddWorksheet(_optionData, "Options Management");

            var sheet4 = wb.AddWorksheet(_imageData, "Image Managements");

            sheet1.Column(1).Width = 40;
            sheet1.Column(2).Width = 30;
            sheet1.Column(3).Width = 50;
            sheet1.Column(4).Width = 10;
            sheet1.Column(5).Width = 25;

            sheet2.Column(1).Width = 40;
            sheet2.Column(2).Width = 25;
            sheet2.Column(3).Width = 25;

            sheet3.Column(1).Width = 40;
            sheet3.Column(2).Width = 30;
            sheet3.Column(3).Width = 20;
            sheet3.Column(4).Width = 20;
            sheet3.Column(5).Width = 20;
            sheet3.Column(6).Width = 20;
            sheet3.Column(7).Width = 60;
            sheet3.Column(8).Width = 25;

            sheet4.Column(1).Width = 40;
            sheet4.Column(2).Width = 50;
            sheet4.Column(3).Width = 50;
            sheet4.Column(4).Width = 25;

            using MemoryStream ms = new();
            wb.SaveAs(ms);
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Product.xlsx");
        }

        [NonAction]

        private DataTable GetProductData()
        {
            DataTable dt = new()
            {
                TableName = "Product"
            };

            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Thumbnail", typeof(string));
            dt.Columns.Add("Brand", typeof(string));
            dt.Columns.Add("CreateAt", typeof(string));

            var _list = _context.Books.ToList();

            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(
                        item.Id,
                        item.Name,
                        item.Thumbnail,
                        item.Brand,
                        item.CreateAt.ToString("dd/MM/yyyy HH:mm")
                    );
                });
            }

            return dt;
        }


        [NonAction]

        private DataTable GetCategoryData()
        {
            DataTable dt = new()
            {
                TableName = "Category"
            };

            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("CreateAt", typeof(string));

            var _list = _context.Categories.ToList();

            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(
                        item.Id,
                        item.Name,
                        item.CreateAt.ToString("dd/MM/yyyy HH:mm")
                    );
                });
            }

            return dt;
        }

        [NonAction]

        private DataTable GetOptionData()
        {
            DataTable dt = new()
            {
                TableName = "Option"
            };

            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Sale", typeof(int));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Price", typeof(double));
            dt.Columns.Add("Status", typeof(bool));
            dt.Columns.Add("ProductId", typeof(string));
            dt.Columns.Add("CreateAt", typeof(string));

            var _list = _context.Options.ToList();

            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(
                        item.Id,
                        item.Name,
                        item.Sale,
                        item.Quantity,
                        item.Price,
                        item.Status,
                        item.BookId,
                        item.CreateAt.ToString("dd/MM/yyyy HH:mm")
                    );
                });
            }

            return dt;
        }

        [NonAction]

        private DataTable GetImageData()
        {
            DataTable dt = new()
            {
                TableName = "Image"
            };

            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Url", typeof(string));
            dt.Columns.Add("ProductId", typeof(string));
            dt.Columns.Add("CreateAt", typeof(string));

            var _list = _context.Images.ToList();

            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(
                        item.Id,
                        item.Url,
                        item.BookId,
                        item.CreateAt.ToString("dd/MM/yyyy HH:mm")
                    );
                });
            }

            return dt;
        }
    }
}