$(document).ready(function(){
    $('.slick-class').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 2000,
    });
});

if ($('.css-filter').is(':visible')) {
    $('body').addClass('full-page-filter');
}

if ($('.css-create-phone').is(':visible')) {
    $('body').addClass('full-page-add');
}

if ($('.form-edit').is(':visible')) {
    $('body').addClass('full-page-edit');
}