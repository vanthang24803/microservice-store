using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Order.core.Context;

namespace Order.Controllers
{
    [ApiController]
    [Route("api/order/export")]
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
            var _orderData = GetOrderData();

            var _productData = GetProductData();

            using XLWorkbook wb = new();

            var sheet1 = wb.AddWorksheet(_orderData, "Order statistics");

            var sheet2 = wb.AddWorksheet(_productData, "Products Management");

            sheet1.Column(1).Width = 40;
            sheet1.Column(2).Width = 20;
            sheet1.Column(3).Width = 40;
            sheet1.Column(4).Width = 30;
            sheet1.Column(5).Width = 20;
            sheet1.Column(6).Width = 10;
            sheet1.Column(7).Width = 10;
            sheet1.Column(8).Width = 15;
            sheet1.Column(9).Width = 10;
            sheet1.Column(10).Width = 12;
            sheet1.Column(11).Width = 20;


            for (int i = 1; i <= 10; i++)
            {
                sheet2.Column(i).Width = 45;
            }


            using (MemoryStream ms = new())
            {
                wb.SaveAs(ms);
                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Order.xlsx");
            }
        }

        [NonAction]
        private DataTable GetOrderData()
        {
            DataTable dt = new()
            {
                TableName = "Order"
            };
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Customer", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("NumberPhone", typeof(string));
            dt.Columns.Add("Payment", typeof(string));
            dt.Columns.Add("Shipping", typeof(bool));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("TotalPrice", typeof(double));
            dt.Columns.Add("CreateAt" ,typeof(string));

            var _list = _context.Orders.ToList();

            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(
                        item.Id,
                        item.Name,
                        item.Address,
                        item.Email,
                        item.NumberPhone,
                        item.Payment,
                        item.Shipping,
                        item.Status,
                        item.Quantity,
                        item.TotalPrice,
                        item.CreateAt.ToString("dd/MM/yyyy HH:mm")
                    );
                });
            }

            return dt;

        }

        [NonAction]
        private DataTable GetProductData()
        {
            DataTable dt = new()
            {
                TableName = "Product"
            };
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("ProductId", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Thumbnail", typeof(string));
            dt.Columns.Add("Option", typeof(string));
            dt.Columns.Add("Price", typeof(int));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Sale", typeof(int));
            dt.Columns.Add("OrderId", typeof(string));

            var _list = _context.Products.ToList();

            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(
                        item.Id,
                        item.ProductId,
                        item.Name,
                        item.Thumbnail,
                        item.Option,
                        item.Price,
                        item.Quantity,
                        item.Sale,
                        item.OrderId
                    );
                });
            }

            return dt;

        }

    }
}