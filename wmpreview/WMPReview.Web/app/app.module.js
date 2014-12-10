(function () {
  'use strict';

  angular.module('foodApp', [
    /*
     * Order is not important. Angular makes a
     * pass to register all of the modules listed
     * and then when app.dashboard tries to use app.data,
     * it's components are available.
     */

    /*
     * Everybody has access to these.
     * We could place these under every feature area,
     * but this is easier to maintain.
     */
    'foodApp.core',
    'foodApp.widgets',

    /*
     * Feature areas
     */
    'foodApp.admin',
    'foodApp.dashboard',
    'foodApp.layout',
    //'foodApp.search',
    'foodApp.login',
    'services.account',
    'models.account'
  ]);

})();