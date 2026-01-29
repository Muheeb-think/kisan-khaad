

//validation pincode , mobile number and password
$(document).on('input', '.blur-validate', function () {
    this.value = this.value.replace(/\D/g, '');
});


$(document).on('blur', '.blur-validate', function () {
    validatePhoneorPinCode(this); // pass 'this'
});

function validatePhoneorPinCode(element) {
    var $input = $(element); // now $input is the input element
    var value = $input.val().trim();
    var type = $input.data('type');

    // Remove old error
    $input.removeClass('is-invalid');
    $input.next('.invalid-feedback').remove();

    if (!value) return;

    if (type === "pincode") {
        var pinRegex = /^[1-9][0-9]{5}$/;
        if (!pinRegex.test(value)) {
            showError($input, "कृपया वैध 6 अंकों का पिनकोड दर्ज करें");
        }
    }

    if (type === "mobile") {
        var mobileRegex = /^[6-9][0-9]{9}$/;
        if (!mobileRegex.test(value)) {
            showError($input, "कृपया वैध 10 अंकों का मोबाइल नंबर दर्ज करें");
        }
    }
}

function showError(input, message) {
    input.addClass('is-invalid');
    input.closest('.col-md-4').find('.invalid-feedback').text(message).show();
    checkButtonState();
}

function checkButtonState() {
    if ($('.is-invalid').length > 0) {
        $('.nextBtn').prop('disabled', true);
    } else {
        $('.nextBtn').prop('disabled', false);
    }
}

function clearError(input) {
    input.removeClass('is-invalid');
    input.closest('.col-md-4').find('.invalid-feedback').hide();
    checkButtonState();
}

function validatePasswordFields() {
    let isValid = true;

    let $password = $("#Password");
    let $confirmPassword = $("#ConfirmPassword");

    clearError($password);
    clearError($confirmPassword);

    let passwordVal = $password.val().trim();
    let confirmVal = $confirmPassword.val().trim();

    let strongRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?])[A-Za-z\d@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]{8,}$/;

    // Password validation
    if (!passwordVal || !strongRegex.test(passwordVal)) {
        showError($password,
            "पासवर्ड कम से कम 8 अक्षर, एक Capital, एक Small, एक Number और एक Special Character होना चाहिए");
        isValid = false;
    }

    // Confirm password validation
    if (!confirmVal || passwordVal !== confirmVal) {
        showError($confirmPassword, "पासवर्ड और कन्फर्म पासवर्ड समान नहीं हैं");
        isValid = false;
    }

    return isValid;
}

$(document).on('blur', '.blur-validate-password', function () {
    validatePasswordFields();
});

$(document).on('click', '.toggle-password', function () {

    let target = $($(this).data('target'));
    let icon = $(this).find('i');

    if (target.attr('type') === 'password') {
        target.attr('type', 'text');
        icon.removeClass('fa-eye').addClass('fa-eye-slash');
    } else {
        target.attr('type', 'password');
        icon.removeClass('fa-eye-slash').addClass('fa-eye');
    }
});

// end validation 

function formatToDDMMYYYY(dateString) {
    debugger;
    const parts = dateString.split(' ')[0].split('-');
    const day = parts[0];
    const month = parts[1];
    const year = parts[2];

    return `${day}-${month}-${year}`;
}

