'use strict';
app.factory('searchService', ['$http', function ($http) {

    var serviceFactory = {};

    var _getBasicSearch = function (val) {

        return $http.get(serviceBase + 'search/'+val).then(function (results) {
            return results;
        });
    };

   
    serviceFactory.getBasicSearch = _getBasicSearch;

    return serviceFactory;

}]);