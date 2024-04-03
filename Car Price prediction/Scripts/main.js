
function reset_dropdown_year() {
    document.getElementById('year').value = "";
    document.getElementById('year1').value = "";
}
function reset_dropdown_hp() {
    document.getElementById('hp').value = "";
    document.getElementById('hp1').value = "";
}
function rests_radio_button(Name) {
        radio = document.getElementsByName(Name);
        for (var i = 0; i < radio.length; i++) {
            radio[i].checked = false
            radio[i].style.color = ""
        }

} 
function rests_all_radio_button() {
    $("input[type=radio]").each(function () {
        $(this).prop('checked', false);
        $(this).css({ "color": "" });
    });
}

function restBody(labl, checkboxName, labelname) {
    clr = document.getElementsByName(checkboxName);
    labls2 = document.getElementsByName(labelname);
    for (var i = 0; i < clr.length; i++) {
           clr[i].checked == false;
            labl.style.backgroundColor = "";
            clr[i].style.color = ""
            for (var e = 0; e < labls2.length; e++) {
                if (labls2[e] != labl) {
                    labls2[e].style.backgroundColor = "";
                }
            }
           
        }
}

clr1 = document.getElementsByClassName("set_color");
for (var i = 0; i < clr1.length; i++) {
    if (clr1[i].type === 'radio' && clr1[i].checked) {
        clr1[i].parentElement.style.backgroundColor = "#FF914D"
    }
    else {
        clr1[i].parentElement.style.backgroundColor = ""
    }

}


function rest_check_box(Cname) {
    console.log(Cname)
        nodeList1 = document.getElementsByClassName(Cname);
        for (var i = 0; i < nodeList1.length; i++) {
            nodeList1[i].checked = false
        }
   
}
function rest_all_check_box() {
        $("input[type=checkbox]").each(function () {
            $(this).prop('checked', false);
        });
}

function ResetFormWithJS() {
    reset_dropdown_year();
    reset_dropdown_hp();
    rests_all_radio_button();
    rest_all_check_box();

}

(function ($) {
    "use strict";
    // Dropdown on mouse hover
    $(document).ready(function () {
        function toggleNavbarMethod() {
            if ($(window).width() > 992) {
                $('.navbar .dropdown').on('mouseover', function () {
                    $('.dropdown-toggle', this).trigger('click');
                }).on('mouseout', function () {
                    $('.dropdown-toggle', this).trigger('click').blur();
                });
            } else {
                $('.navbar .dropdown').off('mouseover').off('mouseout');
            }
        }
        toggleNavbarMethod();
        $(window).resize(toggleNavbarMethod);
    });


    // Date and time picker
    $('.date').datetimepicker({
        format: 'L'
    });
    $('.time').datetimepicker({
        format: 'LT'
    });


    // Initiate the wowjs
    new WOW().init();

    // Team carousel
    $(".team-carousel, .related-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1000,
        center: true,
        margin: 30,
        dots: false,
        loop: true,
        nav: true,
        navText: [
            '<i class="fa fa-angle-left" aria-hidden="true"></i>',
            '<i class="fa fa-angle-right" aria-hidden="true"></i>'
        ],
        responsive: {
            0: {
                items: 1
            },
            576: {
                items: 1
            },
            768: {
                items: 2
            },
            992: {
                items: 3
            }
        }
    });


    // Testimonials carousel
    $(".testimonial-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1500,
        margin: 30,
        dots: true,
        loop: true,
        center: true,
        responsive: {
            0: {
                items: 1
            },
            576: {
                items: 1
            },
            768: {
                items: 2
            },
            992: {
                items: 3
            }
        }
    });


    // Vendor carousel
    $('.vendor-carousel').owlCarousel({
        loop: true,
        margin: 30,
        dots: true,
        loop: true,
        center: true,
        autoplay: true,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 2
            },
            576: {
                items: 3
            },
            768: {
                items: 4
            },
            992: {
                items: 5
            },
            1200: {
                items: 6
            }
        }
    });

})(jQuery);

// Back to top button
$(window).scroll(function () {
    if ($(this).scrollTop() > 100) {
        $('.back-to-top').fadeIn('slow');
    } else {
        $('.back-to-top').fadeOut('slow');
    }
});
$('.back-to-top').click(function () {
    $('html, body').animate({ scrollTop: 0 }, 1500, 'easeInOutExpo');
    return false;
});


// slider carousel
$(".snslader").owlCarousel({
    autoplay: true,
    smartSpeed: 1000,
    center: true,
    margin: 30,
    dots: false,
    loop: true,
    nav: true,
    navText: [
        '<i class="fa fa-angle-left" aria-hidden="true"></i>',
        '<i class="fa fa-angle-right" aria-hidden="true"></i>'
    ],
    responsive: {
        0: {
            items: 1
        },
        576: {
            items: 2
        },
        768: {
            items: 3
        },
        992: {
            items: 5
        }
    }
});