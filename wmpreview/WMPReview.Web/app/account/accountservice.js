(function(){
  angular.module('services.account', [/*'models.account'*/])
    .factory('AccountService', Account);

  Account.$inject = ['$q', 'AccountModel'];
  function Account($q, AccountModel){
    var AccountService = {
      Login: login
    };

    return AccountService;
    /////

    function login(username, password){
      var deferred = $q.defer();
      deferred.resolve(username + password);
      AccountModel.IsLoggedIn = true;
      return deferred.promise;

    }

    function getFavoriteRest(userId){
      //fetch rest. from api
      AccountModel.FavRest = apiReturn;
    }
  }

})();