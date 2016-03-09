using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CashMachine.BusinessLogic.Interfaces;
using CashMachine.DataAccess.Entities;
using CashMachine.Web.Helpers;
using CashMachine.Web.Models;

namespace CashMachine.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private readonly IAccountService _accountService;
        private readonly IOperationHistoryService _operationHistoryService;

        public AccountController(IAccountService accountService, IOperationHistoryService operationHistoryService)
        {
            _accountService = accountService;
            _operationHistoryService = operationHistoryService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CardNumberViewModel model)
        {
            if (ModelState.IsValid && model.CardNumber!=null)
            {
                var account = _accountService.GetAccountByCardNumber(model.CardNumber);
                if(model.CardNumber.Length < 16)
                    ModelState.AddModelError("","Введите все 16 цифр Вашей карточки");
                if (account == null)
                {
                    ModelState.AddModelError("", "Мы не смогли найти Вашу карточку");
                }
                else if (account.IsBlocked)
                {
                    //ModelState.AddModelError("","Ваша карточка заблокирована");
                    return View("Error", new ErrorViewModel
                    {
                        Message = "Ваша карточка заблокирована",
                        ReturnUrl = Request.Url.AbsolutePath
                    });
                }
                else
                {
                    GetCurrentAccountInSession().CardNumber = account.CardNumber;
                    return RedirectToAction("PinCode");
                }
            }
            return View(model);
        }

        //[HttpPost]
        public ActionResult PinCode()
        {
            if (GetCurrentAccountInSession().CardNumber == null)
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PinCode(PinCodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = _accountService.GetAccountByCardNumber(GetCurrentAccountInSession().CardNumber);
                if (account.PinCode == model.PinCode)
                    return RedirectToAction("Operation");

                if (GetCurrentAccountInSession().NumberOfAttempt <= 1)
                {
                    _accountService.BlockAccount(account);
                    Session.Abandon();
                    return View("Error", new ErrorViewModel
                    {
                        Message = "Ваша карточка была заблокирована",
                        ReturnUrl = Request.Url.AbsolutePath
                    });
                }
                else
                {
                    GetCurrentAccountInSession().NumberOfAttempt--;
                    ModelState.AddModelError("",String.Format("У Вас осталось {0} попытки чтобы ввести правильный PinCode",GetCurrentAccountInSession().NumberOfAttempt));
                }
            }
            return View(model);
        }

        private AccountSession GetCurrentAccountInSession()
        {
            var account = (AccountSession)Session["account"];
            if (account == null)
            {
                account = new AccountSession();
                Session["account"] = account;
            }
            return account;
        }

        public ActionResult Operation()
        {
            if(GetCurrentAccountInSession().CardNumber==null && GetCurrentAccountInSession().PinCode==0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View();
        }

        public ActionResult Balance()
        {
            if (GetCurrentAccountInSession().CardNumber == null && GetCurrentAccountInSession().PinCode == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var currentAccount = _accountService.GetAccountByCardNumber(GetCurrentAccountInSession().CardNumber);
            _operationHistoryService.CreateOperationHistory(new OperationHistory
            {
                CardNumber = currentAccount.CardNumber,
                DateTime = DateTime.Now,
                CodeOfOperation = "просмотр баланса",
                AccountId = currentAccount.Id
            });

            return View(new AccountViewModel
            {
                CardNumber = currentAccount.CardNumber,
                Money = currentAccount.Money
            });
        }

        public ActionResult WithdrawMoney()
        {
            if (GetCurrentAccountInSession().CardNumber == null && GetCurrentAccountInSession().PinCode == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WithdrawMoney(AccountViewModel model)
        {
            var currentAccount = _accountService.GetAccountByCardNumber(GetCurrentAccountInSession().CardNumber);
            if (model.Money <= currentAccount.Money)
            {
                currentAccount.Money -= model.Money;
                _accountService.EditAccount(currentAccount);
                _operationHistoryService.CreateOperationHistory(new OperationHistory
                {
                    CardNumber = currentAccount.CardNumber,
                    DateTime = DateTime.Now,
                    CodeOfOperation = "снятие денег",
                    AccountId = currentAccount.Id
                });
                return View("WithdrawMoneyReport", new WithdrawMoneyReportViewModel
                {
                    CardNumber = currentAccount.CardNumber,
                    WithdrawedSum = model.Money,
                    Balance = currentAccount.Money,
                    Date = DateTime.Now
                });
            }
            else
            {
                //ModelState.AddModelError("","На Вашем счету недостаточно средств");
                return View("Error", new ErrorViewModel
                {
                    Message = "На Вашем счету недостаточно средств",
                    ReturnUrl = Request.Url.AbsolutePath
                });
            }
            //return View(model);
        }

        public ActionResult WithdrawMoneyReport()
        {
            return View();
        }

        public ActionResult ExitAccount()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}