/// <reference path="../libs/_references.js" />
window.dataPersister = (function () {
    var UsersPersister = Class.create({
        init: function (apiUrl) {
            this.apiUrl = apiUrl
        },
        login: function (username, password) {
            var user = {
                username: username,
                authcode: CryptoJS.SHA1(password).toString()
            };
            console.log(user);
            console.log(5);
            return httpRequester.postJSON(this.apiUrl + "/login", user)
            .then(function (data) {
                localStorage.setItem("sessionKey", data.sessionKey);
                localStorage.setItem("nickname", data.nickname);
            });
        },
        register: function (username, nickname, avatar, password) {
            var user = {
                username: username,
                nickname: nickname,
                avatar: avatar,
                authcode: CryptoJS.SHA1(password).toString()
            };

            return httpRequester.postJSON(this.apiUrl + "/register", user)
            .then(function (data) {
                localStorage.setItem("sessionKey", data.sessionKey);
                localStorage.setItem("nickname", data.nickname);
            });
        },
        logout: function () {
            var sessionKey = localStorage.getItem("sessionKey");
            if (sessionKey != "") {
                var headers = {
                    "X-sessionKey": sessionKey
                };
                console.log(this.apiUrl);
                return httpRequester.putJSON(this.apiUrl, null, headers).then(function () {
                    localStorage.clear();
                    console.log("clear");
                });
            }
            else {
                console.log("error");
            }

        },
        isLogin: function () {
            if (localStorage.getItem("sessionKey") == null) {
                return false;
            }
            else {
                return true;
            }
        },
        currentUser: function () {
            return localStorage.getItem("nickname");
        }
    });

    var MainPersister = Class.create({
        init: function (rootUrl) {
            this.rootUrl = rootUrl;
            this.users = new UsersPersister(rootUrl + "users");
        }
    });

    return {
        get: function (rootUrl) {
            return new MainPersister(rootUrl);
        }
    }
}());