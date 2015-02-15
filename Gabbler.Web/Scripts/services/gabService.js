'use strict';
app.factory('gabService', ['$http', function ($http) {

    var gabServiceFactory = {};

    var _getAllGabs = function () {

        return $http.get(serviceBase + 'gabs').then(function (results) {
            return results;
        });
    };

    var _getMoreGabs = function(startGab) {
        return $http.get(serviceBase + 'Gabs/Start/'+ startGab).then(function (results) {
            return results;
        });
    }
    var _getGabComments = function (gabId) {
        return $http.get(serviceBase + 'Gabs/' + gabId + '/Comments' ).then(function (results) {
            return results;
        });
    }

    var _getGabMoreComments = function (gabId,start) {
        return $http.get(serviceBase + 'Gabs/' + gabId + '/Comments/'+start).then(function (results) {
            return results;
        });
    }

    var _likeGab = function (gabId) {
        return $http.post(serviceBase + 'Gabs/' + gabId + '/Like').success(function (results) {
            return results;
        }).error(function(err) {
            return err;
        });
    }
    var _unLikeGab = function (gabId) {
        return $http.delete(serviceBase + 'Gabs/' + gabId + '/Like').success(function (results) {
            return results;
        }).error(function (err) {
            return err;
        });
    }

    var _addComment=function(gabId,comment) {
        return $http.post(serviceBase + 'Gabs/' + gabId + '/Comments', comment).success(function (results) {
            return results;
        }).error(function (err) {
            return err;
        });
    }

    gabServiceFactory.getAllGabs = _getAllGabs;
    gabServiceFactory.getGabComments = _getGabComments;
    gabServiceFactory.getGabMoreComments = _getGabMoreComments;
    gabServiceFactory.addComment = _addComment;
    gabServiceFactory.getMoreGabs = _getMoreGabs;
    gabServiceFactory.likeGab = _likeGab;
    gabServiceFactory.unLikeGab = _unLikeGab;

    return gabServiceFactory;

}]);