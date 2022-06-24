using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Application.CheckPaymentManager
{
    public class ResultCheckPaymentModel
    {
        public decimal PaymentAmount { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public bool IsConfirmed { get; set; }
    }
}