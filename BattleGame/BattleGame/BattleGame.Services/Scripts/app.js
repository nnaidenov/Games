﻿/// <reference path="libs/_references.js" />

$(document).ready(function () {
    var dataPersist = dataPersister.get("api/");


    var app = Sammy('#app', function () {

        this.get('#/', function () {
            if (!dataPersist.users.isLogin()) {
                httpRequester.getTemplate("publicMenu")
                  .then(function (html) {
                      $('#menu').html(html);
                  });
            }
            else {
                privateLayout();
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
                           ko.removeNode(document.getElementById("registerForm"));
                           self.redirect('#/profile');
                       });

                       ko.applyBindings(vm, document.getElementById("registerForm"));
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
                             ko.removeNode(document.getElementById("loginForm"));
                             self.redirect('#/');
                         });

                         ko.applyBindings(vm, document.getElementById("loginForm"));
                     });
            }
        });

        this.get('#/logout', function () {
            var self = this;
            if (dataPersist.users.isLogin()) {
                dataPersist.users.logout()
                .then(function () {
                    $("#main-content").html("");
                    $("#profile").html("");
                    self.redirect('#/');
                });
            }
            else {
                httpRequester.getTemplate("publicMenu")
                     .then(function (html) {
                         $('#menu').html(html);
                     });
            }
        });

        this.get('#/profile', function () {
            if (!dataPersist.users.isLogin()) {
                this.redirect('#/');
            }
            else {
                httpRequester.getTemplate("privateMenu")
                     .then(function (html) {
                         $('#menu').html(html);
                     });
                httpRequester.getTemplate("profilePreview")
                          .then(function (html) {
                              $('#profile').html(html);
                              var vm = vmFactory.getProfilePreviewVM(function () {
                                  self.redirect('#/profile');
                              });
                              ko.cleanNode(document.getElementById("profilePreview"));
                              ko.applyBindings(vm, document.getElementById("profilePreview"));
                          });

                var self = this;
                httpRequester.getTemplate("profile")
                     .then(function (html) {
                         $('#main-content').html(html);

                         vmFactory.getProfileVM().
                         then(function (data) {
                             ko.applyBindings(data, document.getElementById("profileDetails"));
                         });
                     });
            }
        });

        this.get('#/createHero', function () {
            if (!dataPersist.users.isLogin()) {
                this.redirect('#/');
            }
            else {
                privateLayout();

                viewsFactory.getCreateHeroeView()
                      .then(function (html) {
                          $('#main-content').html(html);

                          vmFactory.getCreateHeroeVM().
                          then(function (data) {
                              ko.applyBindings(data, document.getElementById("createHeroe"));
                          });
                      });
            }
        });

        this.get('#/manageHeroes', function () {
            if (!dataPersist.users.isLogin()) {
                this.redirect('#/');
            }
            else {
                var self = this;
                privateLayout();

                httpRequester.getTemplate("profile")
                     .then(function (html) {
                         $('#main-content').html(html);

                         vmFactory.getProfileVM().
                         then(function (data) {
                             ko.applyBindings(data, document.getElementById("profileDetails"));
                         });
                     });
            }
        });
    });

    app.run('#/');

    function privateLayout() {
        httpRequester.getTemplate("privateMenu")
             .then(function (html) {
                 $('#menu').html(html);
             });
        httpRequester.getTemplate("profilePreview")
            .then(function (html) {
                $('#profile').html(html);
                var vm = vmFactory.getProfilePreviewVM(function () {
                    window.location.assign("#/profile")
                });
                ko.cleanNode(document.getElementById("profilePreview"));
                ko.applyBindings(vm, document.getElementById("profilePreview"));
            });
    }
});