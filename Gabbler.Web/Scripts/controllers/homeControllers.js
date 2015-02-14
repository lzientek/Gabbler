'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.homeControllers', [])

    // Path: /
    .controller('HomeCtrl', ['$scope', '$location', '$window', 'gabService',
        function ($scope, $location, $window, gabService) {


            gabService.getAllGabs().then(function(result) {
                $scope.gabs = result.data;
                $scope.gabs.isAllShow = result.data.NbOfShownGabs >= result.data.TotalGabs;
            });

            $scope.getMoreGabs = function() {

                gabService.getMoreGabs($scope.gabs.NbOfShownGabs).then(function (result) {
                    $scope.gabs.StartGab = result.data.StartGab;
                    $scope.gabs.NbOfShownGabs += result.data.NbOfShownGabs;
                    Array.prototype.push.apply($scope.gabs.Gabs, result.data.Gabs);
                    $scope.gabs.isAllShow = $scope.gabs.NbOfShownGabs >= result.data.TotalGabs;
                });
            }

            $scope.$root.title = 'Welcome on gabbler!';
            $scope.$on('$viewContentLoaded', function () {
                $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
            });
        }])

    // Path: /about
    .controller('AboutCtrl', ['$scope', '$location', '$window', function ($scope, $location, $window) {
        $scope.$root.title = 'AngularJS SPA | About';
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }])



    // Path: /error/404
    .controller('Error404Ctrl', ['$scope', '$location', '$window', function ($scope, $location, $window) {
        $scope.$root.title = 'Error 404: Page Not Found';
        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }]);