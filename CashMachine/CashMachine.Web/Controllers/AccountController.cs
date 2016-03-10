﻿using System;
using System.Net;
using System.Web.Mvc;
using CashMachine.BusinessLogic.Interfaces;
using CashMachine.BusinessLogic.Services;
using CashMachine.Web.Helpers;
using CashMachine.Web.Models;

namespace CashMachine.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private readonly IAccountService _accountService;
        private readonly IOperationService _operationService;

        public AccountController(IAccountService accountService, IOperationService operationService)
        {
            _accountService = accountService;
            _operationService = operationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CardNumberViewModel model)
        {
            if (ModelState.IsValid && model.CardNumber != null)
            {
                var account = _accountService.GetAccountByCardNumber(model.CardNumber);
                if (model.CardNumber.Length < 16)
                    ModelState.AddModelError("", "Введите все 16 цифр Вашей карточки");
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

                var check = _accountService.Auth(GetCurrentAccountInSession().CardNumber, model.PinCode);

                if (check == AccountService.AuthResult.Success)
                    return RedirectToAction("Operation");

                if (check == AccountService.AuthResult.Blocked)
                {
                    Session.Abandon();
                    return View("Error", new ErrorViewModel
                    {
                        Message = "Ваша карточка была заблокирована",
                        ReturnUrl = Request.Url.AbsolutePath
                    });
                }
                if (check == AccountService.AuthResult.Fail)
                {
                    var account = _accountService.GetAccountByCardNumber(GetCurrentAccountInSession().CardNumber);
                    ModelState.AddModelError("",
                        String.Format("У Вас осталось {0} попытки чтобы ввести правильный PinCode",
                            account.AttemptsCount));
                }
            }
            return View();
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
            if (GetCurrentAccountInSession().CardNumber == null && GetCurrentAccountInSession().PinCode == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View();
        }

        public ActionResult Balance()
        {
            if (GetCurrentAccountInSession().CardNumber == null && GetCurrentAccountInSession().PinCode == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var currentAccount = _accountService.GetAccountByCardNumber(GetCurrentAccountInSession().CardNumber);
            if (_accountService.BalanceOperationSuccess(currentAccount))
            {
                return View(new AccountViewModel
                {
                    CardNumber = currentAccount.CardNumber,
                    Money = currentAccount.AvailableBalance
                });
            }
            return View();
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
            if (_accountService.WithdrawMoneySuccess(currentAccount, model.Money))
            {
                return View("WithdrawMoneyReport", new WithdrawMoneyReportViewModel
                {
                    CardNumber = currentAccount.CardNumber,
                    WithdrawedSum = model.Money,
                    Balance = currentAccount.AvailableBalance,
                    Date = DateTime.Now
                });
            }

            return View("Error", new ErrorViewModel
            {
                Message = "На Вашем счету недостаточно средств",
                ReturnUrl = Request.Url.AbsolutePath
            });


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