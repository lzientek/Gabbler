'use strict';

angular.module('app.userControllers', [])

// Path: /login
    .controller('LoginCtrl', [
        '$scope', '$rootScope', '$location', '$window', 'authService', 'userServices',
        function ($scope, $rootScope, $location, $window, authService, userServices) {

            $scope.$root.title = 'Gabbler - Sign In';
            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.message = "";


            $scope.login = function () {
                authService.login($scope.loginData).then(function (response) {
                    $location.path('/TimeLine');
                    userServices.getActualUser().then(function (result) {
                        $rootScope.actualUser = result.data;
                    });
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

        }])

    .controller('UserEditCtrl', [
        '$scope', '$rootScope', '$stateParams', 'authService', 'userServices',
        function ($scope, $rootScope, $stateParams, authService, userServices) {

            var id = $stateParams.userId;

            //get user
            userServices.getUserById(id).then(function (result) {
                $scope.$root.title = result.data.Pseudo + ' - Gabbler';
                $scope.user = result.data;
                $('body').css("background-image", "url(" + serviceBase + $scope.user.BackgroundImagePath + ")");
            });

            $scope.edit = function() {
                userServices.editUser($scope.user).success(function(result) {
                    $scope.user = result;
                    alert("saved");
                }).error(function(error) {
                    alert(error.Message,'danger');
                });
            }

        }])

    //profile image upload
    .controller('UploadProfilCtrl', ['$scope', '$upload','$rootScope',
        function ($scope, $upload,$rootScope) {
            $scope.$watch('files', function () {
                $scope.upload($scope.files);
            });

            $scope.upload = function (files) {
                if (files && files.length) {
                    $('#progressbarProfil').removeClass("progress-bar-danger");
                    $('#progressbarProfil').addClass("progress-bar-success");
                    var file = files[0];
                    $upload.upload({
                        url: serviceBase + 'Account/Me/ProfileImage',
                        file: file,
                        fileFormDataName: 'ProfileImg'
                    }).progress(function (evt) {//progression
                        var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                        $('#progressbarProfil').css("width", progressPercentage + "%");
                        $scope.profilPourcent = progressPercentage;
                    }).success(function (data, status, headers, config) { //success
                        $('#progressbarProfil').text("success");
                        $rootScope.actualUser.ProfilImagePath = data.path;
                        $scope.user.ProfilImagePath = data.path;

                    }).error(function (error) { //error
                        $('#progressbarProfil').text(error.Message);
                        $('#progressbarProfil').removeClass("progress-bar-success");
                        $('#progressbarProfil').addClass("progress-bar-danger");
                    });
                }
            };
        }])

    //background upload
    .controller('UploadBackgroundCtrl', ['$scope', '$upload','$rootScope',
        function ($scope, $upload,$rootScope) {
            $scope.$watch('files', function () {
                $scope.upload($scope.files);
            });

            $scope.upload = function (files) {
                if (files && files.length) {
                    $('#progressbarBack').removeClass("progress-bar-danger");
                    $('#progressbarBack').addClass("progress-bar-success");
                    var file = files[0];
                    $upload.upload({
                        url: serviceBase + 'Account/Me/Background',
                        file: file,
                        fileFormDataName: 'Background'
                    }).progress(function (evt) { //progression
                        var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                        $('#progressbarBack').css("width", progressPercentage + "%");
                        $scope.backPourcent = progressPercentage;
                    }).success(function (data, status, headers, config) { //success
                        $('#progressbarBack').text("success");
                        $rootScope.actualUser.BackgroundImagePath = data.path;
                        $scope.user.BackgroundImagePath = data.path;
                        $('body').css("background-image", "url(" + serviceBase + $scope.user.BackgroundImagePath + ")");

                    }).error(function(error) { //error
                        $('#progressbarBack').text(error.Message);
                        $('#progressbarBack').removeClass("progress-bar-success");
                        $('#progressbarBack').addClass("progress-bar-danger");
                    });
                }
            };
        }]);
