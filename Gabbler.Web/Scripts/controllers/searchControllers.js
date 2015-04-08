'use strict';
function getGabIndex($scope, gabId) {
    for (var i = 0; i < $scope.gabs.Gabs.length; i++) {
        if ($scope.gabs.Gabs[i].Id == gabId) {
            return i;
        }
    }
    return -1;
}






// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.gabControllers', [])

    // Path: /
    .controller('SearchCtrl', [
        '$scope', '$location', '$window', '$stateProvider', 'searchService',
        function ($scope, $location, $window, $stateProvider, searchService) {

            $scope.search = {};

            searchService.getSearch($stateProvider.search, $stateProvider.numberOfUser, $stateProvider.numberOfGab).then(function (result) {
                
                $scope.search = result.data;

                for ($user in $scope.search.ListOfUser) {
                    $scope.userSearch += $user;
                }

                for ($gab in $scope.search.ListOfGab) {
                    $scope.gabSearch += $gab;
                }

                });


            }]);