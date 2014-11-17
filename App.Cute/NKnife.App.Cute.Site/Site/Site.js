
$(function() {
    activeNavMenuItem = $("#menu-enginestater");
    activeMenuListByNav = $("#menulist-enginestater");
    $("#menulist-clientoption").hide();
    $("#menulist-loggerview").hide();
    $("#menulist-engineoption").hide();

    $("#menu-enginestater").live("click", function() {
        navMenuClick("#menu-enginestater", "#menulist-enginestater");
    });

    $("#menu-clientoption").live("click", function() {
        navMenuClick("#menu-clientoption", "#menulist-clientoption");
    });

    $("#menu-loggerview").live("click", function() {
        navMenuClick("#menu-loggerview", "#menulist-loggerview");
    });

    $("#menu-engineoption").live("click", function() {
        navMenuClick("#menu-engineoption", "#menulist-engineoption");
    });
});

/*导航区菜单中被点击的项目*/
var activeNavMenuItem;
/*当导航区菜单中被点击时关联的内容区菜单列表*/
var activeMenuListByNav;
/*内容区左侧菜单中被点击的项目*/
var activeLeftMenuItem;

function navMenuClick(self, menuList) {
    if (activeNavMenuItem != null)
        activeNavMenuItem.removeClass("nav-menu-active");
    if (activeMenuListByNav != null)
        activeMenuListByNav.hide();

    activeNavMenuItem = $(self);
    activeNavMenuItem.addClass("nav-menu-active");
    activeMenuListByNav = $(menuList);
    activeMenuListByNav.show(300);
}

function menuItemClick(item) {
    if (activeLeftMenuItem != null)
        activeLeftMenuItem.removeClass("menu-item-active");
    activeLeftMenuItem = $(item);
    activeLeftMenuItem.addClass("menu-item-active");
}