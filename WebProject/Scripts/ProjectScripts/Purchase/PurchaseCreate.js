$("#AddButton").click(function () {

    if (CheckValidations()) {
        createRowForPurchase();
    }

});

function createRowForPurchase() {
    var selectedItem = getSelectedItem();

    var index = $("#purchaseDetailsTable").children("tr").length;
    
        var sl = index;

        var indexcell = "<td style='display:none'><input type='hidden' id='Index" + index + "' name='PurchaseDetails.Index' value='" + index + "' /></td>";
        var serialCell = "<td>" + (++sl) + "</td>";

        var itemProductIDCell = "<td style='display:none'><input type='hidden' id='itemProductID" + index + "' name='PurchaseDetails[" + index + "].ProductId' value='" + selectedItem.ProductID + "'/>" + selectedItem.ProductID + "</td>";
        var itemCategoryIDCell = "<td style='display:none'><input type='hidden' id='itemCategoryID" + index + "' name='PurchaseDetails[" + index + "].CategoryId' value='" + selectedItem.CategoryID + "'/>" + selectedItem.CategoryID + "</td>";
        var itemProductCodeCell = "<td><input type='hidden' id='itemProductCode" + index + "' name='PurchaseDetails[" + index + "].Code' value='" + selectedItem.ProductCode + "'/>" + selectedItem.ProductCode + "</td>";
        var itemMDateCell = "<td><input type='hidden' id='itemMDate" + index + "' name='PurchaseDetails[" + index + "].ManufacturedDate' value='" + selectedItem.Mdate + "'/>" + selectedItem.Mdate + "</td>";
        var itemEDateCell = "<td><input type='hidden' id='itemEDate" + index + "' name='PurchaseDetails[" + index + "].ExpireDate' value='" + selectedItem.EDate + "'/>" + selectedItem.EDate + "</td>";
        var itemQuantity = "<td><input type='hidden' id='itemQuantity" + index + "' name='PurchaseDetails[" + index + "].Quantity' value='" + selectedItem.Quantity + "'/>" + selectedItem.Quantity + "</td>";
        var itemUPrice = "<td><input type='hidden' id='itemUPrice" + index + "' name='PurchaseDetails[" + index + "].UnitPrice' value='" + selectedItem.UPrice + "'/>" + selectedItem.UPrice + "</td>";
        var itemTPrice = "<td><input type='hidden' id='itemTPrice" + index + "' name='PurchaseDetails[" + index + "].TotalPrice' value='" + selectedItem.TPrice + "'/>" + selectedItem.TPrice + "</td>";
        var itemMRP = "<td><input type='hidden' id='itemMRP" + index + "' name='PurchaseDetails[" + index + "].MRP' value='" + selectedItem.MRP + "'/>" + selectedItem.MRP + "</td>";
        var itemRemarks = "<td><input type='hidden' id='itemRemarks" + index + "' name='PurchaseDetails[" + index + "].Remarks' value='" + selectedItem.Remarks + "'/>" + selectedItem.Remarks + "</td>";
    //var itemDelete = "<td><input type=\"button\" value=\"Delete\" id=\"DeleteButton\" /></td>";
    //var itemDelete = "<td>@Html.ActionLink(\"Details\", \"Details\", \"Purchase\", new { Id = purchase.Id }, new { target=\"_blank\" })</td>";
    //onclick = \" EditorDelete();\"

    var createNewRow = "<tr> " + indexcell + serialCell + itemProductIDCell + itemCategoryIDCell + itemProductCodeCell + itemMDateCell + itemEDateCell + itemQuantity + itemUPrice + itemTPrice + itemMRP + itemRemarks +"</tr>";

        $("#purchaseDetailsTable").append(createNewRow);


    $("#itemCategoryID").val("");
    $("#itemProductID").val("");
    $("#itemProductCode").val("");
    $("#itemMDate").val("");
    $("#itemEDate").val("");
    $("#itemQuantity").val("");
    $("#itemAQuantity").val("");
    $("#itemUPrice").val("");
    $("#itemTPrice").val("");
    $("#itemPUPrice").val("");
    $("#itemMRP").val("");
    $("#itemPMRP").val("");
    $("#itemRemarks").val("");

    
}

function getSelectedItem() {

    var categoryID = $("#itemCategoryID").val();
    var productID = $("#itemProductID").val();
    var productCode = $("#itemProductCode").val();
    var Mdate = $("#itemMDate").val();
    var EDate = $("#itemEDate").val();
    var Quantity = $("#itemQuantity").val();
    var UPrice = $("#itemUPrice").val();
    var TPrice = $("#itemTPrice").val();
    var MRP = $("#itemMRP").val();
    var Remarks = $("#itemRemarks").val();

    var item;
    
    item = {
        "CategoryID": categoryID,
        "ProductID": productID,
        "ProductCode": productCode,
        "Mdate": Mdate,
        "EDate": EDate,
        "Quantity": Quantity,
        "UPrice": UPrice,
        "TPrice": TPrice,
        "MRP": MRP,
        "Remarks": Remarks
    }
    

    return item;
    }

function CheckValidations() {
    var check = true;

    var categoryId = document.getElementById("itemCategoryID").value;
    var productId = document.getElementById("itemProductID").value;
    var Mdate = document.getElementById("itemMDate").value;
    var Edate = document.getElementById("itemEDate").value;
    var Quantity = document.getElementById("itemQuantity").value;
    var UPrice = document.getElementById("itemUPrice").value;
    var TPrice = document.getElementById("itemTPrice").value;
    var MRP = document.getElementById("itemMRP").value;
    var Remarks = document.getElementById("itemRemarks").value;

    
    if (categoryId == "") {
        alert("Category Id Needed");
        check = false;
    }
    else if (productId == "--Select--" || productId == "") {
        alert("Product Id Needed");
        check = false;
    }
    else if (Mdate == "") {
        alert("Manufactured Date Needed");
        check = false;
    }
    
    else if (Edate == "") {
        alert("Expired Date Needed");
        check = false;
    }

    else if (Quantity == "0") {
        alert("Quantity Needed");
        check = false;
    }

    else if (UPrice == "0") {
        alert("Quantity Needed");
        check = false;
    }
            
    return true;
    return check;
}

$("#DeleteButton").click(function () {
    alert("hahhha");
});