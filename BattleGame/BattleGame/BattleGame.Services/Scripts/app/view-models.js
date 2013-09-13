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
                     $("#registerInformation").fadeOut(500);
                     $("#uploadAvatar").fadeIn(1000);
                     $("#uploadAvatar").submit(function () {
                         $("#uploadAvatar").fadeOut(1000);
                         $("#registerForm").prepend("Your registration complite!");
                         var a = $("<a/>");
                         a.attr("href", "http://localhost:40111/index.html#/profile");
                         a.text("Go to your Profile")
                         $("#registerForm").append(a);
                     });
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

                var image = data.avatar;

                var stratImageUrl = image.indexOf('upload/') + 7;
                var endImageUrl = image.lastIndexOf('/');
                var substring = image.substr(stratImageUrl, endImageUrl - stratImageUrl);
                var avatarUrl = image.replace(substring, "w_100,h_100");

                var viewModel = {
                    username: data.username,
                    nickname: data.nickname,
                    avatar: avatarUrl,
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
                            alert("Hero was created!");
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