(function () {
  angular
    .module('foodApp.search')
    .factory('SearchManager', SearchManager);

  SearchManager.$inject = ['ServiceManager'];

  /* @ngInject */
  function SearchManager(ServiceManager) {
    var that = this;
    return {
      GetClosestPlaces: getClosestPlaces

    };

    ////////////////

    function getLocation(){
     return navigator.geolocation.getCurrentPosition(function(pos){
        that.lat = pos.coords.latitude;
        that.long = pos.coords.longitude;
      })
    }
    function getClosestPlaces(){
      getLocation();
      var url = 'app/data/placesnearby.json';
      return ServiceManager.Get(url);
    }

  }

})();