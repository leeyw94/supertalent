
function delete_it() {
    if (confirm("게시글을 삭제하시겠습니까?")) {
        $('#DelForm').submit();
    } else {
        alert('취소되었습니다.');
    }

}



function AddFile(obj, _id, _name) {

    var filearea = $("#" + _id);

    filearea.append('<div class="col-md-10" style="margin:0;padding:0;margin-bottom:10px;"><span style="float:left;"> <input type="file" class="btn btn_file"  name="' + _name + '" multiple /></span> <button type="button" rel="tooltip" title="Remove" class="btn btn-danger btn-simple btn-xs"  style="float:left;padding-left:15px;"  onclick="RemoveFile($(this))"> <img src="/images/common/s_icon_del.gif" /></button></div>');
}

function RemoveFile(obj) {
    var file = $(obj).parent();
    if (confirm("선택한 파일이 삭제됩니다.\r\n삭제하시겠습니까?")) {
        file.remove();
    }
}



function fileUpload(img_t, img_width, img_height) {


    $('#myfile_' + img_t).click();


    $('#myfile_' + img_t).on('change', function() {

        imgUpload(img_t, img_width, img_height);
    });
    imgUpload(img_t, img_width, img_height);

}

function imgUpload(img_t, img_width, img_height) {


    var formData = new FormData();


    formData.append("file", $('#myfile_' + img_t)[0].files[0]);


    var ext = $('#myfile_' + img_t).val().split('.').pop().toLowerCase();


    if (ext.length > 0) {

        if ($.inArray(ext, ["gif", "png", "jpg", "jpeg"]) == -1) {
            alert("gif,png,jpg 파일만 업로드 할수 있습니다.");
            return false;
        }
    }


    var url_it = "/admin/SingleFileUpload?img_t=" + img_t + "&img_width=" + img_width + "&img_height=" + img_height;
    var URL = url_it;


    $.ajax({
        url: url_it,
        data: formData,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function(data) {

            $("#photo_" + img_t).val(data);
            $("#pic_" + img_t).attr('src', data);
            $("#pic_" + img_t).attr("class", "no_imge_after");


        },
        error: function(request, status, error) {

            console.log("code:" + request.status + "\n" + "message:" + request.responseText + "\n" + "error:" + error);

        }
    });

}


function fileUp(_gubun) {


    $('input[name = "file_' + _gubun + '"]').on('change', function() {


        fileUp_action(_gubun);


    });
}

function fileUp_action(_gubun) {


    var t = 0;

    var _name = $('input[name = "file_' + _gubun + '"]');
    var file_sn = $("#file_sn").val();


    for (var k = 0; k < _name.length; ++k) {

        var formData = new FormData();

        for (var i = 0; i < _name.get(0).files.length; ++i) {


            formData.append("file", $('input[name = "file_' + _gubun + '"]')[k].files[i]);


        }


        t = ++t;
    }


    var url_it = "/admin/MultiFileUpload?file_gubun=" + _gubun + "&file_sn=" + file_sn;
    var URL = url_it;


    $.ajax({
        url: url_it,
        data: formData,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function(data) {


        },
        error: function(request, status, error) {


            console.log("code:" + request.status + "\n" + "message:" + request.responseText + "\n" + "error:" + error);

        }
    });

}


function AddFile2(obj, _id, _gubun) {

    $(".file_list_none").css("display", "block");


    var filearea = $("#" + _id);
    var _name = "file_" + _gubun;

    filearea.append("<div class='col-md-10' style='margin:0;padding:0;margin-bottom:10px;'><span style='float:left;'> <input type='file' class='btn btn_file'  name='" + _name + "' multiple  onclick = 'fileUp(" + _gubun + ")' /></span> <button type='button' rel='tooltip' title='Remove' class='btn btn-danger btn-simple btn-xs'  style='float:left;padding-left:15px;'  onclick='RemoveFile2($(this))'> <img src='/images/common/s_icon_del.gif' /></button></div>");
}

function RemoveFile2(obj) {
    var formData = new FormData();


    formData.append("text", $('#fileName'));

    if (confirm("선택한 파일이 삭제됩니다.\r\n삭제하시겠습니까?")) {


        var file_sn = $("#file_sn").val();
        var file = $(obj).parent().children('span').children('input');
        var file_set = $(obj).parent();
        var fileValue = file.val().split("\\");
        var fileName = fileValue[fileValue.length - 1]; // 파일명
        var state = "N";


        var _fileName = encodeURI(fileName);

        var url_it = "/admin/MultiFileUpload_del?file_name=" + _fileName + "&file_sn=" + file_sn;     
       
       
        $.ajax({                  
            url: url_it,
            processData: false,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            type: 'POST',
            success: function(data) {

                file_set.remove();

            },
            error: function(request, status, error) {


                console.log("code:" + request.status + "\n" + "message:" + request.responseText + "\n" + "error:" + error);

            }            
        });


    } else {
        return;
    }
}


