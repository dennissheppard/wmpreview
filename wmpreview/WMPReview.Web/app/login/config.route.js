(function () {
  'use strict';

  angular
    .module('foodApp.login')
    .run(appRun);

  // appRun.$inject = ['routehelper'];

  /* @ngInject */
  function appRun(routehelper) {
    routehelper.configureRoutes(getRoutes());
  }

  function getRoutes() {
    return [
      {
        url: '/',
        config: {
          templateUrl: 'app/login/login.html',
          controller: 'Login',
          controllerAs: 'vm',
          title: 'login'
        }
      }
    ];
  }
})();