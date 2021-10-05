using Pandora.Application.Command.Payments;
using Pandora.Application.ViewModel;
using Pandora.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Application.Contract
{
    public interface IPaymentService
    {
        Task CreateAsync(CreatePaymentCommand command, Guid userId);
        Task<Payment> GetById(Guid userId);
    }
}
