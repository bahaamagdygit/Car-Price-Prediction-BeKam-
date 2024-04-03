
function initImageUpload(box) {
    let uploadField = box.querySelector('.image-upload');

    uploadField.addEventListener('change', getFile);

    function getFile(e) {
        let file = e.currentTarget.files[0];
        checkType(file);
    }

    function previewImage(file) {
        let thumb = box.querySelector('.js--image-preview'),
            reader = new FileReader();

        reader.onload = function () {
            thumb.style.backgroundImage = 'url(' + reader.result + ')';
        }
        reader.readAsDataURL(file);
        thumb.className += ' js--no-default';
    }

    function checkType(file) {
        let imageType = /image.*/;
        if (!file.type.match(imageType)) {
            throw 'Datei ist kein Bild';
        } else if (!file) {
            throw 'Kein Bild gewählt';
        } else {
            previewImage(file);
        }
    }

}

// initialize box-scope
var boxes = document.querySelectorAll('.box');

for (let i = 0; i < boxes.length; i++) {
    let box = boxes[i];
    initDropEffect(box);
    initImageUpload(box);
}



/// drop-effect
function initDropEffect(box) {
    let area, drop, areaWidth, areaHeight, maxDistance, dropWidth, dropHeight, x, y;

    // get clickable area for drop effect
    area = box.querySelector('.js--image-preview');
    area.addEventListener('click', fireRipple);

    function fireRipple(e) {
        area = e.currentTarget
        // create drop
        if (!drop) {
            drop = document.createElement('span');
            drop.className = 'drop';
            this.appendChild(drop);
        }
        // reset animate class
        drop.className = 'drop';

        // calculate dimensions of area (longest side)
        areaWidth = getComputedStyle(this, null).getPropertyValue("width");
        areaHeight = getComputedStyle(this, null).getPropertyValue("height");
        maxDistance = Math.max(parseInt(areaWidth, 10), parseInt(areaHeight, 10));

        // set drop dimensions to fill area
        drop.style.width = maxDistance + 'px';
        drop.style.height = maxDistance + 'px';

        // calculate dimensions of drop
        dropWidth = getComputedStyle(this, null).getPropertyValue("width");
        dropHeight = getComputedStyle(this, null).getPropertyValue("height");

        // calculate relative coordinates of click
        // logic: click coordinates relative to page - parent's position relative to page - half of self height/width to make it controllable from the center
        x = e.pageX - this.offsetLeft - (parseInt(dropWidth, 10) / 2);
        y = e.pageY - this.offsetTop - (parseInt(dropHeight, 10) / 2) - 30;

        // position drop and animate
        drop.style.top = y + 'px';
        drop.style.left = x + 'px';
        drop.className += ' animate';
        e.stopPropagation();

    }
}

function getobj(name,id,id1) {
    var val = document.getElementsByName(name).value;
    if (val != "") {
        id.style.border = "2px solid green";
        id1.innerHTML = "<i class='fa fa-check bg-light rounded-pill p-2 text-success '>This value is correct</i>";
    }
    else {
        id.style.border = "2px solid green";
    }
}


function change_color(labl, checkboxName, labelname) {

    clr = document.getElementsByName(checkboxName);
    labls2 = document.getElementsByName(labelname);
    for (var i = 0; i < clr.length; i++) {

        if (clr[i].type === 'radio' && clr[i].checked) {
            labl.style.backgroundColor = "#FF914D";
            clr[i].style.color = "#F77D0A"
        } else {
            for (var e = 0; e < labls2.length; e++) {
                if (labls2[e] != labl) {
                    labls2[e].style.backgroundColor = "";
                    labls2[e].style.border = "";
                }
            }
            clr[i].style.color = ""
        }

    }
}


/* Form Validator*/

const validateForm = form => {
    const inputs = form.querySelectorAll('.validate');
    for (const input of inputs) {

        input.classList.remove('invalid');
        input.parentNode.classList.remove('invalid');

        let allOK = true;

        switch (input.dataset.validation) {
            case 'regex': {
                allOK = input.value.match(new RegExp(input.dataset.regex));
                break;
            }
            case 'match': {
                allOK = input.value === form.querySelector(input.dataset.target).value;
                break;
            }
            default: {
                allOK = false;
                break;
            }
        } //end of switch

        if (!allOK) {
            input.classList.add('invalid');
            input.parentNode.classList.add('invalid');
            return false;
        }
    } //end of for-of

}