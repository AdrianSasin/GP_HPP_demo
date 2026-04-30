using GlobalPayments.Api;
using GlobalPayments.Api.Entities;
using GlobalPayments.Api.Entities.Enums;
using GlobalPayments.Api.Services;
using GlobalPayments.Api.Utils;
using System;

namespace PaymentLinkDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServicesContainer.RemoveConfig();

                var config = new GpApiConfig
                {
                    AppId = System.Environment.GetEnvironmentVariable("GP_APP_ID"),
                    AppKey = System.Environment.GetEnvironmentVariable("GP_APP_KEY"),
                    Channel = Channel.CardNotPresent,
                    Country = "PL",
                    ServiceUrl = "https://apis.sandbox.eservicegateway.com/ucp"
                };

                ServicesContainer.ConfigureService(config);

                var customer = new Customer
                {
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Email = "jan.kowalski@example.com",
                    Language = "PL",
                    Status = "NEW"
                };

                var billingAddress = new Address
                {
                    StreetAddress1 = "Testowa 1",
                    City = "Warszawa",
                    PostalCode = "00-001",
                    Country = "PL"
                };

                var payByLink = new PayByLinkData
                {
                    Type = PayByLinkType.HOSTED_PAYMENT_PAGE,
                    UsageMode = PaymentMethodUsageMode.Single,
                    
                    AllowedPaymentMethods = new PaymentMethodName[]
                    {
                        PaymentMethodName.Card,
                        //PaymentMethodName.BLIK,
                        //PaymentMethodName.BankPayment

                    },
                    
                    UsageLimit = 1,
                    Name = "Demo payment link",
                    ExpirationDate = DateTime.UtcNow.AddDays(7),
                    ReturnUrl = "https://example.com/return",
                    CancelUrl = "https://example.com/cancel",
                    StatusUpdateUrl = "https://example.com/status",
                    Configuration = new PaymentMethodConfiguration
                    {
                        IsBillingAddressRequired = true,
                        StorageMode = StorageMode.OFF
                    }
                };

                var response = PayByLinkService.Create(payByLink, 10m)
                    .WithCurrency("PLN")
                    .WithClientTransactionId(Guid.NewGuid().ToString("N"))
                    .WithAddress(billingAddress, AddressType.Billing)
                    .WithCustomerData(customer)
                    .WithDescription("Console demo payment link")
                    .Execute();

                Console.WriteLine("ResponseCode: " + response.ResponseCode);
                Console.WriteLine("ResponseMessage: " + response.ResponseMessage);

                if (response.PayByLinkResponse != null)
                {
                    Console.WriteLine("Payment URL:");
                    Console.WriteLine(response.PayByLinkResponse.Url);
                }
                else
                {
                    Console.WriteLine("Brak PayByLinkResponse w odpowiedzi.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("BLAD:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine();
            Console.WriteLine("Nacisnij dowolny klawisz...");
            Console.ReadKey();
        }
    }
}