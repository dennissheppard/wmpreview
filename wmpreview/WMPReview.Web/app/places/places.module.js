(function(){

  angular.module('foodApp.places', [

  ])
    .config(function($stateProvider, $urlRouterProvider) {
      $urlRouterProvider.otherwise('/');
      $stateProvider
        .state('places', {
          url: '/places',
          templateUrl: 'login/login.html',
          controller: 'login/login.js',
          controllerAs: 'vm'
        })
        .state('places.add', {
          url: '/places.add',
          templateUrl: 'places/addplace.html',
          controller: 'Places',
          controllerAs: 'vm'
        })
        .state('places.edit', {
          url: '/places.edit',
          templateUrl: 'places/editplace.html',
          controller: 'Places',
          controllerAs: 'vm'
        })

    });

})();