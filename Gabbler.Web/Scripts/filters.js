'use strict';

angular.module('app.filters', [])

    .filter('interpolate', ['version', function (version) {
        return function (text) {
            return String(text).replace(/\%VERSION\%/mg, version);
        }
    }])

.filter('join',  function () {
    return function (array) {
        if (!array)return null;
        return array.join(",<br/>");
    }
})
.filter('substr',  function () {
    return function (text) {
        if (!text) return text;
        if (text.length > 60) {
            var smtxt = text.substring(0, 56) + " ...";
            return smtxt;
        }
        return text;
    }
});