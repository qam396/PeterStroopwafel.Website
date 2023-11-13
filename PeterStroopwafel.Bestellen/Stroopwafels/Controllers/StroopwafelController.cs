using Microsoft.AspNetCore.Mvc;
using Ordering;
using Ordering.Commands;
using Ordering.Queries;
using Ordering.Services;
using Stroopwafels.Models;
using System.Collections.Generic;
using System.Linq;

namespace Stroopwafels.Controllers
{
    public class StroopwafelController : Controller
    {
        private readonly QuotesQueryHandler _quotesQueryHandler;
        private readonly OrderCommandHandler _orderCommandHandler;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="httpClientWrapper"></param>
        public StroopwafelController(IHttpClientWrapper httpClientWrapper)
        {
            var suppliers = new IStroopwafelSupplierService[]
            {
                new StroopwafelSupplierAService(httpClientWrapper),
                new StroopwafelSupplierBService(httpClientWrapper),
                new StroopwafelSupplierCService(httpClientWrapper)
            };
            _quotesQueryHandler = new QuotesQueryHandler(suppliers);
            _orderCommandHandler = new OrderCommandHandler(suppliers);
        }

        /// <summary>
        /// Initiate model for main page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var viewModel = new OrderDetailsViewModel();

            return View(viewModel);
        }

        /// <summary>
        /// Post request to get quotes from different suppliers, return their prices (incl shipping cost1) and cheapest supplier.
        /// Quotes are different per supplier, as different types can be ordered from different supplier
        /// </summary>
        /// <param name="formModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetQuotes(OrderDetailsViewModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return Index();
            }

            var orderDetails = GetOrderDetails(formModel.OrderRows);

            var viewModel = new QuoteViewModel();
            var orderRows = new List<OrderRow>();

            foreach (var orderDetail in orderDetails)
            {
                // foreach type and amount get qoutes from supplier
                var quotes = GetQuotesFor(orderDetail);

                var orderRow = new OrderRow
                {
                    Type = orderDetail.Key,
                    Amount = orderDetail.Value,
                    Quotes = new List<Models.Quote>()
                };

                foreach (var quote in quotes)
                {
                    orderRow.Quotes.Add(new Models.Quote
                    {
                        SupplierName = quote.Supplier.Name,
                        TotalAmount = quote.TotalPricePresentation,
                        DeliveryDate = quote.DeliveryDate
                    });
                }

                // from all quotes, get cheapest supplier
                orderRow.SelectedSupplier = quotes.OrderBy(q => q.TotalPrice).First().Supplier.Name;
                orderRows.Add(orderRow);

                formModel.OrderRows = orderRows;

                viewModel.OrderRows = formModel.OrderRows;
            }

            return View(viewModel);
        }

        private IList<Ordering.Quote> GetQuotesFor(KeyValuePair<StroopwafelType, int> orderDetail)
        {
            var query = new QuotesQuery(orderDetail);
            var orders = _quotesQueryHandler.Handle(query);

            return orders;
        }

        private static IList<KeyValuePair<StroopwafelType, int>> GetOrderDetails(IEnumerable<OrderRow> orderRows)
        {
            return orderRows
                .Select(orderRow => new KeyValuePair<StroopwafelType, int>(orderRow.Type, orderRow.Amount))
                .ToList();
        }

        /// <summary>
        /// Place an order request
        /// Todo: Order should be per type, as it different type ordr is sent to different supplier.
        /// </summary>
        /// <param name="formModel"></param>
        /// <returns></returns>
        public ActionResult Order(QuoteViewModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return Index();
            }

            var orderDetails = GetOrderDetails(formModel.OrderRows);


            foreach (var orderDetail in orderDetails)
            {
                // TODO: instead of orderDetails, it should take single orderDetail.Quotes (not done yet)
                var command = new OrderCommand(orderDetails, formModel.OrderRows.Single(x => x.Type == orderDetail.Key).SelectedSupplier);
                _orderCommandHandler.Handle(command);
            }

            return View();

        }
    }
}
