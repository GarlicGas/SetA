using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetA.Shared.Domain
{
    public class Expenses : BaseDomainModel
    {
        public decimal Amount { get; set; }
        public int PaymentMethodId { get; set; }
        public virtual List<PaymentMethod> PaymentMethods { get; set; }
    }
}
