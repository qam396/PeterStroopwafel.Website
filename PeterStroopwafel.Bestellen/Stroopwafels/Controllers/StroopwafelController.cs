﻿using Microsoft.AspNetCore.Mvc;
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
            var quotes = GetQuotesFor(orderDetails);

            var viewModel = new QuoteViewModel();
            foreach (var quote in quotes)
            {
                viewModel.Quotes.Add(new Models.Quote
                {
                    SupplierName = quote.Supplier.Name,
                    TotalAmount = quote.TotalPricePresentation,
                    DeliveryDate = quote.DeliveryDate
                });
            }

            viewModel.OrderRows = formModel.OrderRows;
            // Select cheapest supplier
            viewModel.SelectedSupplier = quotes.OrderBy(q => q.TotalPrice).First().Supplier.Name;

            return View(viewModel);
        }

        private IList<Ordering.Quote> GetQuotesFor(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            var query = new QuotesQuery(orderDetails);
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

            var command = new OrderCommand(orderDetails, formModel.SelectedSupplier);
            _orderCommandHandler.Handle(command);

            return View();

        }
    }
}
