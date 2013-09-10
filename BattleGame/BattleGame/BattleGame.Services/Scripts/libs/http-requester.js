/// <reference path="_references.js" />

window.httpRequester = (function () {
    var postRequest = function (url, data, headers) {
        var deferred = Q.defer();

        var urlReq = url;
        var dataReq = JSON.stringify(data);

        $.ajax({
            url: urlReq,
            type: "POST",
            headers: headers,
            contentType: "application/json",
            data: dataReq || "",
            success: function (data) {
                deferred.resolve(data);
            },
            error: function (errorM) {
                deferred.reject(errorM);
            }
        });

        return deferred.promise;
    };

    var putRequest = function (url, data, headers) {
        var deferred = Q.defer();

        var urlReq = url;
        var dataReq = JSON.stringify(data);

        $.ajax({
            url: urlReq,
            type: "PUT",
            headers: headers,
            contentType: "application/json",
            data: dataReq || "",
            success: function (data) {
                deferred.resolve(data);
            },
            error: function (errorM) {
                deferred.reject(errorM);
            }
        });

        return deferred.promise;
    };

    var getRequest = function (url, headers) {
        var deferred = Q.defer();

        var urlReq = url;

        $.ajax({
            url: urlReq,
            type: "GET",
            headers: headers,
            contentType: "application/json",
            success: function (data) {
                deferred.resolve(data);
            },
            error: function (errorM) {
                deferred.reject(errorM);
            }
        });

        return deferred.promise;
    };


    var getTemplate = function (name) {
        var deferred = Q.defer();

        $.ajax({
            url: "Scripts/partials/" + name + ".html",
            type: "GET",
            success: function (data) {
                deferred.resolve(data);
            },
            error: function (errorM) {
                deferred.reject(errorM);
            }
        });

        return deferred.promise;
    };

    return {
        postJSON: postRequest,
        getJSON: getRequest,
        putJSON: putRequest,
        getTemplate: getTemplate
    }
}());