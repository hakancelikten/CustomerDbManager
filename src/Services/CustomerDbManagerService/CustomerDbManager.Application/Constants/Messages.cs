using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDbManager.Application.Constants
{
    public static class Messages
    {
        public static string AddCustomerErrorMessage = "Müşteri sisteme eklenirken hata alınmıştır.";
        public static string GetAllCustomerErrorMessage = "Aradığınız kriterde müşteri bulunmamaktadır.";
        public static string CustomerAlreadyExistErrorMessage = "Müşteri sistemde kayıtlıdır.";
        public static string VerifyCustomerErrorMessage = "Bilgileriniz NVI servisinden doğrulanamamıştır.";
    }
}
