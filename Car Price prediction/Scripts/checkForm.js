
function validate_div1() {
    var Brand = document.getElementById("Brand").value;
    const Model = document.getElementById("Model").value;
    const Year = document.getElementById("Year").value;
    const Engine_Cylinders = document.getElementById("Engine_Cylinders").value;
    const kilo_meter_Dr = document.getElementById("kilo_meter_Dr").value;
    const Price = document.getElementById("Price").value;
    var Governorate = document.getElementById("Governorate").value;
    const Area = document.getElementById("Area").value;
    var valid = true;
    if (Brand == "") {
        valid = false;
        console.log("brand not work");
        document.getElementById("Brand").style.border = "2px solid red";
        document.getElementById("Brand1").innerHTML = "<i class='fas fa-exclamation-triangle p-2 text-danger bg-light rounded-pill'> Please select <b>Brand<b> <i class='fas fa-exclamation-triangle '> </i> </i>";
        document.getElementById("Brand1").style.color = "red";
    }
    else {
        document.getElementById("Brand").style.border = "2px solid green";
        document.getElementById("Brand1").innerHTML = "<i class='fa fa-check bg-light rounded-pill p-2 text-success '>This value is correct</i>";

    }
    if (Model == "") {
        console.log("model not work");
        valid = false;
        document.getElementById("Model").style.border = "2px solid red";
        document.getElementById("Model1").innerHTML = "<i class='fas fa-exclamation-triangle  p-2 text-danger bg-light rounded-pill'> Please select Model <i class='fas fa-exclamation-triangle '></i></i> ";
        document.getElementById("Model1").style.color = "red";
    }
    else {
        document.getElementById("Model").style.border = "2px solid green";
        document.getElementById("Model1").innerHTML = "<i class='fa fa-check bg-light rounded-pill p-2 text-success '>This value is correct</i>";

    }
    if (Year == "") {
        console.log("year not work");
        valid = false;
        document.getElementById("Year").style.border = "2px solid red";
        document.getElementById("Year1").innerHTML = "<i class='fas fa-exclamation-triangle  p-2 text-danger bg-light rounded-pill'>Please select Year <i class='fas fa-exclamation-triangle  '> </i> </i> ";
        document.getElementById("Year1").style.color = "red";
    }
    else {
        document.getElementById("Year").style.border = "2px solid green";
        document.getElementById("Year1").innerHTML = "<i class='fa fa-check bg-light rounded-pill p-2 text-success '>This value is correct</i>";

    }

    var radio1 = document.querySelector('input[name="Transmission"]:checked');
    if (radio1 == null) {
        console.log("Transmission not work");
        valid = false;
        document.getElementById("Transmission0").style.border = "2px solid red";
        document.getElementById("Transmission00").style.border = "2px solid red";
        document.getElementById("Transmission1").innerHTML = "<i class='fas fa-exclamation-triangle p-2 text-danger bg-light rounded-pill'>Please fill out Transmission field <i class='fas fa-exclamation-triangle  '> </i>  </i>";
        document.getElementById("Transmission1").style.color = "red";
    }
    else {

        document.getElementById("Transmission0").style.border = "2px solid green";
        document.getElementById("Transmission00").style.border = "2px solid green";
        document.getElementById("Transmission1").innerHTML = "<i class='fa fa-check bg-light rounded-pill p-2 text-success '>This value is correct</i>";
    }
    var radio2 = document.querySelector('input[name="Body"]:checked');
    if (radio2 == null) {
        console.log("Body not work");
        valid = false;
        document.getElementById("Body1").style.border = "2px solid red";
        document.getElementById("Body2").style.border = "2px solid red";
        document.getElementById("Body3").style.border = "2px solid red";
        document.getElementById("Body4").style.border = "2px solid red";
        document.getElementById("Body5").style.border = "2px solid red";
        document.getElementById("Body6").style.border = "2px solid red";
        document.getElementById("Body7").style.border = "2px solid red";
        document.getElementById("Body8").style.border = "2px solid red";
        document.getElementById("Body11").innerHTML = "<i class='fas fa-exclamation-triangle p-2 text-danger bg-light rounded-pill'>Please fill out Body field <i class='fas fa-exclamation-triangle'> </i> </i> ";
        document.getElementById("Body11").style.color = "red";
    }
    else {
        document.getElementById("Body1").style.border = "2px solid green";
        document.getElementById("Body2").style.border = "2px solid green";
        document.getElementById("Body3").style.border = "2px solid green";
        document.getElementById("Body4").style.border = "2px solid green";
        document.getElementById("Body5").style.border = "2px solid green";
        document.getElementById("Body6").style.border = "2px solid green";
        document.getElementById("Body7").style.border = "2px solid green";
        document.getElementById("Body8").style.border = "2px solid green";
        document.getElementById("Body11").innerHTML = "<i class='fa fa-check bg-light rounded-pill p-2 text-success '>This value is correct</i>";
    }

    if (Engine_Cylinders == "") {
        console.log("Engine_Cylinders not work");
        valid = false;
        document.getElementById("Engine_Cylinders").style.border = "2px solid red";
        document.getElementById("Engine_Cylinders1").innerHTML = " <i class='fas fa-exclamation-triangle p-2 text-danger bg-light rounded-pill'>Please Choose a value <i class='fas fa-exclamation-triangle'> </i> </i>";
        document.getElementById("Engine_Cylinders1").style.color = "red";
    }
    else {
        document.getElementById("Engine_Cylinders").style.border = "2px solid green";
        document.getElementById("Engine_Cylinders1").innerHTML = "<i class='fa fa-check bg-light rounded-pill p-2 text-success '>This value is correct</i>";

    }
    if (kilo_meter_Dr == "" || kilo_meter_Dr <= 0) {
        console.log("kilo_meter_Dr not work");
        valid = false;
        document.getElementById("kilo_meter_Dr").style.border = "2px solid red";
        document.getElementById("kilo_meter_Dr1").innerHTML = "<i class='fas fa-exclamation-triangle p-2 text-danger bg-light rounded-pill'> Please Choose a value<i class='fas fa-exclamation-triangle'></i>  </i>";
        document.getElementById("kilo_meter_Dr1").style.color = "red";
    }
    else {
        document.getElementById("kilo_meter_Dr").style.border = "2px solid green";
        document.getElementById("kilo_meter_Dr1").innerHTML = "<i class='fa fa-check bg-light rounded-pill p-2 text-success '>This value is correct</i>";

    }
    if (Price == "" || Price <= 0) {
        console.log("Price not work");
        valid = false;
        document.getElementById("Price").style.border = "2px solid red";
        document.getElementById("Price1").innerHTML = "<i class='fas fa-exclamation-triangle p-2 text-danger bg-light rounded-pill'> Please fill Real price field <i class='fas fa-exclamation-triangle'></i>  </i>";
        document.getElementById("Price1").style.color = "red";

    }
    else {
        document.getElementById("Price").style.border = "2px solid green";
        document.getElementById("Price1").innerHTML = "<i class='fa fa-check bg-light rounded-pill p-2 text-success '>This value is correct</i>";

}

    if (Governorate == "") {
        console.log("Governorate not work");
        valid = false;
        document.getElementById("Governorate").style.border = "2px solid red";
        document.getElementById("Governorate1").innerHTML = "<i class='fas fa-exclamation-triangle p-2 text-danger bg-light rounded-pill'> Please select Governorate <i class='fas fa-exclamation-triangle'></i>  </i>";
        document.getElementById("Governorate1").style.color = "red";
    }
    else {
        document.getElementById("Governorate").style.border = "2px solid green";
        document.getElementById("Governorate1").innerHTML = "<i class='fa fa-check bg-light rounded-pill p-2 text-success '>This value is correct</i>";

    }
    if (Area == "") {
        console.log("Area not work");
        valid = false;
        document.getElementById("Area").style.border = "2px solid red";
        document.getElementById("Area1").innerHTML = "<i class='fas fa-exclamation-triangle p-2 text-danger bg-light rounded-pill'> Please select Area <i class='fas fa-exclamation-triangle'> </i> </i>";
        document.getElementById("Area1").style.color = "red";

    }
    else {
        document.getElementById("Area").style.border = "2px solid green";
        document.getElementById("Area1").innerHTML = "<i class='fa fa-check bg-light rounded-pill p-2 text-success '>This value is correct</i>";

    }
   
    if (valid == true) {
        console.log("Inputs OK");
        hideDive();
    }
}


