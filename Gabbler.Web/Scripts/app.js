﻿
var serviceBase = 'http://api.gabbler.com/';

// Declares how the application should be bootstrapped. See: http://docs.angularjs.org/guide/module
var app = angular.module('app', [
    'ui.router', 'app.filters', 'app.services', 'app.directives', 'LocalStorageModule',
    'angularFileUpload',
    'app.indexController', 'app.homeControllers', 'app.gabControllers', 'app.userControllers', 'app.searchControllers'
]);

// Gets executed during the provider registrations and configuration phase. Only providers and constants can be
// injected here. This is to prevent accidental instantiation of services before they have been fully configured.
app.config(['$stateProvider', '$locationProvider', function ($stateProvider, $locationProvider) {

    // UI States, URL Routing & Mapping. For more info see: https://github.com/angular-ui/ui-router
    // ------------------------------------------------------------------------------------------------------------

        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: '/views/Home/index',
                controller: 'HomeCtrl'

            })
            .state('editGab', {
                url: '/gab/edit/:gabId',
                templateUrl: '/views/Gab/edit',
                controller: 'EditGabCtrl'

            })
            .state('GabId', {
                url: '/gab/:gabId',
                templateUrl: '/views/Gab/GabById',
                controller: 'GabIdCtrl'

            })
            .state('about', {
                url: '/about',
                templateUrl: '/views/Home/about',
                controller: 'AboutCtrl'
            })
            .state('login', {
                url: '/login',
                templateUrl: '/views/User/login',
                controller: 'LoginCtrl'
            })
            .state('register', {
                url: '/register',
                templateUrl: '/views/User/register',
                controller: 'RegisterCtrl'
            })
            .state('userGab', {
                url: '/Gab/User/:userId',
                templateUrl: '/views/User/UserGabs',
                controller: 'UserGabCtrl'
            })
            .state('search', {
                url: '/Search/:search/:numberUser/:numberGab',
                templateUrl: '/views/Search/Search',
                controller: 'BigSearchCtrl'
            })
            .state('editUser', {
                url: '/User/Edit/:userId',
                templateUrl: '/views/User/UserEdit',
                controller: 'UserEditCtrl'
            })
             .state('TimeLine', {
                 url: '/TimeLine',
                 templateUrl: '/views/Home/TimeLine',
                 controller: 'TimeLineCtrl'
             })
        .state('otherwise', {
            url: '*path',
            templateUrl: '/views/Error/404',
            controller: 'Error404Ctrl'
        });

    $locationProvider.html5Mode(true);

}])

// Gets executed after the injector is created and are used to kickstart the application. Only instances and constants
// can be injected here. This is to prevent further system configuration during application run time.
.run(['$templateCache', '$rootScope', '$state', '$stateParams','$location',
    function ($templateCache, $rootScope, $state, $stateParams, $location) {

    // <ui-view> contains a pre-rendered template for the current view
    // caching it will prevent a round-trip to a server at the first page load
    var view = angular.element('#ui-view');
    $templateCache.put(view.data('tmpl-url'), view.html());

    // Allows to retrieve UI Router state information from inside templates
    $rootScope.$state = $state;
    $rootScope.$stateParams = $stateParams;
    $rootScope.apiUrl = serviceBase;

    $rootScope.$on('$stateChangeSuccess', function (event, toState) {
        $('body').css("background-image", "");
        $rootScope.path = $location.path();

        // Sets the layout name, which can be used to display different layouts (header, footer etc.)
        // based on which page the user is located
        $rootScope.layout = toState.layout;
    });
}])

.run(['authService', function (authService) {
    authService.fillAuthData();
    $("body").tooltip({
        selector: '[data-toggle="tooltip"]',
        html: true
    });
}])

.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});
