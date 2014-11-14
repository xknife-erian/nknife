/*当点击“用户管理”时*/
$("#action-user-manager").live('click', function () {
	menuItemClick("#action-user-manager");
	activeLeftMenuItem.addClass("menu-item-active");
	mainClear();
	setUserManagerTable();
});

function setUserManagerTable() {
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
        url: 'User/List',
        datatype: 'json',
        mtype: 'POST',
        colNames: ['用户编号', '登录名', '名称', '自定义编号', '手机号码', '电子邮件'],
        colModel: [
            { name: 'Id', index: 'Id', width: 80 },
            { name: 'LoginName', index: 'LoginName', width: 160 },
            { name: 'Name', index: 'Name', width: 100 },
            { name: 'Number', index: 'Number', width: 100 },
            { name: 'MobilePhone', index: 'MobilePhone', width: 140 },
            { name: 'Email', index: 'Email', width: 220 }
        ],
        pager: 'jqgrid-pager-nav',
        rowNum: 10,
        rowList: [10, 20, 30],
        caption: '用户管理',
        multiselect: true
    });

    //定义按键  
    $('#jqgrid').navGrid('#jqgrid-pager-nav', {
        refresh: true,
        edit: true,
        add: true,
        del: true,
        search: true
    });
}