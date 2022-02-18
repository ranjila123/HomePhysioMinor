using HomePhysio.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Services.Payment
{
    public interface IPaymentService
    {
        Task<KhaltiResponseVM> KhaltiPay(PayloadPostVM payloadPostVM);
    }
}
