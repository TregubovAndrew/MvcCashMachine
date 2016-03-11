$(document).ready(function () {

        $(function inputForKeys($input, $maxLength) {
            console.log('asdsad');
            if ($input.val().length < $maxLength) {
                $input.val($input.val() + $(this).attr('value'));
            }
        });
            $(function reset($input) {
                console.log('sssss');
                $input.val("");
            });

})
