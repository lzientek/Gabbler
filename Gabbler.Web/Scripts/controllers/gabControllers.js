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
                        result.data.Gabs[i].isLiked = result.data.Gabs[i].Likes.indexOf($rootScope.authentication.userName) !== -1;
                        result.data.Gabs[i].showComment = false;
                    }

                    Array.prototype.push.apply($scope.gabs.Gabs, result.data.Gabs);
                    $scope.gabs.isAllShow = $scope.gabs.NbOfShownGabs >= result.data.TotalGabs;

                });
            }

            $scope.newGab = { Content: "" };
            $scope.addGab = function () {
                var content = { Content: $scope.newGab.Content };
                $scope.newGab.Content = ""; //reset the field value
                gabService.addGab(content)
                    .success(function (result) {
                        for (var i = $scope.gabs.Gabs.length - 1; i >= 0; i--) {
                            $scope.gabs.Gabs[i + 1] = $scope.gabs.Gabs[i];
                        }
                        $scope.gabs.Gabs[0] = result;
                        $scope.gabs.NbOfShownGabs++;
                        $scope.gabform.$setPristine();

                    }).error(function (error) {
                        $scope.newGab.newGab = content;
                        alert(error.Message, 'danger');
                    });
            }

            //page vars
            $scope.$root.title = 'Welcome on gabbler!';
            $scope.$on('$viewContentLoaded', function () {
                $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
            });
        }
    ])

         // Path: /
    .controller('TimeLineCtrl', [
        '$scope', '$location', '$window', '$rootScope', 'gabService',
        function ($scope, $location, $window, $rootScope, gabService) {


            //get gabs
            gabService.getTimeLineGabs().then(function (result) {
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
                gabService.getMoreTimeLineGabs($scope.gabs.NbOfShownGabs).then(function (result) {
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

            $scope.newGab = { Content: "" };
            $scope.addGab = function () {
                var content = { Content: $scope.newGab.Content };
                $scope.newGab.Content = ""; //reset the field value
                gabService.addGab(content)
                    .success(function (result) {
                        for (var i = $scope.gabs.Gabs.length - 1; i >= 0; i--) {
                            $scope.gabs.Gabs[i + 1] = $scope.gabs.Gabs[i];
                        }
                        $scope.gabs.Gabs[0] = result;
                        $scope.gabs.NbOfShownGabs++;
                        $scope.gabform.$setPristine();

                    }).error(function (error) {
                        $scope.newGab.newGab = content;
                        alert(error.Message, 'danger');
                    });
            }

            //page vars
            $scope.$root.title = 'My TimeLine - gabbler';
            $scope.$on('$viewContentLoaded', function () {
                $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
            });
        }
    ])

    .controller('UserGabCtrl', [
        '$scope', '$location', '$stateParams', '$window', '$rootScope', 'gabService', 'userServices',
        function ($scope, $location, $stateParams, $window, $rootScope, gabService, userServices) {

            var id = $stateParams.userId;
            $scope.isFollow = false;
            
            if ($rootScope.authentication.isAuth) {
                userServices.isFollowingById(id).then(function (result) {
                    $scope.isFollow = result.data;
                });
            }

            //get user
            userServices.getUserById(id).then(function (result) {
                $scope.$root.title = result.data.Pseudo + ' - Gabbler';
                $scope.user = result.data;
                $('body').css("background-image", "url(" + serviceBase + $scope.user.BackgroundImagePath + ")");
                
            });

            $scope.follow = function () {
                userServices.follow(id).success(function (result) {
                    $scope.user.NbFollowers++;
                    $scope.isFollow = true;

                }).error(function (error) {
                    $scope.searchVal = "";
                    alert(error.Message);
                });
            }

            $scope.unfollow = function () {
                userServices.unFollow(id).success(function (result) {
                    $scope.user.NbFollowers--;
                    $scope.isFollow = false;

                }).error(function (error) {
                    $scope.searchVal = "";
                    alert(error.Message);
                });
            }

            //get gabs
            gabService.getAllUserGabs(id).then(function (result) {
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
                gabService.getMoreUserGabs(id, $scope.gabs.NbOfShownGabs).then(function (result) {
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

            $scope.newGab = { Content: "" };
            $scope.addGab = function () {
                var content = { Content: $scope.newGab.Content };
                $scope.newGab.Content = ""; //reset the field value
                $scope.gabform.$setPristine();

                gabService.addGab(content)
                    .success(function (result) {
                        for (var i = $scope.gabs.Gabs.length - 1; i >= 0; i--) {
                            $scope.gabs.Gabs[i + 1] = $scope.gabs.Gabs[i];
                        }
                        $scope.gabs.Gabs[0] = result;
                        $scope.gabs.NbOfShownGabs++;
                        $scope.user.NbGab++;
                        $scope.gabform.$setPristine();

                    }).error(function (error) {
                        $scope.newGab.newGab = content;
                        alert(error.Message, 'danger');
                    });
            }


            //page vars
            $scope.$root.title = '';
            $scope.$on('$viewContentLoaded', function () {

                $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
            });
        }
    ])

    .controller('GabIdCtrl', ['$scope', '$rootScope', '$stateParams', 'gabService',
        function ($scope,$rootScope, $stateProvider, gabService) {

            //get the gab
            gabService.getGab($stateProvider.gabId).then(function (result) {
                $scope.gab = result.data;
                $scope.gab.isLiked = $scope.gab.Likes.indexOf($rootScope.authentication.userName) !== -1;
                $scope.gab.showComment = true;

                gabService.getGabComments($scope.gab.Id).then(function (result) {
                    //get gab
                    result.data.Comments = result.data.Comments.reverse();
                    $scope.gab.comments = result.data;
                });
            });

            // like a gab
            $scope.likeGab = function (gab) {
                gabService.likeGab(gab.Id).success(function (result) {
                    $scope.gab.NbOfLikes += 1;
                    $scope.gab.isLiked = true;

                }).error(function (error) {
                    alert(error.Message, 'danger');
                });
            }

            //unlike a gab
            $scope.unLikeGab = function (gab) {
                gabService.unLikeGab(gab.Id).success(function (result) {
                    var i = getGabIndex($scope, gab.Id);
                    $scope.gab.NbOfLikes -= 1;
                    $scope.gab.isLiked = false;


                }).error(function (error) {
                    alert(error.Message, 'danger');
                });
            }

        }])

    //edit controller
    .controller('EditGabCtrl', ['$scope', '$stateParams', '$window', '$rootScope', 'gabService',
        function ($scope, $stateProvider, $window, $rootScope, gabService) {
            //get the gab
            gabService.getGab($stateProvider.gabId).then(function (result) {
                $scope.gab = result.data;
            });

            //edit the gab
            $scope.edit = function () {
                var editgab = { Content: $scope.gab.Content };
                gabService.editGab($stateProvider.gabId, editgab).success(function (result) {
                    $scope.gab = result;
                    alert('saved');
                }).error(function (error) {
                    alert(error.Message, 'danger');
                });
            }

            //delete a gab
            $scope.delete = function () {
                valid("Do you want to delete this gab?", "danger", function () {
                    gabService.deleteGab($scope.gab.Id).success(function (result) {
                        alert("Deleted!");
                        history.back();
                    }).error(function (error) {
                        alert(error.Message, 'danger');
                    });
                });
            }
        }])

    //like\comment controlleur
    .controller('likeCommentCtrl', ['$scope', '$rootScope', 'gabService',
        function ($scope, $rootScope, gabService) {

            // like a gab
            $scope.likeGab = function (gab) {
                gabService.likeGab(gab.Id).success(function (result) {
                    var i = getGabIndex($scope, gab.Id);
                    $scope.gabs.Gabs[i].NbOfLikes += 1;
                    $scope.gabs.Gabs[i].isLiked = true;

                }).error(function (error) {
                    alert(error.Message, 'danger');
                });
            }

            //unlike a gab
            $scope.unLikeGab = function (gab) {
                gabService.unLikeGab(gab.Id).success(function (result) {
                    var i = getGabIndex($scope, gab.Id);
                    $scope.gabs.Gabs[i].NbOfLikes -= 1;
                    $scope.gabs.Gabs[i].isLiked = false;


                }).error(function (error) {
                    alert(error.Message, 'danger');
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
        }])

    //add comment controlleur
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
            $scope.newComment = { Message: "" };
            $scope.addComment = function (gabId, form) {
                gabService.addComment(gabId, $scope.newComment).success(function (result) {
                    $scope.newComment.Message = "";
                    form.$setPristine();
                    var gab;
                    if ($scope.gabs !==undefined) {
                        var i = getGabIndex($scope, gabId);
                        gab =$scope.gabs.Gabs[i];
                    }
                    else if ($scope.gab !== undefined) {
                        gab = $scope.gab;
                    }
                    if (gab != undefined) {
                        gab.showComment = true;
                        gab.NbOfComments++;
                        gab.comments.TotalComments++;
                        gab.comments.NbOfShownComments++;
                        gab.comments.Comments.push(result);
                    }

                }).error(function (error) {
                    alert(error.Message, 'danger');
                });
            }

        }]);