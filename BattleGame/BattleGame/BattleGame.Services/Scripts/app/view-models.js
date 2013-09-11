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
                var viewModel = {
                    username: data.username,
                    nickname: data.nickname,
                    avatar: data.avatar
                }

                return viewModel;
            });
    }


    return {
        getLoginVM: getLoginViewModel,
        getRegisterVM: getRegisterViewModel,
        getProfilePreviewVM: getProfilePreviewViewModel,
        getProfileVM: getProfileViewModel
    };
}());