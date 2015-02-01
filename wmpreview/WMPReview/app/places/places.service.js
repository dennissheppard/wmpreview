(function(){
  angular.module('foodApp.places')
    .factory('PlacesService', Places);

  Places.$inject = ['apiConstants'];

  function Places(apiConstants){
    var places = {
      Add: add
    };

    return places;


    function add(placeInfo){
      var data = {
        address: placeInfo.placeAddress,
        phone: placeInfo.placePhone,
        hours: placeInfo.placeHours,
        tags: placeInfo.placeTags.split(','),
        url: placeInfo.placeUrl,
        name: placeInfo.placeName
      };

      return $http.post(apiConstants.apiUrl, data).then(
        function(response){
          //get a positive response from api
        },
        function(error){
          console.log(error);

        }
      );
    }
  }
})();