function validate_div2() {
   
    const Image1 = document.getElementById("Image1").value;
    const Image2 = document.getElementById("Image2").value;
    const Image3 = document.getElementById("Image3").value;
    var valid = true;
    if (Image1 == "") {
        valid = false;
        console.log("Please fill out Image1 field");
        document.getElementById("Image10").style.border = "2px solid red";
        document.getElementById("Image11").innerHTML = "Please fill Front Image *";
        document.getElementById("Image11").style.color = "red";
    }
    else {
        document.getElementById("Image1").style.border = "2px solid green";
        document.getElementById("Image11").innerHTML = "";
    }
    if (valid == true) {
        console.log("Inputs OK");
        hideDive1();
    }
}
function hideDive() {
    $("#two").prop("checked", true);
    var sec = document.getElementById("div1").style.display;
    if (sec == "block") {
        document.getElementById("div1").style.display = "none";
        document.getElementById("div2").style.display = "block";

    }
    else {
      document.getElementById("div1").style.display = "block";
    }
    
}

function hideDive1() {
    $("#three").prop("checked", true);
    var sec = document.getElementById("div2").style.display;
    if (sec == "block") {
        document.getElementById("div2").style.display = "none";
        document.getElementById("div3").style.display = "block";
    } else {
        document.getElementById("div2").style.display = "block";
}

}


function back() {
    //$("#one").prop("checked", false);
    var sec = document.getElementById("div2").style.display;
    if (sec == "block") {
        document.getElementById("div2").style.display = "none";
        document.getElementById("div1").style.display = "block";
    } else {
        document.getElementById("div2").style.display = "block";
    }
}
function back1() {
    //$("#two").prop("checked", false);
    var sec1 = document.getElementById("div3").style.display;
    if (sec1 == "block") {
        document.getElementById("div3").style.display = "none";
        document.getElementById("div2").style.display = "block";
    }
    else {
        document.getElementById("div3").style.display = "block";
    }
}