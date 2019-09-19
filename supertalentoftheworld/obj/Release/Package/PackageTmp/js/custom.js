/*
 Template Name: vacayhome
 File Name: custom.js
 Author Name: ThemeVault
 Author URI: http://www.themevault.net/
 License URI: http://www.themevault.net/license/
 */



//function initNavigation() {
//    var url = location.href;

//    var split_url = url.split('/');

//    var split_url_len = split_url.length;

//    var controller = split_url[split_url_len - 2]
//    var page = split_url[split_url_len - 1].toLowerCase();

//    console.log(controller);
//    console.log(page);

//    switch (controller) {      
//        case "Home":
//            $("#nav_ab")
//        case "Finalists":
//            $('#nav_final').addClass('active');
//            break;
//        case "Partners":
//            $('#nav_partner').addClass('active');
//            break;
//        case "Channel":
//            $('#nav_channel').addClass('active');
//            break;
//        case "Collaboration":
//            $('#nav_collabo').addClass('active');
//            break;
//        case "Apply":
//            $('#nav_apply').addClass('active');
//            break;
//        case "Shop":
//            $('#nav_shop').addClass('active');
//            break;
//    }

    
//}



$(document).ready(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('#back-to-top').fadeIn();
        } else {
            $('#back-to-top').fadeOut();
        }
    });
    $('#back-to-top').click(function () {
        $("html, body").animate({scrollTop: 0}, 600);
        return false;
    });

});


