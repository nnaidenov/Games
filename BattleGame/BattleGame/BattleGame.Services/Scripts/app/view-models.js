/// <reference path="../libs/_references.js" />

window.vmFactory = (function () {
    var dataPersist = dataPersister.get("api/");

    function getRegisterViewModel(callBack) {
        var viewModel = {
            username: ko.observable("a"),
            nickname: ko.observable("b"),
            password: ko.observable("c"),
            register: function () {
                dataPersist.users.register(this.username(), this.nickname(), this.password())
                 .then(function () {
                     callBack();
                 }, function (err) {
                     console.log(err);
                 })
            }
        }

        return viewModel;
    }

    function getLoginViewModel(callBack) {
        var viewModel = {
            username: ko.observable("a"),
            password: ko.observable("b"),
            login: function () {
                dataPersist.users.login(this.username(), this.password())
                 .then(function () {
                     callBack();
                 }, function (err) {
                     console.log(err);
                 })
            }
        }

        return viewModel;
    }

    function getProfilePreviewViewModel(callBack) {
        var viewModel = {
            nickname: dataPersist.users.currentUser(),
            goToProfile: function () {
                callBack();
            }
        }

        return viewModel;
    }

    function getProfileViewModel() {
        return dataPersist.users.detailInformation()
            .then(function (data) {
                if (data.heroes.length == 0) {
                    data.heroes.push({ name: "No heroes" });
                }
                var viewModel = {
                    username: data.username,
                    nickname: data.nickname,
                    avatar: "ds",
                    heroes: data.heroes
                }

                return viewModel;
            });
    }

    function getCreateHeroeViewModel() {
        return dataPersist.races.getAll()
            .then(function (data) {
                var viewModel = {
                    races: data,
                    heroeName: ko.observable(),
                    selectedRace: ko.observable(),
                    create: function () {
                        dataPersist.heroes.create(this.heroeName(), this.selectedRace())
                        .then(function () {
                            console.log("created");
                        });
                    }
                }

                return viewModel;
            });
    }

    return {
        getLoginVM: getLoginViewModel,
        getRegisterVM: getRegisterViewModel,
        getProfilePreviewVM: getProfilePreviewViewModel,
        getProfileVM: getProfileViewModel,
        getCreateHeroeVM: getCreateHeroeViewModel
    };
}());