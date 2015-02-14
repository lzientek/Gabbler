'use strict';
app.factory('userServices', ['$http', function ($http) {

    var usrServiceFactory = {};
    var _getActualUser = function () {

        return $http.get(serviceBase + 'Account/Me').then(function (results) {
            return results;
        });
    };


    usrServiceFactory.getActualUser = _getActualUser;

    return usrServiceFactory;

}]);