function SearchKhasraNo() {
    var FarmerId = $('#LandRecordNumber').val();
    $.ajax({
        type: 'POST',
        url: '/Farmer/SearchKhasraNo',
        contentType: 'application/json',
        data: JSON.stringify({ FarmerId }),
        success: function (response) {
            console.log(response.data);
            if (response.status && response.data) {

                $('#LandTotalArea').val(response.data.totalArea ?? '');
                $('#FarmerShare').val(response.data.farmerShareArea ?? '');      
                $('#FarmerNameLandKhasra').val(response.data.farmerNameLand ?? '');      
            }
        }
    });
}
function SearchFarmerById() {
    var FarmerId = $('#FarmerId').val();
    $.ajax({
        type: 'POST',
        url: '/Farmer/SearchFarmerByIdForAgriStack',
        contentType: 'application/json',
        data: JSON.stringify({ FarmerId }),
        success: function (response) {
            console.log(response.data);
            if (response.status && response.data == "-10") {
                alert("आपका आवेदन पहले ही जमा किया जा चुका है।");
                window.location.href = '/Auth/Login';
                return;
            }
            else if (response.status && response.data) {

                $('#FarmerName').val(response.data.farmerName ?? '');
                $('#FarmerNameForLand').val(response.data.farmerName ?? '');
                $('#Village').val(response.data.villageNameHi ?? '');
                // $('#DOB').val(response.data.dob ?? '');
                const dobValue = response.data.dob ?? '';

                if (dobValue) {
                    debugger;
                    const formattedDate = formatToDDMMYYYY(dobValue);
                    $('#DOB').val(formattedDate);
                } else {
                    $('#DOB').val('');
                }
                $('#AadharCardNo').val(response.data.aadharNo ?? '');
                $('#Gender').val(response.data.gender ?? ''); // only if exists
                $('#MobileNumberAgriStack').val(response.data.mobileNo ?? '');
                $('#MobileNumberAgriStackForLand').val(response.data.mobileNo ?? '');
                $('#Gender').val(response.data.gender ?? '');
                $('#FatherHusbandName').val(response.data.fatherOrHusbandName ?? '');
                $('#Area').val(response.data.cropArea ?? ''); // only if exists
                $('#pincodeAgriStack').val(response.data.pincode ?? '');
                $('#tahsilAgriStack').val(response.data.tehsilId);
                $('#tahsilAgriStack').prop('disabled', true);
                $('#agriStackId').val($('#FarmerId').val());
                // $('#AreaType').val(response.data.blockId);
                // $('#village').val(response.data.villageId);

            }
        }
    });
}


//start grid bind code 
let landIndex = 0;

function updateTotals() {
    let totalAreaSum = 0;
    let totalShareSum = 0;

    $("#LandGrid tbody tr").each(function () {
        let area = parseFloat($(this).find("td:eq(3)").text()) || 0; // TotalArea column
        let share = parseFloat($(this).find("td:eq(4)").text()) || 0; // Share column
        totalAreaSum += area;
        totalShareSum += share;
    });

    $("#TotalAreaDisplay").text(totalAreaSum.toFixed(2));
    $("#TotalShareDisplay").text(totalShareSum.toFixed(2));
}

function updateTotals() {
    let totalKhasra = 0;
    let totalArea = 0;
    let totalShare = 0;

    $("#LandGrid tbody tr").each(function () {
        let khasra = parseFloat($(this).find("td:eq(2)").text()) || 0;
        let area = parseFloat($(this).find("td:eq(3)").text()) || 0;
        let share = parseFloat($(this).find("td:eq(4)").text()) || 0;

        totalKhasra += khasra;
        totalArea += area;
        totalShare += share;
    });

    $("#TotalKhasra").text(totalKhasra);
    $("#TotalArea").text(totalArea);
    $("#TotalShare").text(totalShare);
}

$("#AddLandBtn").on("click", function () {
    let tahsilText = $("#LandTahsilDropdown option:selected").text();
    let tahsilVal = $("#LandTahsilDropdown").val();
    let villageText = $("#LandVillageTypeDropdown option:selected").text();
    let villageVal = $("#LandVillageTypeDropdown").val();
    let khasra = $("#LandRecordNumber").val();
    let totalArea = $("#LandTotalArea").val();
    let share = $("#FarmerShare").val();

    if (!tahsilVal || !villageVal || !khasra) {
        alert("कृपया आवश्यक भूमि विवरण भरें");
        return;
    }

    let row = `<tr data-index="${landIndex}">
        <td>${tahsilText}</td>
        <td>${villageText}</td>
        <td>${khasra}</td>
        <td>${totalArea}</td>
        <td>${share}</td>
        <td>
            <button type="button" class="btn btn-danger btn-sm removeLand">❌ Remove</button>
        </td>
    </tr>`;
    $("#LandGrid tbody").append(row);

    let hiddenInputs = `<div id="land_${landIndex}">
        <input type="hidden" name="LandList[${landIndex}].TahsilId" value="${tahsilVal}" />
        <input type="hidden" name="LandList[${landIndex}].VillageId" value="${villageVal}" />
        <input type="hidden" name="LandList[${landIndex}].LandRecordNumber" value="${khasra}" />
        <input type="hidden" name="LandList[${landIndex}].LandTotalArea" value="${totalArea}" />
        <input type="hidden" name="LandList[${landIndex}].FarmerShare" value="${share}" />
    </div>`;
    $("#LandHiddenFields").append(hiddenInputs);

    landIndex++;
    $("#LandRecordNumber, #LandTotalArea, #FarmerShare").val("");

    // Update totals
    updateTotals();
});

