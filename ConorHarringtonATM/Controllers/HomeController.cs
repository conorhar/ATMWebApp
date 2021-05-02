using ConorHarringtonATM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ConorHarringtonATM.Data;
using ConorHarringtonATM.Services;
using ConorHarringtonATM.ViewModels;
using Newtonsoft.Json;

namespace ConorHarringtonATM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IBillService _billService;


        public HomeController(ApplicationDbContext dbContext, IBillService billService)
        {
            _dbContext = dbContext;
            _billService = billService;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel
            {
                RemainingBillItems = _dbContext.Bills.Select(r => new HomeIndexViewModel.BillItem
                {
                    Value = r.Value,
                    Quantity = r.Quantity
                }).ToList()
            };
            
            return View(viewModel);
        }

        public IActionResult Result(int input)
        {
            var viewModel = new HomeResultViewModel();
            var errorString = "ERROR - ENTER AMOUNT ACCORDING TO REMAINING BILLS";
            var dispensedBillsList = new List<int>();

            if (_billService.CheckInputDividesBy100(input) == false) viewModel.Header = errorString;

            if (_billService.CheckEnoughBillsAreAvailable(input) == false) viewModel.Header = errorString;

            if (_billService.CheckRemainingBillsAddUp(input) == false) viewModel.Header = errorString;

            else
            {
                dispensedBillsList = _billService.GetSmallestAmountBills(input);
                var listBillObjects = _billService.ConvertToBills(dispensedBillsList);
                _billService.UpdateBillsInDB(dispensedBillsList);

                viewModel.Header = "SUCCESS";
                viewModel.DispensedBillItems = listBillObjects.Select(r => new HomeResultViewModel.BillItem
                {
                    Value = r.Value,
                    Quantity = r.Quantity
                }).ToList();
            }

            viewModel.RemainingBillItems = _dbContext.Bills.Select(r => new HomeResultViewModel.BillItem
            {
                Value = r.Value,
                Quantity = r.Quantity
            }).ToList();

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
