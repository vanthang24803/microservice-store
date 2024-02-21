using System.Text;
using Order.core.Enum;
using Order.core.Models;

namespace Order.Core.Utils
{
    public class MailSend
    {

        public static string OrderMailSend(core.Models.Order order)
        {
            string htmlContent = "";

            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Core", "Templates", "order.html");
                htmlContent = File.ReadAllText(path);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            htmlContent = htmlContent.Replace("{ID}", order.Id.ToString());
            htmlContent = htmlContent.Replace("{CUSTOMER}", order.Name);
            htmlContent = htmlContent.Replace("{ADDRESS}", order.Address);
            htmlContent = htmlContent.Replace("{PHONE}", order.NumberPhone);
            htmlContent = htmlContent.Replace("{QUANTITY}", order.Quantity.ToString());
            htmlContent = htmlContent.Replace("{TOTAL_PRICE}", order.TotalPrice.ToString());
            htmlContent = htmlContent.Replace("{PAYMENT}", order.Payment.ToString());
            htmlContent = htmlContent.Replace("{STATUS}", order.Status.ToDescription());



            StringBuilder detailsBuilder = new();
            foreach (Product detail in order.Products)
            {
                detailsBuilder.Append("<tr>")
                    .Append("<td style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">").Append(detail.Name).Append("</td>")
                    .Append("<td style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">").Append(detail.Price).Append(" VNĐ</td>")
                    .Append("<td style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">").Append(detail.Option).Append("</td>")
                    .Append("<td style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">").Append(detail.Quantity).Append("</td>")
                    .Append("</tr>");
            }

            htmlContent = htmlContent.Replace("{DETAILS}", detailsBuilder.ToString());


            return htmlContent;
        }

    }

    public static class StatusExtensions
    {
        public static string ToDescription(this Status status)
        {
            switch (status)
            {
                case Status.PENDING:
                    return "Đang xác thực";
                case Status.CREATE:
                    return "Đã xác nhận";
                case Status.SHIPPING:
                    return "Đang vận chuyển";
                case Status.SUCCESS:
                    return "Giao thành công";
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }

}