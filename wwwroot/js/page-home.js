var map = new FlaMap(map_cfg);
map.drawOnDomReady('map-container');
map.on('click', function (ev, sid, map) {
    $.ajax({
        type: "GET",
        url: "/Home/GetMapRegionPartialView",
        data: { federalSubjectID: sid },
        beforeSend: function (xhr) {
            $('.simple-popup-content').html("Запрос...")
            $('.simple-popup').removeClass('d-none');
            $('.simple-popup').offset({ top: ev.onMapY - 0, left: ev.onMapX + $('.simple-popup').width() / 2 })
        },
    }).done(function (data, statusText, xhdr) {
        $(".simple-popup-content").html(data);
        $('.carousel').carousel('pause');
    }).fail(function (xhdr, statusText, errorText) {
        console.log("Failed");
        $("#tableContainer").text(JSON.stringify(xhdr));
    });
});

$("#btnTable").click(function () {

    HideMapPopup();

    $.ajax({
        type: "GET",
        url: "/Home/GetTablePartialView",
        beforeSend: function (xhr) {
            $('#btnTable').attr("disabled", true);
            $('#tableContainer').html("Запрос...")
        },
    }).done(function (data, statusText, xhdr) {
        console.log("Done");
        $("#tableContainer").html(data);
        $('#btnTable').attr("disabled", false);
        ConfigTableEvents();
    }).fail(function (xhdr, statusText, errorText) {
        console.log("Failed");
        $("#tableContainer").text(JSON.stringify(xhdr));
    });
});

/* $(function () {


}); */ 

function HideMapPopup() {
    $('.simple-popup').addClass('d-none');
    $(".simple-popup-content").html('');
}

function ConfigTableEvents() {
    $(".displayChildRows").click(function (e) {
        e.stopPropagation();

        HideMapPopup();

        var visibility = $(this).data('visibility');
        console.log('visibility ' + visibility);
        var id = $(this).data('id');
        console.log('id ' + id);

        if (parseInt(visibility) == 0) {
            console.log('false');
            $(this).data('visibility', '1');
            $('.childrow-' + id).each(function (i, obj) {
                console.log(obj);
                $(obj).removeClass('d-none');
            });

            $(this).children().first().removeClass("fa-arrow-right");
            $(this).children().first().addClass("fa-arrow-down");
        }
        else {
            console.log('true');
            $(this).data('visibility', '0');
            $('.childrow-' + id).each(function (i, obj) {
                console.log(obj);
                $(obj).addClass('d-none');
            });

            $(this).children().first().removeClass("fa-arrow-down");
            $(this).children().first().addClass("fa-arrow-right");
        }
    });
}