// Remove row
$(document).on("click", ".removeLand", function () {
    let row = $(this).closest("tr");
    let index = row.data("index");
    $("#land_" + index).remove();
    row.remove();
    updateTotals();
});


$(document).on("click", ".removeLand", function () {
    let row = $(this).closest("tr");
    let index = row.data("index");
    row.remove();
    $(`#land_${index}`).remove();
});

// end grid bind code 

// REMOVE ROW
$(document).on("click", ".removeLand", function () {

    let row = $(this).closest("tr");
    let index = row.data("index");

    $("#land_" + index).remove();
    row.remove();
});

//WIZARD step one by one
$(document).ready(function () {

    let currentStep = 1;
    const totalSteps = $(".wizard-step").length;

    showStep(currentStep);

    // NEXT BUTTON
    $(".nextBtn").on("click", function () {
       
        if (!validateStep(currentStep)) {
            alert("कृपया सभी आवश्यक जानकारी भरें");
            return;
        }

        if (currentStep < totalSteps) {
            currentStep++;
            showStep(currentStep);
        }
    });

    // PREVIOUS BUTTON
    $(".prevBtn").on("click", function () {

        if (currentStep > 1) {
            currentStep--;
            showStep(currentStep);
        }
    });

    function showStep(step) {

        // Show Wizard Step
        $(".wizard-step").removeClass("active");
        $(".wizard-step[data-step='" + step + "']").addClass("active");

        // Reset Stepper
        $(".step").removeClass("active completed");

        $(".step").each(function () {

            let s = $(this).data("step");
            let circle = $(this).find(".circle");

            if (s < step) {
                $(this).addClass("completed");
                circle.html("✓"); // completed tick
            }
            else {
                circle.text(s); // reset number
            }

            if (s === step) {
                $(this).addClass("active");
            }
        });
    }

    // VALIDATION
    function validateStep(step) {

        let valid = true;

        $(".wizard-step[data-step='" + step + "']")
            .find("input, select, textarea")
            .each(function () {

                if ($(this).prop("required") && $.trim($(this).val()) === "") {
                    $(this).addClass("is-invalid");
                    valid = false;
                } else {
                    $(this).removeClass("is-invalid");
                }
            });

        return valid;
    }
});


