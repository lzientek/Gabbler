'use strict';

angular.module('app.directives', [])
    .directive('appVersion', [
        'version', function(version) {
            return function(scope, elm, attrs) {
                elm.text(version);
            };
        }
    ])
    .directive('gabComments', function() {
        return {
            restrict: 'E',
            templateUrl: '/views/Gab/GabComment',
        };
    })
    .directive('fallbackSrc', function () {
        return {
            link: function (scope, element, attrs) {

                scope.$watch(function () {
                    return attrs['ngSrc'];
                }, function (value) {
                    if (!value) {
                        element.attr('src', attrs.fallbackSrc);
                    }
                });

                element.bind('error', function () {
                    element.attr('src', attrs.fallbackSrc);
                });
            }
        }

    });