$(function () {

    // Check for the various File API support.
    if (!window.FileReader) {
        alert('您的浏览器不支持照片上传，请升级您的浏览器到最新版本！');
    }

    $("#File").change(function (evt) {
        var f = evt.target.files[0];
        var reader = new FileReader();

        if (!f.type.match("image.*")) {
            alert("The selected file does not appear to be an image.");
            return;
        }

        reader.onload = function (e) {
            LoadPhoto(GetFileNameNoExt(f.name), e.target.result);
        };
        reader.readAsDataURL(f);
    });

    $('form').submit(function () {
        $('button').attr('disabled', true).text('请稍等 ...');
    });
});

//取文件名不带后缀
function GetFileNameNoExt(filepath) {
    if (filepath != "") {
        var names = filepath.split("\\");
        var pos = names[names.length - 1].lastIndexOf(".");
        return names[names.length - 1].substring(0, pos);
    }
}

//选择图片后的事件
function LoadPhoto(name, src) {
    $('#Title').val(name);
    $("#divPreview").html("<img id='preview' width='500' src='" + src + "' />");
    $("#divPreview").fadeIn(3000);
}