$(document).ready(function () {
    $("#tahsilId").change(function () {
        
        var tahsilId = $(this).val();
        var villageDropdown = $("#VillageTypeDropdown");

        console.log("Selected Tehsil ID: " + tahsilId);

        // Clear previous items
        villageDropdown.empty();
        villageDropdown.append('<option value="">-- चयन करें --</option>');

        if (tahsilId) {
            $.getJSON('/Farmer/GetVillageByTehsil', { tahsilId: tahsilId }, function (data) {
                console.log(data);

                $.each(data, function (index, item) {
                    villageDropdown.append($('<option/>', {
                        value: item.villageId,
                        text: item.villageName
                    }));
                });
            });
        }
    });

    $("#AreaTypeDropdown").change(function () {
        var areaTypeId = $(this).val();
        var villageDropdown = $("#VillageTypeDropdown");

        console.log("Selected AreaType ID: " + areaTypeId);

        // Clear previous items
        villageDropdown.empty();
        villageDropdown.append('<option value="">-- चयन करें --</option>');

        if (areaTypeId) {
            $.getJSON('/Farmer/GetVillageByBlock', { BlockId: areaTypeId }, function (data) {
                console.log(data);
                $.each(data, function (index, item) {
                    villageDropdown.append($('<option/>', {
                        value: item.id,
                        text: item.villageNameHi
                    }));
                });
            });
        }
    });

    //Land Details  tehsil then select blcok in step 3
    // $("#LandTahsilDropdown").change(function () {
    //        var tahsilId = $(this).val();
    //        var areaDropdown = $("#LandAreaTypeDropdown");
    //        console.log("Selected Tehsil ID: " + tahsilId);
    //        // Clear previous items
    //        areaDropdown.empty();
    //        areaDropdown.append('<option value="">-- चयन करें --</option>');

    //        if (tahsilId) {
    //            $.getJSON('/Farmer/GetAreaTypesByTehsil', { tahsilId: tahsilId }, function (data) {
    //                console.log(data);
    //                    $.each(data, function (index, item) {
    //                    areaDropdown.append($('<option/>', {
    //                    value: item.blockId,
    //                    text: item.blockName
    //                  }));
    //                });
    //            });
    //        }
    //    });

    $("#LandAreaTypeDropdown").change(function () {
        var areaTypeId = $(this).val();
        var villageDropdown = $("#LandVillageTypeDropdown");

        console.log("Selected AreaType ID: " + areaTypeId);

        // Clear previous items
        villageDropdown.empty();
        villageDropdown.append('<option value="">-- चयन करें --</option>');

        if (areaTypeId) {   // check correct variable
            $.getJSON('/Farmer/GetVillageByBlock', { BlockId: areaTypeId }, function (data) {
                console.log(data);
                $.each(data, function (index, item) {
                    villageDropdown.append($('<option/>', {
                        value: item.id,
                        text: item.villageNameHi
                    }));
                });
            });
        }
    });


    //tehsil change bind village in step 3
    $("#LandTahsilDropdown").change(function () {
      
        var tahsilId = $(this).val();
        var villageDropdown = $("#LandVillageTypeDropdown");

        console.log("Selected Tehsil ID: " + tahsilId);

        // Clear previous items
        villageDropdown.empty();
        villageDropdown.append('<option value="">-- चयन करें --</option>');

        if (tahsilId) {
            $.getJSON('/Farmer/GetVillageByTehsil', { tahsilId: tahsilId }, function (data) {
                console.log(data);

                $.each(data, function (index, item) {
                    villageDropdown.append($('<option/>', {
                        value: item.villageId,
                        text: item.villageName
                    }));
                });
            });
        }
    });

});


$("#TahsilDropdown").change(function () {
    var tahsilId = $(this).val();
    var areaDropdown = $("#AreaTypeDropdown");
    console.log("Selected Tehsil ID: " + tahsilId);
    // Clear previous items
    areaDropdown.empty();
    areaDropdown.append('<option value="">-- चयन करें --</option>');

    if (tahsilId) {
        $.getJSON('/Farmer/GetAreaTypesByTehsil', { tahsilId: tahsilId }, function (data) {
            console.log(data);
            $.each(data, function (index, item) {
                areaDropdown.append($('<option/>', {
                    value: item.blockId,
                    text: item.blockName
                }));
            });
        });
    }
});


