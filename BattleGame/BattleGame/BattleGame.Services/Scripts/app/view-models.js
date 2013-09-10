/// <reference path="../libs/_references.js" />

window.vmFactory = (function () {
    var dataPersist = dataPersister.get("api/");

    function getRegisterViewModel(callBack) {
        var viewModel = {
            username: ko.observable(""),
            nickname: ko.observable(""),
            password: ko.observable(""),
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

        dataPersist.users.detailInformation()
            .then(function (data) {
                var viewModel = {
                    details: data
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