using System.Security.Claims;
using System.Text;
using API.DTOs;
using API.Interfaces.Services;
using ClosedXML.Excel;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrdersForUser()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orders = await _service.GetAllOrdersForUser(username);
            return Ok(orders);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            var orders = await _service.GetAllOrders();
            return Ok(orders);
        }
        [HttpPut("update-status-to-finished/{orderId}")]
        public async Task<ActionResult> UpdateOrderStatus(int orderId)
        {
            await _service.UpdateOrderStatusToFinished(orderId);
            return Ok();
        }
        [HttpGet("download-invoice/{id}")]
        public async Task<ActionResult> DownloadInvoice(int id)
        {
            var order = await _service.GetOrderById(id);
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Template.docx");
            var document = DocumentModel.Load(templatePath);
            document.Content.Replace("{{OrderNumber}}", order.Id.ToString());
            document.Content.Replace("{{OrderDate}}", order.DateCreated.ToString());
            StringBuilder sb = new();
            foreach (var item in order.ShoppingCart.CartItems)
            {
                sb.AppendLine("Food Name: " + item.Food.Name + " Quantity: " + item.Quantity + " Subtotal:" + item.Subtotal + "\n");
            }
            document.Content.Replace("{{Foods}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", order.ShoppingCart.TotalPrice.ToString());

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "OrderInvoice.pdf");
        }
        [HttpGet("orders-report")]
        public async Task<ActionResult> ExportToExcel()
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "Orders.xlsx";
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                        workbook.Worksheets.Add("Orders");
                    worksheet.Cell(1, 1).Value = "Status";
                    worksheet.Cell(1, 2).Value = "User";
                    worksheet.Cell(1, 3).Value = "Address";
                    worksheet.Cell(1, 4).Value = "Phone Number";
                    worksheet.Cell(1, 5).Value = "Total Price";
                    worksheet.Cell(1, 6).Value = "Products";
                    var i = 2;
                    var orders = await _service.GetAllOrders();
                    foreach (var order in orders)
                    {
                        worksheet.Cell(i, 1).Value = order.OrderStatus;
                        worksheet.Cell(i, 2).Value = order.AppUserName;
                        worksheet.Cell(i, 3).Value = order.Address;
                        worksheet.Cell(i, 4).Value = order.PhoneNumber;
                        worksheet.Cell(i, 5).Value = order.ShoppingCart.TotalPrice;
                        StringBuilder sb = new();
                        foreach (var item in order.ShoppingCart.CartItems)
                        {
                            sb.AppendLine(item.Food.Name + " -> " + item.Quantity + "x" + item.Food.Price + "MKD = " + item.Subtotal + "MKD\n");
                        }
                        sb.AppendLine("Total Price: " + order.ShoppingCart.TotalPrice + "MKD");
                        worksheet.Cell(i, 6).Value = String.Join(',', sb.ToString());
                        i++;
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }

                }
            }
            catch (Exception ex) { }
            return BadRequest();
        }

    }
}