(function(){
  angular.module('models.account', [])
    .factory('AccountModel', Model);

  function Model(){
    var AccountModel = {

      IsLoggedIn: false,
      Username: '',
      Password: '',
      LastLoggedIn: '',
      FavoriteRestaurant: '',
      ReviewedRestaurants: ''
    };

    return AccountModel;
  }


})();