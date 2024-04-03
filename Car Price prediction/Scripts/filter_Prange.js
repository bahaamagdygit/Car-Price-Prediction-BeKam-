
$(function () {
    $("#slider-range").slider({
        range: true,
        min: 0,
        max: 3500000.0,
        step: 2,
        values: [price0_js, price1_js],
        slide: function (event, ui) {
            $("#amount").val(" " + ui.values[0] + " - " + ui.values[1] + " EGP");

            $("#Price0").val(ui.values[0]);
            $("#Price1").val(ui.values[1]);
        }

    });

    $("#amount").val(" " + $("#slider-range").slider("values", 0) +
        " - " + $("#slider-range").slider("values", 1) + " EGP");

});

$(document).ready(function () {
    $("#restbtn").click(function () {

        $("#slider-range").slider({
            range: true,
            min: 0,
            max: 3500000.0,
            step: 2,
            values: [0, 3500000.0],
            slide: function (event, ui) {
                $("#amount").val(" " + ui.values[0] + " - " + ui.values[1] + " EGP");
                $("#Price0").val(ui.values[0]);
                $("#Price0").val(ui.values[1]);
            }
        });
        $("#amount").val(" " + $("#slider-range").slider("values", 0) +
            " - " + $("#slider-range").slider("values", 1) + " EGP");
        document.getElementById('Price0').value = 0;
        document.getElementById('Price1').value = 3500000.0;

    });
});

