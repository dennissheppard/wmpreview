(function(){
  angular.module('foodApp.userAccount')
    .factory('AccountService', Account);

  Account.$inject = ['$http', 'AccountModel', 'foodApi'];
  function Account($http, AccountModel, foodApi){
    var AccountService = {
      Login: login
    };

    return AccountService;
    /////

    function login(username, password){
      var credentials = {
        username: username,
        password: password
      };
      return $http.post(foodApi, credentials).then(
        function(data, status, headers, config){
          AccountModel = data;
        },
        function(data, status, headers, config){
          AccountModel.error = data;
        }
      );
    }
  }
})();