/// <reference path="../libs/_references.js" />
window.viewsFactory = (function () {
    var templates = {};

    function getLoginView() {
        if (templates["loginForm"]) {
            var defer = Q.defer();
            defer.resolve(templates["loginForm"]);
            return defer.promise;
        }
        else {
            return httpRequester.getTemplate("loginForm")
                .then(function (data) {
                    return templates["loginForm"] = data;
                });
        }
    }

    function getRegisterView() {
        if (templates["registerForm"]) {
            var defer = Q.defer();
            defer.resolve(templates["registerForm"]);
            return defer.promise;
        }
        else {
            return httpRequester.getTemplate("registerForm")
                .then(function (data) {
                    templates["registerForm"] = data;
                    return data;
                });
        }
    }

    function getProfilePreviewView() {
        if (templates["profilePreview"]) {
            var defer = Q.defer();
            defer.resolve(templates["profilePreview"]);
            return defer.promise;
        }
        else {
            return httpRequester.getTemplate("profilePreview")
                .then(function (data) {
                    templates["profilePreview"] = data;
                    return data;
                });
        }
    }

    return {
        getLoginView: getLoginView,
        getRegisterView: getRegisterView,
        getProfilePreviewView: getProfilePreviewView
    }
}());