
// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.searchController', [])

    // Path: /
    .controller('SearchCtrl', [
        '$scope', '$location', '$window', '$stateProvider', 'searchService',
        function ($scope, $location, $window, $stateProvider, searchService) {

            $scope.search = {};

            searchService.getSearch($stateProvider.search, $stateProvider.numberOfUser, $stateProvider.numberOfGab).then(function (result) {
                
                $scope.search = result.data;
                $scope.userSearch = $scope.search.ListOfUser;
                $scope.gabSearch = $scope.search.ListOfGab;

                });


            }]);