jQuery(document).ready(function () {
    var link = $("#linkPortal");

    if (link != null)
    {
        var href = $(link).attr('href');
        var anchorValue;
        var url = document.location;
        var strippedUrl = url.toString().split("#");

        if (strippedUrl.length > 1) {
            anchorvalue = strippedUrl[1];
            $(link).attr('href', href + '?formulario=' + anchorvalue);
        }
    }
});


