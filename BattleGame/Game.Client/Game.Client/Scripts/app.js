/// <reference path="libs/_references.js" />

$(document).ready(function () {
    var dataPersist = dataPersister.get("http://localhost:40111/api/");
    var app = Sammy('#app', function () {

        this.get('#/', function () {
            if (!dataPersist.users.isLogin()) {
                httpRequester.getTemplate("publicMenu")
                  .then(function (html) {
                      $('#menu').html(html);
                  });
            }
            else {
                httpRequester.getTemplate("privateMenu")
                 .then(function (html) {
                     $('#menu').html(html);
                 });

                httpRequester.getTemplate("profilePreview")
                  .then(function (html) {
                      $('#profile').html(html);
                      var vm = vmFactory.getProfilePreviewVM();

                      ko.applyBindings(vm);
                  });
            }
        });

        this.get('#/register', function () {
            if (dataPersist.users.isLogin()) {
                this.redirect('#/');
            }
            else {
                httpRequester.getTemplate("publicMenu")
                     .then(function (html) {
                         $('#menu').html(html);
                     });

                var self = this;
                httpRequester.getTemplate("registerForm")
                   .then(function (html) {
                       $('#main-content').html(html);
                       var vm = vmFactory.getRegisterVM(function () {
                           $("#main-content").html("");
                           self.redirect('#/');
                       });

                       ko.applyBindings(vm);
                   });
            }
        });

        this.get('#/login', function () {
            if (dataPersist.users.isLogin()) {
                this.redirect('#/');
            }
            else {
                httpRequester.getTemplate("publicMenu")
                     .then(function (html) {
                         $('#menu').html(html);
                     });

                var self = this;
                httpRequester.getTemplate("loginForm")
                   .then(function (html) {
                       $('#main-content').html(html);
                       var vm = vmFactory.getLoginVM(function () {
                           $("#main-content").html("");
                           self.redirect('#/');
                       });

                       ko.applyBindings(vm);
                   });
            }
        });

        this.get('#/logout', function () {
            if (dataPersist.users.isLogin()) {
                dataPersist.users.logout();
            }
            else {
                httpRequester.getTemplate("publicMenu")
                     .then(function (html) {
                         $('#menu').html(html);
                     });
            }
        });
    });

    app.run('#/');
});