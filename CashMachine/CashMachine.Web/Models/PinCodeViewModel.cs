﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CashMachine.Web.Models
{
    public class PinCodeViewModel
    {
        [Required(ErrorMessage = "Введите Pin Code Вашей карточки, поле не должно быть пустым")]
        [StringLength(4, ErrorMessage = "Введите все 4 цифры Вашего Pin Code", MinimumLength = 4)]
        [RegularExpression(@"^\d+$", ErrorMessage = "...")]
        public string PinCode { get; set; }
    }
}