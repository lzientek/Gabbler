angular.module('app.indexController', [])

    // Path: /
    .controller('IndexCtrl', [
        '$scope','$rootScope', '$location', '$window', 'authService', 'userServices',
        function ($scope, $rootScope, $location, $window, authService, userServices) {
            if (authService.authentication.isAuth) {
                userServices.getActualUser().then(function (result) {
                    $scope.actualUser = result.data;
                });
            }


            $scope.logOut = function () {
                authService.logOut();
                $location.path('/login');
            }


            $rootScope.authentication = authService.authentication;


        }
    ]);