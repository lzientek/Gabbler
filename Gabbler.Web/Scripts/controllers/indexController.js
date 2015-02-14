angular.module('app.indexController', [])

    // Path: /
    .controller('IndexCtrl', [
        '$scope', '$location', '$window', 'authService','userServices',
        function ($scope, $location, $window, authService, userServices) {

            userServices.getActualUser().then(function(result) {
                $scope.actualUser = result.data;
            });


            $scope.logOut = function() {
                authService.logOut();
                $location.path('/home');
            }
            
            
            $scope.authentication = authService.authentication;

            
        }
    ]);