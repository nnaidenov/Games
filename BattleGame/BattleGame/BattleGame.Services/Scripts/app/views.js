/// <reference path="../libs/_references.js" />
window.viewsFactory = (function () {
    var templates = {};

    function getLoginView() {
        return httpRequester.getTemplate("loginForm")
            .then(function (data) {
                console.log("new");
                return data;
            });
    }

    function getRegisterView() {
        return httpRequester.getTemplate("registerForm")
            .then(function (data) {
                return data;
            });

    }

    function getProfilePreviewView() {
        return httpRequester.getTemplate("profilePreview")
            .then(function (data) {
                return data;
            });
    }

    function getProfileView() {

        return httpRequester.getTemplate("profile")
            .then(function (data) {
                return data;
            });

    }

    function getCreateHeroeView() {
        return httpRequester.getTemplate("createHeroe")
            .then(function (data) {
                return data;
            });
    }

    return {
        getLoginView: getLoginView,
        getRegisterView: getRegisterView,
        getProfilePreviewView: getProfilePreviewView,
        getProfileView: getProfileView,
        getCreateHeroeView: getCreateHeroeView
    }
}());