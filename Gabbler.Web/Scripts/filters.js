﻿'use strict';

angular.module('app.filters', [])

    .filter('interpolate', ['version', function (version) {
        return function (text) {
            return String(text).replace(/\%VERSION\%/mg, version);
        }
    }])

.filter('join', ['version', function (version) {
    return function (array) {
        return array.join(",<br/>");
    }
}]);