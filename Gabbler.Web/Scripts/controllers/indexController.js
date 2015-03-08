angular.module('app.indexController', [])

    // Path: /
    .controller('IndexCtrl', [
        '$scope','$rootScope', '$location', '$window', 'authService', 'userServices',
        function ($scope, $rootScope, $location, $window, authService, userServices) {
            if (authService.authentication.isAuth) {
                userServices.getActualUser().then(function (result) {
                    $rootScope.actualUser = result.data;
                });
            }

            $scope.logOut = function () {
                authService.logOut();
                $location.path('/login');
            }

            $rootScope.authentication = authService.authentication;

        }
    ])
    .controller('SearchCtrl', [
        '$scope', 'searchService', 
        function ($scope,searchService) {
            $scope.searchVal = "";
            $scope.result = { NbResultUser: 0, NbResultGab: 0, ListOfUser: [], ListOfGab: [] };
            $scope.search = function() {
                searchService.getBasicSearch($scope.searchVal).then(function(res) {
                    $scope.result = res.data;
                });
            };
        }
    ]);