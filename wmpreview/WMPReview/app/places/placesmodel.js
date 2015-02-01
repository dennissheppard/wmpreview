(function(){
  angular.module('foodApp.places')
    .factory('PlacesModel', Places);

  function Places(){
    var places = {
      Name: '',
      Address: '',
      Hours: '',
      Tags: [],
      Url: '',
      Phone: ''
    };

    return places;

  }
})();