function RemoveFile3(obj, f_name) {
    var formData = new FormData();


    formData.append("text", $('#fileName'));
    var file_set = $(obj).parent().parent();


    if (confirm("선택한 파일이 삭제됩니다.\r\n삭제하시겠습니까?")) {


        var file_sn = $("#file_sn").val();

        var state = "N";

        var _fileName = encodeURI(f_name);
        var url_it = "/admin/MultiFileUpload_del?file_name=" + _fileName + "&file_sn=" + file_sn;
     

        $.ajax({
            url: url_it,
            processData: false,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            type: 'POST',
            success: function(data) {

                file_set.remove();

            },
            error: function(request, status, error) {


                console.log("code:" + request.status + "\n" + "message:" + request.responseText + "\n" + "error:" + error);

            }
        });


    } else {
        return;
    }
}


function board_imgUpload(img_t, img_width, img_height) {


    var formData = new FormData();


    formData.append("file", $('#myfile_' + img_t)[0].files[0]);


    var ext = $('#myfile_' + img_t).val().split('.').pop().toLowerCase();


    if (ext.length > 0) {

        if ($.inArray(ext, ["gif", "png", "jpg", "jpeg"]) == -1) {
            alert("gif,png,jpg 파일만 업로드 할수 있습니다.");
            return false;
        }
    }


    var url_it = "/admin/SingleFileUpload?img_t=" + img_t + "&img_width=" + img_width + "&img_height=" + img_height;
    var URL = url_it;


    $.ajax({
        url: url_it,
        data: formData,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function(data) {

            $("#photo_" + img_t).val(data);
            $("#pic_" + img_t).attr('src', data);
            $("#pic_" + img_t).attr("class", "no_imge_after");


        },
        error: function(request, status, error) {

            console.log("code:" + request.status + "\n" + "message:" + request.responseText + "\n" + "error:" + error);

        }
    });

}

function go(a) {

    location.href = a;

}
function check(a, c) {


    var a_check = a.split(",");
    // var b_check = b.split(",");

    var i = 0;
    for (var idx in a_check) {
        try {

            var check_it = $("#" + a_check[idx]).val();

            if (check_it == "") {
                //alert(b_check[i] + " is required.");
                $("#" + a_check[idx]).css("border", "solid 1px red");
                $("#" + a_check[idx]).focus();

                return;

            } else {
                $("#" + a_check[idx]).css("border", "solid 1px #e3e3e3");

            }


            i++;

        } catch (e) {
            return;
        }
    }

    $("#" + c).submit();

}
function check2(a, c) {


    var a_check = a.split(",");
    // var b_check = b.split(",");

    var i = 0;
    for (var idx in a_check) {
        try {

            var check_it = $("#" + a_check[idx]).val();

            if (check_it == "") {
                //alert(b_check[i] + " is required.");
                $("#" + a_check[idx]).css("border", "solid 1px red");
                $("#" + a_check[idx]).focus();

                return;

            } else {
                $("#" + a_check[idx]).css("border", "solid 1px #e3e3e3");

            }


            i++;

        } catch (e) {
            return;
        }
    }
    $("#" + c).attr("action", "user_set_action?my=Y").submit();
   

}
function addComma(n) {
    var reg = /(^[+-]?\d+)(\d{3})/;
    n += '';

    while (reg.test(n)) {
        n = n.replace(reg, '$1' + ',' + '$2');
    }

    return n;
}

function searchlist() {


    //로딩바=====================================================================================================================================================
    $('<div id="loading" style="position: relative; top:50%; left:50%;z-index:20;"> <img src ="/images/common/ajax-loader_blue.gif" /></div>')
        .insertBefore('#result').ajaxStart(function () {
            $(this).show();

        }).ajaxStop(function () {
            $(this).hide();
        });
    //========================================================================================================================================================

    // 로딩바 끝===================================================================================================================================== 


    document.bbsform.submit();

    return false;
}

//function check_one(a,b){
//    var URL = "/ContractManageMent/aj2?doc_seq=" + b;   // ContractManageMent

//$.get(URL, function (data) {

//    $("#Result").html(data);
//});
//}


function del(url, idx) {


    if (confirm('Are you sure you want to delete it?')) {


        var url_full = url + "?idx=" + idx + "&mode_type=D";
        location.href = url_full;


    } else {
        return;
    }

}

function del_it(url) {


    if (confirm('선택한 항목을 취소 또는 삭제하시겠습니까?')) {


        var url_full = url + "&mode_type=D";

        location.href = url_full;


    } else {
        return;
    }

}

function del_imsi(url) {


    if (confirm('Are you sure you want to delete it?')) {


        var url_full = url + "&mode_type=E";

        location.href = url_full;


    } else {
        return;
    }

}


function go_set(url, pp) {

    var url_v = $("#" + pp + " option:selected").val();
    var _target = url + "?" + pp + "=" + url_v;

    location.href = _target;
}


// 숫자만 입력================================================================
$(".numOnly").css({ "ime-mode": "disabled" }); // 소수점 가능


$(".telOnly").css({ "ime-mode": "disabled" }); // 숫자


$('.telOnly').keyup(function (event) {
    $(this).val($(this).val().replace(/[^0-9]/g, ''));
});


$('.telOnly').keypress(function (event) {


    if (event.which && (event.which > 47 && event.which < 58 || event.which == 8)) {
    } else {


        event.preventDefault();
    }
});

$('.numOnly').keypress(function (event) {


    if (event.which && (event.which > 47 && event.which < 58 || event.which == 8 || event.which == 46)) {

    } else {


        event.preventDefault();
    }
});