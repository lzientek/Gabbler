'use strict';

angular.module('app.userControllers', [])

// Path: /login
    .controller('LoginCtrl', [
        '$scope', '$location', '$window', 'authService',
        function ($scope, $location, $window,authService) {

            $scope.$root.title = 'Gabbler - Sign In';
            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.message = "";


            $scope.login = function () {
                authService.login($scope.loginData).then(function (response) {
                    // TODO: right path
                    $location.path('/');

                },
                 function (err) {
                     $scope.message = err.error_description;
                 });
            };
            $scope.$on('$viewContentLoaded', function () {
                $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
            });
        }
    ])
.controller('RegisterCtrl', [
    '$scope', '$location', '$timeout', 'authService',
    function ($scope, $location, $timeout, authService) {

        $scope.$root.title = 'Gabbler - Register';

        $scope.savedSuccessfully = false;
        $scope.message = "";

        $scope.registration = {
            Mail: "",
            Password: "",
            Pseudo: "",
            FirstName: "",
            LastName: ""
        };

        $scope.signUp = function () {

            authService.saveRegistration($scope.registration).then(function (response) {

                $scope.savedSuccessfully = true;
                $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
                startTimer();

            },
             function (response) {
                 var errors = [];
                 for (var key in response.data.ModelState) {
                     for (var i = 0; i < response.data.ModelState[key].length; i++) {
                         errors.push(response.data.ModelState[key][i]);
                     }
                 }
                 $scope.message = "Failed to register user due to:" + errors.join(' ');
             });
        };

        var startTimer = function () {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path('/login');
            }, 2000);
        }

    }]);
