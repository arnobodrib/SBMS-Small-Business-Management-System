$(document).ready(function () {
    $("#itemCategoryID").change(function () {

        var id = $("#itemCategoryID").val();

        var jsonRequesData = { id: id };

        $.ajax({
            url: "/Purchase/GetProductById",
            type: "POST",
            data: jsonRequesData,
            success: function (data) {

                $("#itemProductID").empty();
                $("#itemProductID").append('<option>--Select--</option>');
                //alert("Ajax Requiest Success");

                $.each(data, function (key, value) {
                    //alert("Id:" + value.Id + " Name: " + value.Name);
                    $("#itemProductID").append('<option value="' + value.Id + '">' + value.Name + '</option>');
                });
            },
            error: function () {
                alert("Ajax Requiest Error");
            }
        });


    });


});

$(document).ready(function () {
    $("#itemProductID").change(function () {
        var productId = $("#itemProductID").val();
        var categoryId = $("#itemCategoryID").val();

        var jsonRequesData = { productId: productId, categoryId: categoryId};

        $.ajax({
            url: "/Purchase/GetProductDetailsById",
            type: "POST",
            data: jsonRequesData,
            success: function (data) {

                //alert("Ajax Request Success");
                $("#itemAQuantity").val(data.aq);
                $("#itemProductCode").val(data.pc);
                $("#itemPUPrice").val(Math.round(data.pup));
                $("#itemPMRP").val(Math.round(data.pmrp));

            },
            error: function () {
                alert("Ajax Requiest Error");
            }
        });
    });
});

$(document).ready(function () {
    $("#itemQuantity").change(function () {

        var q = $("#itemQuantity").val();
        var u = $("#itemUPrice").val();

        $("#itemTPrice").val(q * u);
        $("#itemMRP").val((Number(u) + Number((u) * .25)));


    });
});

$(document).ready(function () {
    $("#itemUPrice").change(function () {

        var q = $("#itemQuantity").val();
        var u = $("#itemUPrice").val();

        $("#itemTPrice").val(q * u);
        $("#itemMRP").val((Number(u) + Number((u) * .25)));


    });
});

function submitValidate() {

    $("#AddButton").click(function () {

        var index = $("#purchaseDetailsTable").children("tr").length;

        if (index == 0) {
            alert("Please At least One Purchase");
            return false;
        }
        else {
            return true;
        }
    });
}


function validate(evt) {
    var theEvent = evt || window.event;

    // Handle paste
    if (theEvent.type === 'paste') {
        key = event.clipboardData.getData('text/plain');
    } else {
        // Handle key press
        var key = theEvent.keyCode || theEvent.which;
        key = String.fromCharCode(key);
    }
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}

