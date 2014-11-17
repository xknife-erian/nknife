
/*当点击“启动引擎”时*/
$("#action-engine-start").live('click', function () {
    
    menuItemClick("#action-engine-start");
    
    $.ajax({
        url: "/EngineStarter/Start",
        cache: false,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        statusCode: {
            200: function (result) {
                mainClear();
                mainShowMessage(result);
            }
        }
    });
});

/*当点击“重新启动引擎”时*/
$("#action-engine-restart").live('click', function () {

    menuItemClick("#action-engine-restart");

    $.ajax({
        url: "/EngineStarter/ReStart",
        cache: false,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        statusCode: {
            200: function (result) {
                mainClear();
                mainShowMessage(result);
            }
        }
    });
    activeLeftMenuItem.addClass("menu-item-active");
});

/*当点击“关闭引擎”时*/
$("#action-engine-stop").live('click', function() {

    menuItemClick("#action-engine-stop");

    $.ajax({
        url: "/EngineStarter/Stop",
        cache: false,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        statusCode: {
            200: function(result) {
                mainClear();
                mainShowMessage(result);
            }
        }
    });
    activeLeftMenuItem.addClass("menu-item-active");
});

function mainClear() {
    $(".panel").empty();
}

function mainShowMessage(message) {
    var html = "";
    if (message == true) {
        html = "<p>系统运行成功。</p>";
    }
    else {
        html = "<p>系统运行异常。</p>";
    }
    $(".panel").append(html);
}