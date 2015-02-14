'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.homeControllers', [])

    // Path: /
    .controller('HomeCtrl', ['$scope', '$location', '$window','$rootScope', 'gabService',
        function ($scope, $location, $window,$rootScope, gabService) {


            gabService.getAllGabs().then(function(result) {
                $scope.gabs = result.data;

                //add isLiked
                for (var i = 0; i < $scope.gabs.Gabs.length; i++) {
                    $scope.gabs.Gabs[i].isLiked = $scope.gabs.Gabs[i].Likes.indexOf($rootScope.authentication.userName) != -1;
                }
                $scope.gabs.isAllShow = result.data.NbOfShownGabs >= result.data.TotalGabs;
                $('[data-toggle="tooltip"]').tooltip();

            });

            $scope.getMoreGabs = function() {
                $scope.gabs.isAllShow = true;
                gabService.getMoreGabs($scope.gabs.NbOfShownGabs).then(function (result) {
                    //add gabs to the list
                    $scope.gabs.StartGab = result.data.StartGab;
                    $scope.gabs.NbOfShownGabs += result.data.NbOfShownGabs;

                    //add the isLiked
                    for (var i = 0; i < result.data.Gabs.length; i++) {
                        result.data.Gabs[i].isLiked = result.data.Gabs[i].Likes.indexOf($rootScope.authentication.userName) != -1;
                    }

                    Array.prototype.push.apply($scope.gabs.Gabs, result.data.Gabs);
                    $scope.gabs.isAllShow = $scope.gabs.NbOfShownGabs >= result.data.TotalGabs;

                });
            }

            $scope.likeGab = function (gab) {
                gabService.likeGab(gab.Id).success(function (result) {
                    for (var i=0;i < $scope.gabs.Gabs.length;i++) {
                        if ($scope.gabs.Gabs[i].Id == gab.Id) {
                            $scope.gabs.Gabs[i].NbOfLikes += 1;
                            $scope.gabs.Gabs[i].isLiked = true;
                            break;
                        }
                    }

                }).error(function(error) {
                    alert(error.Message);
                });
            }
            $scope.unLikeGab = function (gab) {
                gabService.unLikeGab(gab.Id).success(function (result) {
                    for (var i=0;i < $scope.gabs.Gabs.length;i++) {
                        if ($scope.gabs.Gabs[i].Id == gab.Id) {
                            $scope.gabs.Gabs[i].NbOfLikes -= 1;
                            $scope.gabs.Gabs[i].isLiked = false;
                            break;
                        }
                    }

                }).error(function(error) {
                    alert(error.Message);
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