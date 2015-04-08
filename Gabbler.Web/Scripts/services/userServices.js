'use strict';
app.factory('userServices', ['$http', function ($http) {

    var usrServiceFactory = {};
    var _getActualUser = function () {

        return $http.get(serviceBase + 'Account/Me').then(function (results) {
            return results;
        });
    };
    
    var _getUserById = function (userId) {

        return $http.get(serviceBase + 'Users/'+userId).then(function (results) {
            return results;
        });
    };

    var _editUser = function(data) {
        return $http.put(serviceBase + 'Account/Me',data).success(function (results) {
            return results;
        }).error(function(error) {
            return error;
        });
    }

    var _isFollowing = function (arrayId) {
        return $http.post(serviceBase + 'Users/IsFollowing', arrayId).then(function (results) {
            return results;
        });
    }
    var _isFollowingById = function (id) {
        return $http.get(serviceBase + 'Users/IsFollowing/'+id).then(function (results) {
            return results;
        });
    }

    var _follow = function (id) {
        return $http.post(serviceBase + 'Follow/' + id).success(function (results) {
            return results;
        }).error(function (error) {
            return error;
        });
    }
    var _unFollow = function (id) {
        return $http.delete(serviceBase + 'UnFollow/' + id).success(function (results) {
            return results;
        }).error(function (error) {
            return error;
        });
    }

    usrServiceFactory.getActualUser = _getActualUser;
    usrServiceFactory.getUserById = _getUserById;

    usrServiceFactory.editUser = _editUser;

    usrServiceFactory.isFollowing = _isFollowing;
    usrServiceFactory.isFollowingById = _isFollowingById;

    usrServiceFactory.follow = _follow;
    usrServiceFactory.unFollow = _unFollow;

    return usrServiceFactory;

}]);