function showPreview() {

    const checkbox = document.getElementById('DeclarationAccepted');
    const preview = document.getElementById('preview');

    if (checkbox.checked) {
        //preview.style.display = 'block';
    } else {
        // Alert user if checkbox is not checked
        alert('Please accept the declaration before previewing.');
        preview.style.display = 'none';
    }
      // Step-1
    document.getElementById("pvFarmerId").innerText = document.getElementById("FarmerId").value;
    document.getElementById("pvFarmerName").innerText = document.querySelector("[name='FarmerName']").value;
    document.getElementById("pvMobileAgri").innerText = document.querySelector("[name='MobileNumberAgriStack']").value;
    document.getElementById("pvGenderAgri").innerText = document.querySelector("[name='Gender']").value;
    document.getElementById("pvAadhar").innerText = document.querySelector("[name='AadharCardNo']").value;
    document.getElementById("pvDOB").innerText = document.getElementById("DOB").value;

    // Step-2
    document.getElementById("pvState").innerText = document.querySelector("[name='State']").value;
    document.getElementById("pvDistrict").innerText = document.querySelector("[name='District']").value;
    document.getElementById("pvTahsil").innerText =
    document.getElementById("tahsilId").selectedOptions[0].text;
    document.getElementById("pvVillage").innerText =
    document.getElementById("VillageTypeDropdown").selectedOptions[0]?.text || "";
    document.getElementById("pvPincode").innerText = document.querySelector("[name='pincode']").value;
    document.getElementById("pvAddress").innerText = document.querySelector("[name='address']").value;

    // Land Grid
    let previewBody = document.querySelector("#previewLandTable tbody");
    previewBody.innerHTML = "";

    document.querySelectorAll("#LandGrid tbody tr").forEach(row => {
        let cloneRow = row.cloneNode(true);
    cloneRow.removeChild(cloneRow.lastElementChild); // remove Action column
    previewBody.appendChild(cloneRow);
    });

    // Show Modal
    new bootstrap.Modal(document.getElementById('previewModal')).show();
}

//function SendOTPtoValidate() {
//    var FarmerId = $('#MobileNumberAgriStack').val();
//    $.ajax({
//        type: 'POST',
//        url: '/Farmer/SearchFarmerByIdForAgriStack',
//        contentType: 'application/json',
//        data: JSON.stringify({ FarmerId }),
//        success: function (response) {
//            console.log(response.data);
//            if (response.status && response.data) {

//                $('#FarmerName').val(response.data.farmerName ?? '');
//                $('#Village').val(response.data.villageNameHi ?? '');
//                // $('#DOB').val(response.data.dob ?? '');
//                const dobValue = response.data.dob ?? '';

//                if (dobValue) {
//                    const formattedDate = formatToDDMMYYYY(dobValue);
//                    $('#DOB').val(formattedDate);
//                } else {
//                    $('#DOB').val('');
//                }
//                $('#AadharCardNo').val(response.data.aadharNo ?? '');
//                $('#Gender').val(response.data.gender ?? ''); // only if exists
//                $('#MobileNumberAgriStack').val(response.data.mobileNo ?? '');
//                $('#FatherHusbandName').val(response.data.fatherOrHusbandName ?? '');
//                $('#Area').val(response.data.cropArea ?? ''); // only if exists
//                $('#pincodeAgriStack').val(response.data.pincode ?? '');
//                $('#tahsilAgriStack').val(response.data.tehsilId);
//                $('#tahsilAgriStack').prop('disabled', true);
//                // $('#AreaType').val(response.data.blockId);
//                // $('#village').val(response.data.villageId);
//            }
//        }
//    });
//}

 function SendOTPtoValidate() {
        $.ajax({
            url: '/Farmer/GetOTP', 
            type: 'GET',
            success: function (otp) {
                console.log("OTP:", otp);

                showToast("आपके मोबाइल नंबर पर OTP भेज दी गई है। यह 2 मिनट तक मान्य है: " + otp);
  
                var otpModal = new bootstrap.Modal(document.getElementById('otpModal'));
                otpModal.show();

                // Focus first box
                document.querySelector('.otp-input').focus();
            },
            error: function () {
                showToast("Error generating OTP");
            }
        });
    }

 function showToast(message) {
        var toast = document.getElementById("toast");
    toast.innerText = message;
    toast.classList.add("show");

    setTimeout(function () {
        toast.classList.remove("show");
        }, 6000);
    }

document.addEventListener('input', function (e) {
    if (e.target.classList.contains('otp-input')) {
        if (e.target.value.length === 1) {
            let next = e.target.nextElementSibling;
            if (next) next.focus();
        }
    }
});
function VerifyOTPSession() {
        let otp = "";
        document.querySelectorAll('.otp-input').forEach(i => otp += i.value);

    if (otp.length !== 6) {
        alert("कृपया 6 अंकों का OTP दर्ज करें");
    return;
        }

    $.ajax({
    url: '/Farmer/VerifyOTP',
    type: 'POST',
    data: {otp: otp },
    success: function (isValid) {
      if (isValid === true) {
        $('#IsOtpVerified').val('true');

    bootstrap.Modal.getInstance(
    document.getElementById('otpModal')
    ).hide();

    //  Submit the form AFTER OTP verification
    document.getElementById('registrationForm').submit();

        } else {
          showToast("गलत OTP, कृपया पुनः प्रयास करें");
                }
            }
        });
    }

