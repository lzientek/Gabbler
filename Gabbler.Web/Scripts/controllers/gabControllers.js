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

            $scope.editGab = function (gabId) {
                //var i = getGabIndex($scope, gabId);
                //todo: insérer le formulaire 
                //ou une popup
            }

            $scope.newGab = { Content: "" };
            $scope.addGab = function () {
                var content = $scope.newGab;
                $scope.newGab.Content = ""; //reset the field value

                gabService.addGab(content)
                    .success(function (result) {
                        for (var i = $scope.gabs.Gabs.length - 1; i >= 0; i--) {
                            $scope.gabs.Gabs[i + 1] = $scope.gabs.Gabs[i];
                        }
                        $scope.gabs.Gabs[0] = result;
                    }).error(function (error) {
                        $scope.newGab.newGab = content;
                        alert(error.Message);
                    });
            }
            // like a gab
            $scope.likeGab = function (gab) {
                gabService.likeGab(gab.Id).success(function (result) {
                    var i = getGabIndex($scope, gab.Id);
                    $scope.gabs.Gabs[i].NbOfLikes += 1;
                    $scope.gabs.Gabs[i].isLiked = true;

                }).error(function (error) {
                    alert(error.Message);
                });
            }

            //unlike a gab
            $scope.unLikeGab = function (gab) {
                gabService.unLikeGab(gab.Id).success(function (result) {
                    var i = getGabIndex($scope, gab.Id);
                    $scope.gabs.Gabs[i].NbOfLikes -= 1;
                    $scope.gabs.Gabs[i].isLiked = false;


                }).error(function (error) {
                    alert(error.Message);
                });
            }

            //load comments
            $scope.getComments = function (gabId) {
                var i = 0;
                for (i = 0; i < $scope.gabs.Gabs.length; i++) {
                    if ($scope.gabs.Gabs[i].Id == gabId) {
                        //if already load do nothing
                        if ($scope.gabs.Gabs[i].showComment) {
                            return;
                        }
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
                var i = getGabIndex($scope, gabId);
                $scope.gabs.Gabs[i].showComment = true;

                gabService.getGabMoreComments(gabId, $scope.gabs.Gabs[i].comments.NbOfShownComments)
                    .then(function (result) {
                        //get gab
                        $scope.gabs.Gabs[i].comments.NbOfShownComments += result.data.NbOfShownComments;
                        var tab = $scope.gabs.Gabs[i].comments.Comments.reverse();
                        Array.prototype.push.apply(tab, result.data.Comments);
                        $scope.gabs.Gabs[i].comments.Comments = tab.reverse();
                    });
            }


            //add a new comment
            $scope.newComment = { Message: "",isSend:false };
            $scope.addComment = function (gabId) {
                $scope.newComment.isSend = true;
                gabService.addComment(gabId, $scope.newComment).success(function (result) {
                    $scope.newComment.isSend = false;
                    $scope.newComment.Message = "";
                    var i = getGabIndex($scope, gabId);
                    $scope.gabs.Gabs[i].showComment = true;
                    $scope.gabs.Gabs[i].NbOfComments++;
                    $scope.gabs.Gabs[i].comments.Comments.push(result);

                }).error(function (error) {
                    alert(error.Message);
                    $scope.newComment.isSend = false;

                });
            }

        }]);