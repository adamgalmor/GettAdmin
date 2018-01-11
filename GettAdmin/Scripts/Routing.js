//$(function () {
//    $("#dialog").dialog({
//        autoOpen: false,
//        show: {
//            effect: "blind",
//            duration: 1000
//        },
//        hide: {
//            effect: "explode",
//            duration: 1000
//        },
//        create: function() {
//            $(this).closest('div.ui-dialog')
//                    .find('.ui-dialog-titlebar-close')
//                    .click(function(e) {
//                        app.setLocation("");
//                    });
//        }
//    });

//    //var routing = new Routing('@Url.Content("~/")', '#page', 'Drivers');            
//    //routing.init();

//    GetDrivers();
//    GetRiders();
//    GetActiveOrders();
//    GetInactiveOrders();

//});

////initialize "SAMMY"
//var app = $.sammy(function () {
           
//    this.get('/', function () {
//        //alert('/');
//    });

//    this.get('riders/Create/:id?:name', function () {
//        var queries = {};
//        $.each(document.location.search.substr(1).split('&'), function (c, q) {
//            var i = q.split('=');
//            queries[i[0].toString()] = i[1].toString();
//        });
//        $.ajax({
//            method: 'get',
//            url: '/orders/create/' + this.params["name"] + "?name=" + queries["name"]
//        }
//           ).done(function (data) {
//               $("dialog").empty();
//               $("#dialog").html(data);
//               $("#dialog").dialog("open");
//           });
//    });

//    this.post('orders/create/:id?:name', function () {
//        let dataObj = { Destination: this.params["Destination"], Origin: this.params["Origin"] };
//        $.ajax({
//            type: 'post',
//            url: CreateOrderURL,
//            data: dataObj,
//            success: function (data) {
//                $("#dialog").dialog("close");
//                GetInactiveOrders();
//                app.setLocation("")
//            }
//        })

//    });

//    this.get('orders/Assign/:id', function () {
//        $.ajax({
//            method: 'get',
//            url: '/orders/Assign/' + this.params['id']
//        }
//           ).done(function (data) {
//               $("dialog").empty();
//               $("#dialog").html(data);
//               $("#dialog").dialog("open");
//           });
//    });

//    this.post('orders/Assign/:id', function () {
//        let dataObj = { SelectedDriverID: this.params["AvailabelDriversDDL"], id: this.params["id"] };
//        $.ajax({
//            type: 'post',
//            url: AssignOrderURL,
//            data: dataObj,
//            success: function (data) {
//                $("#dialog").dialog("close");
//                GetActiveOrders();
//                GetInactiveOrders();
//                app.setLocation("")
//            }
//        })
//    });

//    this.get('drivers/Create', function () {
//        $.ajax({
//            method: 'get',
//            url: '/drivers/Create'
//        }
//           ).done(function (data) {
//               $("dialog").empty();
//               $("#dialog").html(data);
//               $("#dialog").dialog("open");
//           });
//    });

//    this.post('drivers/Create', function () {
//        let dataObj = { Name: this.params["Name"], Phone: this.params["Phone"], Email: this.params["Email"] };
//        $.ajax({
//            type: 'post',
//            url: CreateDriverURL,
//            data: dataObj,
//            success: function (data) {
//                if (data.indexOf("errors") != -1) {
//                    $("#dialog").empty();
//                    $("#dialog").html(data);
//                }
//                else {
//                    $("#dialog").dialog("close");
//                    $('#divDrivers').empty();
//                    GetDrivers();
//                    app.setLocation("");
//                }
//            },
//            error: function (xhr, error) {
//                console.log(xhr);
//                console.error(error);
//            }
//        })
//    });

//}).run('');

//var GetRiders = function () {
//    $.ajax({
//        method: 'get',
//        url: '/riders/'
//    }
//       ).done(function (dataRcvd) {
//           $('#divRiders').empty;
//           $('#divRiders').html(dataRcvd);
//       });
//}
//var GetDrivers= function () {
//    $.ajax({
//        method: 'get',
//        url: '/drivers/'
//    }
//       ).done(function (dataRcvd) {
//           $('#divDrivers').empty();
//           $('#divDrivers').html(dataRcvd);
//       });
//}
//var GetActiveOrders = function () {
//    $.ajax({
//        method: 'get',
//        url: '/orders/'
//    }
//       ).done(function (dataRcvd) {
//           $('#divActiveOrders').empty();
//           $('#divActiveOrders').html(dataRcvd);
//       });
//}
//var GetInactiveOrders = function () {
//    $.ajax({
//        method: 'get',
//        url: '/orders/InactiveOrders'
//    }
//       ).done(function (dataRcvd) {
//           $('#divInactiveOrders').empty();
//           $('#divInactiveOrders').html(dataRcvd);
//       });
//}

//var Routing = function (appRoot, contentSelector, defaultRoute) {

//    function getUrlFromHash(hash) {
//        var url = hash.replace('#/', '');
//        if (url === appRoot)
//            url = defaultRoute;
//        return url;
//    }

//    return {
//        init: function () {
//            Sammy(contentSelector, function () {
//                this.get(/\#\/(.*)/, function (context) {
//                    var url = getUrlFromHash(context.path);
//                    context.load(url).swap();
//                });
//            }).run('#/');
//        }
//    };
//}