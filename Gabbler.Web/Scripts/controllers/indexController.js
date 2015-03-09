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
            //fonction global
            $rootScope.showGab = function (gabId) {
                $location.path("/gab/" + gabId);
            }
            $rootScope.showUser = function (userId) {
                $location.path("/Gab/User/" + userId);
            }


            $rootScope.authentication = authService.authentication;

        }
    ])
    .controller('SearchCtrl', [
        '$scope','$rootScope','$location', 'searchService', 
        function ($scope, $rootScope, $location, searchService) {

            
            $scope.searchVal = "";
            $scope.result = { NbResultUser: 0, NbResultGab: 0, ListOfUser: [], ListOfGab: [] };
            $scope.search = function () {
                if ($scope.searchVal && $scope.searchVal.length <= 0) { return; }
                searchService.getBasicSearch($scope.searchVal).then(function(res) {
                    $scope.result = res.data;
                });
            };

            $rootScope.$on('$stateChangeSuccess', function (event, toState) {
                $scope.searchVal = "";
            });
        }
    ]);