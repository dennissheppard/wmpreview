(function(){

  angular.module('foodApp.login', [])
    .config(['$stateProvider', '$urlRouterProvider', function($stateProvider, $urlRouterProvider) {
      //$urlRouterProvider.otherwise('/login');
      $stateProvider
        .state('login', {
          url: '/login',
          templateUrl: 'app/login/login.html',
          controller: 'Login',
          controllerAs: 'vm'
        });
    }]);


})();