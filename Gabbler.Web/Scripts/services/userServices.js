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

    usrServiceFactory.getActualUser = _getActualUser;
    usrServiceFactory.getUserById = _getUserById;

    usrServiceFactory.editUser = _editUser;

    return usrServiceFactory;

}]);