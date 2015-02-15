'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.gabControllers', [])

    // Path: /
    .controller('HomeCtrl', [
        '$scope', '$location', '$window', '$rootScope', 'gabService',
        function ($scope, $location, $window, $rootScope, gabService) {

            //get gabs
            gabService.getAllGabs().then(function (result) {
                $scope.gabs = result.data;

                //add isLiked
                for (var i = 0; i < $scope.gabs.Gabs.length; i++) {
                    $scope.gabs.Gabs[i].isLiked = $scope.gabs.Gabs[i].Likes.indexOf($rootScope.authentication.userName) != -1;
                    $scope.gabs.Gabs[i].showComment = false;
                }
                $scope.gabs.isAllShow = result.data.NbOfShownGabs >= result.data.TotalGabs;
                $('[data-toggle="tooltip"]').tooltip();

            });

            //get more gabs
            $scope.getMoreGabs = function () {
                $scope.gabs.isAllShow = true;
                gabService.getMoreGabs($scope.gabs.NbOfShownGabs).then(function (result) {
                    //add gabs to the list
                    $scope.gabs.StartGab = result.data.StartGab;
                    $scope.gabs.NbOfShownGabs += result.data.NbOfShownGabs;

                    //add the isLiked
                    for (var i = 0; i < result.data.Gabs.length; i++) {
                        result.data.Gabs[i].isLiked = result.data.Gabs[i].Likes.indexOf($rootScope.authentication.userName) != -1;
                        result.data.Gabs[i].showComment = false;
                    }

                    Array.prototype.push.apply($scope.gabs.Gabs, result.data.Gabs);
                    $scope.gabs.isAllShow = $scope.gabs.NbOfShownGabs >= result.data.TotalGabs;

                });
            }

            // like a gab
            $scope.likeGab = function (gab) {
                gabService.likeGab(gab.Id).success(function (result) {
                    for (var i = 0; i < $scope.gabs.Gabs.length; i++) {
                        if ($scope.gabs.Gabs[i].Id == gab.Id) {
                            $scope.gabs.Gabs[i].NbOfLikes += 1;
                            $scope.gabs.Gabs[i].isLiked = true;
                            break;
                        }
                    }

                }).error(function (error) {
                    alert(error.Message);
                });
            }

            //unlike a gab
            $scope.unLikeGab = function (gab) {
                gabService.unLikeGab(gab.Id).success(function (result) {
                    for (var i = 0; i < $scope.gabs.Gabs.length; i++) {
                        if ($scope.gabs.Gabs[i].Id == gab.Id) {
                            $scope.gabs.Gabs[i].NbOfLikes -= 1;
                            $scope.gabs.Gabs[i].isLiked = false;
                            break;
                        }
                    }

                }).error(function (error) {
                    alert(error.Message);
                });
            }

            //load comments
            $scope.getComments = function (gabId) {
                var i = 0;
                for (i = 0; i < $scope.gabs.Gabs.length; i++) {
                    if ($scope.gabs.Gabs[i].Id == gabId) {
                        $scope.gabs.Gabs[i].showComment = true;
                        break;
                    }
                }
                gabService.getGabComments(gabId).then(function (result) {
                    //get gab
                    result.data.Comments = result.data.Comments.reverse();
                    $scope.gabs.Gabs[i].comments = result.data;
                });
            }


            //page vars
            $scope.$root.title = 'Welcome on gabbler!';
            $scope.$on('$viewContentLoaded', function () {
                $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
            });
        }
    ])


    .controller('CommentCtrl', [
        '$scope', '$location', '$window', '$rootScope', 'gabService',
        function ($scope, $location, $window, $rootScope, gabService) {

            //load more comments
            $scope.getMoreComments = function (gabId) {
                var i;
                for (i = 0; i < $scope.gabs.Gabs.length; i++) {
                    if ($scope.gabs.Gabs[i].Id == gabId) {
                        $scope.gabs.Gabs[i].showComment = true;
                        break;
                    }
                }
                gabService.getGabMoreComments(gabId, $scope.gabs.Gabs[i].comments.NbOfShownComments)
                    .then(function (result) {
                        //get gab
                        $scope.gabs.Gabs[i].comments.NbOfShownComments += result.data.NbOfShownComments;
                        var tab = $scope.gabs.Gabs[i].comments.Comments.reverse();
                        Array.prototype.push.apply(tab, result.data.Comments);
                        $scope.gabs.Gabs[i].comments.Comments = tab.reverse();
                    });
            }


            //ajout des commentaires
            $scope.newComment = { Message: "" };
            $scope.addComment = function (gabId) {
                gabService.addComment(gabId, $scope.newComment).success(function (result) {
                    for (var i = 0; i < $scope.gabs.Gabs.length; i++) {
                        if ($scope.gabs.Gabs[i].Id == gabId) {
                            $scope.gabs.Gabs[i].showComment = true;
                            $scope.gabs.Gabs[i].NbOfComments++;

                            $scope.gabs.Gabs[i].comments.Comments.push(result);

                            break;
                        }
                    }
                }).error(function (error) {
                    alert(error.Message);
                });
            }

        }]);