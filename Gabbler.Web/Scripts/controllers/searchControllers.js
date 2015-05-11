
// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.searchControllers', [])
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
    ])
    // Path: /
    .controller('BigSearchCtrl', [
        '$scope', '$stateParams', 'searchService',
        function ($scope, $stateProvider, searchService) {
            $scope.search = { NbMaxUser: 0, NbMaxGab: 0, NbResultUser: 0, NbResultGab: 0, ListOfUser: [], ListOfGab: [] };
            $scope.userSearch = [];
            $scope.gabSearch = [];
            $scope.maxUser = 0;
            $scope.maxGab = 0;

            searchService.getSearch($stateProvider.search, $stateProvider.numberUser, $stateProvider.numberGab).then(function (result) {
                console.log(result.data);
                $scope.search = result.data;
                $scope.userSearch = $scope.search.ListOfUser;
                $scope.gabSearch = $scope.search.ListOfGab;
                $scope.maxUser = $scope.search.NbMaxUser;
                $scope.maxGab = $scope.search.NbMaxGab;
                $scope.searchValue = $stateProvider.search;
            });


            }]);