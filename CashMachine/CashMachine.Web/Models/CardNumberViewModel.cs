using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CashMachine.Web.Models
{
    public class CardNumberViewModel
    {
        [Required(ErrorMessage = "Введите номер Вашей карточки, поле не должно быть пустым")]
        [StringLength(16, ErrorMessage = "Введите все 16 цифр Вашей карточки", MinimumLength = 16)]
        [RegularExpression(@"^\d+$", ErrorMessage = "...")]
        public string CardNumber { get; set; }
    }
}