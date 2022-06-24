using Pandora.Application.Command.Payments;
using Pandora.Application.ViewModel;
using Pandora.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pandora.Application.Scheduler;

namespace Pandora.Application.Contract
{
    public interface IPaymentService
    {
        Task<PaymentViewModel> CreateAsync(CreatePaymentCommand command, Guid userId, CheckPaymentConfirmationScheduler scheduler);
        Task<Payment> GetById(Guid userId);
    }
}
