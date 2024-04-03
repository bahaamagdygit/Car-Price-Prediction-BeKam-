var usernameError = document.getElementById('username-error');
var nameError = document.getElementById('name-error');
var emailError = document.getElementById('email-error');
var phoneError = document.getElementById('phone-error');
var passError = document.getElementById('pass-error');
var ConfirmPassError = document.getElementById('Confirm-pass-error');
var submitError = document.getElementById('submit-error');


function validateUserName() {

    var username = document.getElementById('User_Name').value;
    usernameError.style.color = 'red';

    if (username.length == 0) {
        usernameError.innerHTML = 'UserName is required';
        //    nameError.style.color = 'red';
        return false;
    }

    var username_re = /^[A-Za-z][A-Za-z0-9_]{3,29}$/;
    if (!username.match(username_re)) {
        usernameError.innerHTML = 'Write Valid UserName';
        return false;
    }

    usernameError.innerHTML = 'valid';
    usernameError.style.color = 'seagreen';
    return true;

}

function validateName() {

    var name = document.getElementById('Name').value;
    nameError.style.color = 'red';

    if (name.length == 0) {
        nameError.innerHTML = 'Name is required';
        return false;
    }

    var name_re = /^[A-Za-z]{2,9}\s*[A-Za-z]*\s[A-Za-z]*$/;
    if (!name.match(name_re)) {
        nameError.innerHTML = 'Write Valid Name';
        // nameError.style.color = 'red';
        return false;
    }

    nameError.innerHTML = 'valid';
    nameError.style.color = 'seagreen';
    return true;

}

function validateEmail() {

    var email = document.getElementById('Email').value;

    if (email.length == 0) {

        emailError.innerHTML = 'E-mail is required';
        emailError.style.color = 'red';
        return false;
    }

    var Re_Email = /[a-z0-9]+@[a-z]+\.[a-z]{2,3}/;
    if (!email.match(Re_Email)) {
        emailError.innerHTML = 'E-mail Invalid';
        emailError.style.color = 'red';
        return false;
    }

    emailError.innerHTML = 'valid';
    emailError.style.color = 'seagreen';
    return true;

}

function validatePhone() {


    var phone = document.getElementById('phone_num').value;
    phoneError.style.color = 'red';


    if (phone.length == 0) {
        phoneError.innerHTML = 'Phone is required';
        //phoneError.style.color = 'red';

        return false;
    }
    if (phone.length != 11) {

        phoneError.innerHTML = 'Phone number should be 11 digits';
        // phoneError.style.color = 'red';
        return false;
    }

    if (!phone.match(/^[0-9]{11}$/)) {
        phoneError.innerHTML = 'Only digits please';
        // phoneError.style.color = 'red';
        return false;
    }

    phoneError.innerHTML = 'valid';
    phoneError.style.color = 'seagreen';
    return true;

}
function validatePass() {

    var Password = document.getElementById('Password').value;
    passError.style.color = 'red';

    if (Password.length == 0) {
        passError.innerHTML = 'Password is required';
        return false;
    }
    var pass_re1231 = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;
    if (!Password.match(pass_re1231)) {
        passError.innerHTML = 'Min 8 characters, at least 1 letter and 1 number';
        return false;
    }

    passError.innerHTML = 'valid';
    passError.style.color = 'seagreen';
    return true;

}

function ConfirmPass() {

    var Password = document.getElementById('Password').value;
    console.log(Password)
    var ConfirmPass = document.getElementById('Confirm_Password').value;
    console.log(ConfirmPass)
    ConfirmPassError.style.color = 'red';

    if (Password != ConfirmPass || ConfirmPass == "") {
        ConfirmPassError.innerHTML = 'Password Does Not Match';
        return false;
    }
    else {
        ConfirmPassError.innerHTML = 'Matched';
        ConfirmPassError.style.color = 'seagreen';
        return true;
    }


}

function validateForm() {
    if (!validateUserName() || !validateName() || !validateEmail() || !validatePhone() || !validatePass() || !ConfirmPass()) {
        //onsubmit = "return validateForm()
        submitError.style.display = 'block'
        submitError.innerHTML = 'Please fix error to submit'
        setTimeout(function () { submitError.style.display = 'none' }, 3000)
        return false;


    }
}