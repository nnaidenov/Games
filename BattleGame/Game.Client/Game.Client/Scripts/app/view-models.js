/// <reference path="../libs/_references.js" />

window.vmFactory = (function () {
    var dataPersist = dataPersister.get("http://localhost:19261/api/");

    function getRegisterViewModel(callBack) {
        var viewModel = {
            username: ko.observable("a"),
            nickname: ko.observable("a"),
            password: ko.observable("a"),
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
            username: ko.observable(""),
            password: ko.observable(""),
            login: function () {
                console.log(this.username());
                console.log(this.password());
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

    function getProfilePreviewViewModel() {
        dataPersist.users.currentUser()
            .then(function (data) {
                console.log(data);
                var viewModel = {
                    nickname: ko.observable(data)
                }

                return viewModel;
            });
    }

    return {
        getLoginVM: getLoginViewModel,
        getRegisterVM: getRegisterViewModel,
        getProfilePreviewVM: getProfilePreviewViewModel
    };
}());