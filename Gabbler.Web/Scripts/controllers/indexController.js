angular.module('app.indexController', [])

    // Path: /
    .controller('IndexCtrl', [
        '$scope','$rootScope', '$location', '$window', 'authService', 'userServices',
        function ($scope, $rootScope, $location, $window, authService, userServices) {
                if (authService.authentication.isAuth) {
                userServices.getActualUser().then(function (result) {
                    $rootScope.actualUser = result.data;
                    $rootScope.actualUser.isAuth = true;
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
        '$scope','$rootScope','$location', 'searchService', 'userServices',
        function ($scope, $rootScope, $location, searchService, userServices) {
            
            
            $scope.searchVal = "";
            $scope.result = { NbResultUser: 0, NbResultGab: 0, ListOfUser: [], ListOfGab: [] };
            $scope.isFollowing = [];
            $scope.search = function () {
                if ($scope.searchVal && $scope.searchVal.length <= 0) { return; }
                searchService.getBasicSearch($scope.searchVal).then(function(res) {
                    $scope.result = res.data;

                    //récupération des Id a vérifier
                    var arrayId = [];
                    for (var i = 0; i < $scope.result.ListOfUser.length; i++) {
                        arrayId.push($scope.result.ListOfUser[i].Id);
                    }
                    if (arrayId.length > 0 && $rootScope.authentication.isAuth) {
                        userServices.isFollowing(arrayId).then(function(result) {
                            $scope.isFollowing = result.data;
                        });
                    }
                });
            };

            $scope.close = function () {
                $scope.searchVal = "";
            }

            $scope.follow = function(userId) {
                userServices.follow(userId).success(function(result) {
                    $scope.isFollowing.push(userId);
                }).error(function (error) {
                    $scope.searchVal = "";
                    alert(error.Message);
                });
            }

            $scope.unfollow = function (userId) {
                userServices.unFollow(userId).success(function (result) {

                    $scope.isFollowing = removeValueFromArray(userId, $scope.isFollowing);
                }).error(function (error) {
                    $scope.searchVal = "";
                    alert(error.Message);
                });
            }

            $rootScope.$on('$stateChangeSuccess', function (event, toState) {
                $scope.searchVal = "";
            });
        }
    ]);