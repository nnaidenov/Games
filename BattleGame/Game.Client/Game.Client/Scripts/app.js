/// <reference path="libs/_references.js" />

$(document).ready(function () {
    var dataPersist = dataPersister.get("http://localhost:19261/api/");
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
                       $('#game').html(html);
                       var vm = vmFactory.getRegisterVM(function () {
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
                       $('#game').html(html);
                       var vm = vmFactory.getLoginVM(function () {
                           self.redirect('#/');
                       });

                       ko.applyBindings(vm);
                   });
            }
        });
    });

    app.run('#/');
});