/// <reference path="../libs/_references.js" />
window.dataPersister = (function () {
    var UsersPersister = Class.create({
        init: function (apiUrl) {
            this.apiUrl = apiUrl
        },
        login: function (username, password) {
            var user = {
                username: username,
                authCode: CryptoJS.SHA1(password).toString()
            };
            console.log(user);
            console.log(5);
            return httpRequester.postJSON(this.apiUrl + "/login", user)
            .then(function (data) {
                localStorage.setItem("sessionKey", data.sessionKey);
                localStorage.setItem("nickname", data.nickname);
            });
        },
        register: function (username, nickname, password, avatar) {
            var user = {
                username: username,
                nickname: nickname,
                avatar: avatar,
                authCode: CryptoJS.SHA1(password).toString()
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
                console.log(headers);
                return httpRequester.putJSON(this.apiUrl + "/logout", null, headers).then(function () {
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
        },
        detailInformation: function () {
            var sessionKey = localStorage.getItem("sessionKey");
            var headers = {
                "X-sessionKey": sessionKey
            };
            console.log(5);

            return httpRequester.getJSON(this.apiUrl + "/details", headers);
        }
    });

    var RacePersister = Class.create({
        init: function (apiUrl) {
            this.apiUrl = apiUrl
        },
        getAll: function () {
            var sessionKey = localStorage.getItem("sessionKey");
            var headers = {
                "X-sessionKey": sessionKey
            };

            return httpRequester.getJSON(this.apiUrl + "/all", headers);
        }
    });

    var HeroePersister = Class.create({
        init: function (apiUrl) {
            this.apiUrl = apiUrl
        },
        create: function (name, race) {
            var sessionKey = localStorage.getItem("sessionKey");
            var headers = {
                "X-sessionKey": sessionKey
            };

            var heroe = {
                name: name,
                race: race
            }

            return httpRequester.postJSON(this.apiUrl + "/create", heroe, headers);
        }
    });

    var MainPersister = Class.create({
        init: function (rootUrl) {
            this.rootUrl = rootUrl;
            this.users = new UsersPersister(rootUrl + "users");
            this.races = new RacePersister(rootUrl + "races");
            this.heroes = new HeroePersister(rootUrl + "heroes");
        }
    });

    return {
        get: function (rootUrl) {
            return new MainPersister(rootUrl);
        }
    }
}());