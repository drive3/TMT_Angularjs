﻿angular.module("ui-notification").factory("Notification", ["$timeout", "uiNotificationTemplates", "$http", "$compile", "$templateCache", "$rootScope", "$injector", "$sce", function (t, e, i, n, a, o, s, c) {
    var r = 10,
        l = 10,
        u = 10,
        p = 10,
        d = 5e3,
        f = [],
        m = function (s, m) {
            "object" != typeof s && (s = {
                message: s
            }), s.template = s.template ? s.template : e, s.delay = angular.isUndefined(s.delay) ? d : s.delay, s.type = m ? m : "", i.get(s.template, {
                cache: a
            }).success(function (e) {
                var i = o.$new();
                if (i.message = c.trustAsHtml(s.message), i.title = c.trustAsHtml(s.title), i.t = s.type.substr(0, 1), i.delay = s.delay, "object" == typeof s.scope)
                    for (var a in s.scope) i[a] = s.scope[a];
                var d = function () {
                    for (var t = 0, e = 0, i = r, n = f.length - 1; n >= 0; n--) {
                        var a = f[n],
                            o = parseInt(a[0].offsetHeight),
                            s = parseInt(a[0].offsetWidth);
                        c + o > window.innerHeight && (i = r, e++ , t = 0);
                        var c = i + (0 === t ? 0 : u),
                            d = l + e * (p + s);
                        a.css("top", c + "px"), a.css("right", d + "px"), i = c + o, t++
                    }
                },
                    m = n(e)(i);
                m.addClass(s.type), m.bind("webkitTransitionEnd oTransitionEnd otransitionend transitionend msTransitionEnd click", function (t) {
                    ("click" === t.type || "opacity" === t.propertyName && t.elapsedTime >= 1) && (m.remove(), f.splice(f.indexOf(m), 1), d())
                }), t(function () {
                    m.addClass("killed")
                }, s.delay), angular.element(document.getElementsByTagName("body")).append(m), f.push(m), t(d)
            }).error(function (t) {
                throw new Error("Template (" + s.template + ") could not be loaded. " + t)
            })
        };
    return m.config = function (t) {
        r = t.top ? t.top : r, u = t.verticalSpacing ? t.verticalSpacing : u
    }, m.primary = function () {
        this(args, "")
    }, m.error = function (t) {
        this(t, "error")
    }, m.success = function (t) {
        this(t, "success")
    }, m.info = function (t) {
        this(t, "info")
    }, m.warning = function (t) {
        this(t, "warning")
    }, m
}]), angular.module("ui-notification").run(["$templateCache", function (t) {
    t.put("angular-ui-notification.html", '<div class="ui-notification"><h3 ng-show="title" ng-bind-html="title"></h3><div class="message" ng-bind-html="message"></div></div>')
}]);
