
/*当点击“流水线动作管理”时*/
$("#action-log-list").live('click', function() {
    menuItemClick("#action-log-list");
    activeLeftMenuItem.addClass("menu-item-active");
    mainClear();
    setLogListTable();
});

function setLogListTable() {
    var jqGridCode = "<table id='jqgrid'></table> <div id='jqgrid-pager-nav'></div>";
    $(".panel").append(jqGridCode);
    $('#jqgrid').jqGrid({
        jsonReader: {
            root: "Data",
            repeatitems: false
        },
        autowidth: true,
        shrinkToFit: false,
        height: '100%',
        url: 'Log/List',
        datatype: 'json',
        mtype: 'POST',
        colNames: ['时间', '详细信息', '日志等级', '日志源'],
        colModel: [
            { name: 'Id', index: 'Id', width: 150, formatter: formatTime },
            { name: 'Info', index: 'Info', width: 550 },
            { name: 'Level', index: 'Level', width: 60 },
            { name: 'Src', index: 'Src', width: 200 }
        ],
        sortname: 'Time',
        pager: 'jqgrid-pager-nav',
        viewrecords: true, //在分页导航条中显示页数相关的信息
        rowNum: 50,
        rowList: [10, 20, 30, 40, 50, 100, 200],
        caption: '系统运行日志',
        multiselect: true
    });
    //定义按键  
    $('#jqgrid').navGrid('#jqgrid-pager-nav', {
        refresh: true,
        edit: false,
        add: false,
        del: false,
        search: true
    });

    function formatTime(input) {
        var date = Date.parseTicks(input);
        var result = date.format("Y-m-d H:i:s:u");
        return result; //把时间截取出来
    }
}
