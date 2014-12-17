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
    'foodApp.places'


  ]);


})();