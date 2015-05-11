angular.module('app.indexController', [])

    // Path: /
    .controller('IndexCtrl', [
        '$scope', '$rootScope', '$location', '$window', 'authService', 'userServices',
        function($scope, $rootScope, $location, $window, authService, userServices) {
            if (authService.authentication.isAuth) {
                userServices.getActualUser().then(function(result) {
                    $rootScope.actualUser = result.data;
                    $rootScope.actualUser.isAuth = true;
                });
            }

            $scope.logOut = function() {
                authService.logOut();
                $location.path('/login');
            }
            //fonction global
            $rootScope.showGab = function(gabId) {
                $location.path("/gab/" + gabId);
            }
            $rootScope.showUser = function(userId) {
                $location.path("/Gab/User/" + userId);
            }


            $rootScope.authentication = authService.authentication;

        }
    ]);
    