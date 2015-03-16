'use strict';
app.factory('searchService', ['$http', function ($http) {

    var gabServiceFactory = {};

    var _getSearch = function () {

        return $http.get(serviceBase + 'gabs').then(function (results) {
            return results;
        });
    };

    gabServiceFactory.getSearch = _getSearch;


    return gabServiceFactory;

}]);