//OTP modal pop up start 
const otpInputs = document.querySelectorAll('.otp-input');

// Auto-focus and backspace handling
otpInputs.forEach((input, idx) => {
    input.addEventListener('input', (e) => {
        const value = e.target.value;
        if (value.length > 0 && idx < otpInputs.length - 1) {
            otpInputs[idx + 1].focus();
        }
    });

    input.addEventListener('keydown', (e) => {
        if (e.key === "Backspace" && !input.value && idx > 0) {
            otpInputs[idx - 1].focus();
        }
    });
});

// Countdown Timer
let timer = 120;
let otpInterval; // global interval variable

function startOTPTimer() {
    const timerEl = document.getElementById('otp-timer');

    if (otpInterval) {
        clearInterval(otpInterval);
    }

    timer = 120;
    timerEl.textContent = timer + "s";

    otpInterval = setInterval(() => {
        if (timer <= 0) {
            clearInterval(otpInterval);
            otpInterval = null;
            timerEl.textContent = "00s";
        } else {
            timer--;
            timerEl.textContent = timer + "s";
        }
    }, 1000);
}

// Resend OTP
function ResendOTP() {
    document.getElementById('otp-message').textContent = "";
    // showToast("OTP फिर से भेजा गया!");
    SendOTPtoValidate();
    startOTPTimer();
}

// Focus first input and start timer on modal open
const otpModal = document.getElementById('otpModal');
otpModal.addEventListener('shown.bs.modal', () => {
    otpInputs[0].focus();
    startOTPTimer();
});

// Verify OTP
function VerifyOTP() {
    let otp = '';
    otpInputs.forEach(input => otp += input.value);
    if (otp.length < 6) {
        document.getElementById('otp-message').textContent = "कृपया सभी 6 अंक भरें।";
        return;
    }
    VerifyOTPSession(); 
}

$('#otpModal').off('shown.bs.modal').on('shown.bs.modal', function () {

    otpInputs.forEach(input => input.value = '');
    document.getElementById('otp-message').textContent = '';

    if (otpInterval) {
        clearInterval(otpInterval);
        otpInterval = null;
    }

    startOTPTimer();

    // Focus first OTP box
    otpInputs[0].focus();
});

$('#otpModal').on('hidden.bs.modal', function () {

    otpInputs.forEach(input => input.value = '');
    document.getElementById('otp-message').textContent = '';

    if (otpInterval) {
        clearInterval(otpInterval);
        otpInterval = null;
    }
});


//reset the form after msg 
document.addEventListener("DOMContentLoaded", function () {
    // Check if there is a TempData message
    if (window.TempMessage) {
        alert(window.TempMessage);

        // Reset all inputs
        document.querySelectorAll("#registrationForm input").forEach(function (input) {
            if (input.type === "checkbox" || input.type === "radio") {
                input.checked = false;
            } else {
                input.value = "";
            }
        });

        // Reset all selects
        document.querySelectorAll("#registrationForm select").forEach(function (select) {
            select.selectedIndex = 0;
        });

        // Clear dynamic table
        var landGrid = document.getElementById("LandGrid");
        if (landGrid) {
            var tbody = landGrid.querySelector("tbody");
            if (tbody) tbody.innerHTML = "";
        }

        // Reset wizard steps to Step 1
        document.querySelectorAll(".wizard-step").forEach(function (step) {
            step.classList.remove("active");
        });
        var firstStep = document.querySelector(".wizard-step[data-step='1']");
        if (firstStep) firstStep.classList.add("active");

        // Reset hidden fields
        document.querySelectorAll("#registrationForm input[type=hidden]").forEach(function (input) {
            input.value = "";
        });

        // Remove the message so alert doesn't show on refresh
        window.TempMessage = null;
    }
});

//end reset form 

