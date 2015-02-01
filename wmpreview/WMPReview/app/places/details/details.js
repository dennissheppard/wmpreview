(function(){

  angular.module('foodApp.places.details')
  .controller('Details', Details);

  Details.$inject = ['PlacesService']

  function Details(PlacesService){
    /*jshint validthis: true */
    var vm = this;

    activate();

    //DOM events


    ///////

    function activate(){
      /*
       * any startup code goes here
       */
    }

    function GetDetails(placeId){

      PlacesService.Get(placeId).then(function(response){

      });
    }

  }




})();