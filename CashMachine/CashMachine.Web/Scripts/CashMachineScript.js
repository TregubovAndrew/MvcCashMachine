var $maxLengthForCardNumber = 16;
var $maxLengthForPinCode = 4;
var $maxLengthForWithdrawMoney = 10;

function inputForKeys($input,$value, $maxLength) {
    if ($input.val().length < $maxLength) {
        $input.val($input.val() + $value);
    }
};

function reset($input) {
    $input.val("");
};
