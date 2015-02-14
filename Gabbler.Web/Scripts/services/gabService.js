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


    gabServiceFactory.getAllGabs = _getAllGabs;
    gabServiceFactory.getMoreGabs = _getMoreGabs;

    return gabServiceFactory;

}]);