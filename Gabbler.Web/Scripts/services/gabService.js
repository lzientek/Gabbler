'use strict';
app.factory('gabService', ['$http', function ($http) {

    var gabServiceFactory = {};

    var _getAllGabs = function () {

        return $http.get(serviceBase + 'gabs').then(function (results) {
            return results;
        });
    };

    var _getAllUserGabs = function (userId) {

        return $http.get(serviceBase + 'Users/'+userId+'/gabs').then(function (results) {
            return results;
        });
    };

    var _getGab = function (gabId) {

        return $http.get(serviceBase + 'gabs/' + gabId).then(function (results) {
            return results;
        });
    };

    var _getMoreGabs = function (startGab) {
        return $http.get(serviceBase + 'Gabs/Start/' + startGab).then(function (results) {
            return results;
        });
    }

    var _getMoreUserGabs = function (userId,startGab) {
        return $http.get(serviceBase + 'Users/' + userId + '/gabs/' + startGab).then(function (results) {
            return results;
        });
    }

    var _getGabComments = function (gabId) {
        return $http.get(serviceBase + 'Gabs/' + gabId + '/Comments').then(function (results) {
            return results;
        });
    }

    var _getGabMoreComments = function (gabId, start) {
        return $http.get(serviceBase + 'Gabs/' + gabId + '/Comments/' + start).then(function (results) {
            return results;
        });
    }

    var _likeGab = function (gabId) {
        return $http.post(serviceBase + 'Gabs/' + gabId + '/Like').success(function (results) {
            return results;
        }).error(function (err) {
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

    var _addComment = function (gabId, comment) {
        return $http.post(serviceBase + 'Gabs/' + gabId + '/Comments', comment).success(function (results) {
            return results;
        }).error(function (err) {
            return err;
        });
    }

    var _addGab = function (gab) {
        return $http.post(serviceBase + 'Gabs/', gab).success(function (results) {
            return results;
        }).error(function (err) {
            return err;
        });
    }

    var _editGab = function (gabId, gab) {
        return $http.put(serviceBase + 'Gabs/' + gabId, gab).success(function (results) {
            return results;
        }).error(function (err) {
            return err;
        });
    };

    var _deleteGab = function (gabId) {
        return $http.delete(serviceBase + 'Gabs/' + gabId).success(function (results) {
            return results;
        }).error(function (err) {
            return err;
        });
    };

    var _getTimeLineGabs = function() {
        return $http.get(serviceBase + 'Gabs/TimeLine').then(function (results) {
            return results;
        });

    }
    var _getMoreTimeLineGabs = function (startGab) {
        return $http.get(serviceBase + 'Gabs/TimeLine/'+startGab).then(function (results) {
            return results;
        });
    }



    gabServiceFactory.getAllGabs = _getAllGabs;
    gabServiceFactory.getAllUserGabs = _getAllUserGabs;
    gabServiceFactory.getGab = _getGab;
    gabServiceFactory.getTimeLineGabs = _getTimeLineGabs;
    gabServiceFactory.getMoreTimeLineGabs = _getMoreTimeLineGabs;
    gabServiceFactory.getGabComments = _getGabComments;
    gabServiceFactory.getGabMoreComments = _getGabMoreComments;
    gabServiceFactory.getMoreGabs = _getMoreGabs;
    gabServiceFactory.getMoreUserGabs = _getMoreUserGabs;

    gabServiceFactory.addGab = _addGab;
    gabServiceFactory.addComment = _addComment;

    gabServiceFactory.editGab = _editGab;
    gabServiceFactory.deleteGab = _deleteGab;

    gabServiceFactory.likeGab = _likeGab;
    gabServiceFactory.unLikeGab = _unLikeGab;

    return gabServiceFactory;

}]);