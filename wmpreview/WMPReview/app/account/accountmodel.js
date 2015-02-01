(function(){
  angular.module('foodApp.userAccount')
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