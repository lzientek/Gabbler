'use strict';
app.factory('searchService', ['$http', function ($http) {

    var serviceFactory = {};

    var _getBasicSearch = function (val) {

        return $http.get(serviceBase + 'basicsearch/'+val).then(function (results) {
            return results;
        });
    };


    var _getSearch = function (val, val2, val3) {

        return $http.get(serviceBase + 'search/' + val + '/' + val2 + '/' + val3 ).then(function (results) {
            return results;
        });
    };
    serviceFactory.getBasicSearch = _getBasicSearch;
    serviceFactory.getSearch = _getSearch;
    
    return serviceFactory;

}]);