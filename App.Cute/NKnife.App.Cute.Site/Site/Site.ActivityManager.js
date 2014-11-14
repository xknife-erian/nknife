/*当点击“流水线动作管理”时*/
$("#action-activity-manager").live('click', function () {
    menuItemClick("#action-activity-manager");
    activeLeftMenuItem.addClass("menu-item-active");
    mainClear();
    setActivityManagerTable();
});

function setActivityManagerTable() {
    var jqgrid = "<table id='jqgrid'></table> <div id='jqgrid-pager-nav'></div>";
    $(".panel").append(jqgrid);
    $('#jqgrid').jqGrid({
        jsonReader: {
            root: "Data",
            repeatitems: false
        },
        autowidth: true,
        shrinkToFit: false,
        height: '100%',
        url: 'Activity/List',
        datatype: 'json',
        mtype: 'POST',
        colNames: ['分类编号', '描述'],
        colModel: [
            { name: 'Id', index: 'Id', align: 'center', width: 80 },
            { name: 'Description', index: 'Description', width: 700 }
        ],
        sortname: 'Id',
        pager: 'jqgrid-pager-nav',
        rowNum: 10,
        rowList: [10, 20, 30],
        caption: '流水线动作列表',
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
}