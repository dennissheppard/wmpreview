(function () {
  'use strict';

  angular.module('foodApp', [
    /*
     * 3rd party imports
     */
    'ui.router',

    /*
     * Shared modules
     */
    'foodApp.layout',
    'foodApp.widgets',

    /*
     * Feature areas
     */
    'foodApp.login',
    'foodApp.places',
    'foodApp.userAccount',
    'foodApp.places',
    'foodApp.search'
  ])
    .config(['$stateProvider', '$urlRouterProvider', '$locationProvider', function($stateProvider, $urlRouterProvider, $locationProvider) {
      $stateProvider
        .state('login', {
          url: '',
          templateUrl: 'app/login/login.html',
          controller: 'Login as vm'
        })
        .state('search', {
          url: '/search',
          templateUrl: 'app/search/search.html',
          controller: 'Search as vm'
        })
        .state('search.results', {
          // url will become '/search/results'
          url: '/:searchTerm/results',
          templateUrl: 'app/search/results/search.results.html',
          controller: 'Results as vm'
        })
        .state('search.filter-results', {
            // url will become '/search/results'
            url: '/search/filter-results',
            templateUrl: 'app/search/filter_results/search.filter-results.html',
            controller: 'FilterResults as vm'
        })
        .state('places', {
          // url will become '/places'
          abstract: true,
          url: '/places',
          templateUrl: 'app/places/places.html',
          controller: 'Places as vm'
        }).state('places.add', {
          // url will become '/places/add'
          url: '/add',
          templateUrl: 'app/places/places.form.html',
          controller: 'Places as vm'
        })
        .state('places.edit', {
          // url will become '/places/12/edit'
          url: '/:placeId/edit',
          templateUrl: 'app/places/places.form.html',
          controller: 'Places as vm'
        })
        .state('places.review', {
          // url will become '/places/12/review'
          url: '/:placeId/review',
          templateUrl: 'app/places/review/places.review.html',
          controller: 'Review as vm'
        }).state('places.details', {
          // url will become '/places/12/details'
          url: '/:placeId/details',
          templateUrl: 'app/places/details/places.details.html',
          controller: 'Details as vm'
        })
    }]);


})();