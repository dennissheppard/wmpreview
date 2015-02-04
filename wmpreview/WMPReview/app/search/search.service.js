(function () {
  angular
    .module('foodApp.search')
    .factory('SearchManager', SearchManager);

  SearchManager.$inject = ['ServiceManager'];

  /* @ngInject */
  function SearchManager(ServiceManager) {
    var that = this;
    return {
      getNearbyPlaces: GetNearbyPlaces,
      getPlacesBySearchTerm: GetPlacesBySearchTerm,
      getSearchResultsFiltered: GetSearchResultsFiltered
    };

    ////////////////
    function GetNearbyPlaces(){
      navigator.geolocation.getCurrentPosition(geoLocationSuccess);
      var url = 'app/data/placesnearby.json';// 'api/placesbylocation/' + that.lat + '/' + that.long;
      return ServiceManager.Get(url);
    }

    function GetPlacesBySearchTerm(term) {
        var url = 'app/data/placesbysearch.json';
        return ServiceManager.Get(url).then(function (results) {
            that.searchResults = results;
        });
    }

    function GetSearchResultsFiltered(filter){
        var url = 'app/data/placesbysearchfilter.json';
        return ServiceManager.Get(url);
    }
    /////private methods/////
    function geoLocationSuccess(pos){
      that.lat = pos.coords.latitude;
      that.long = pos.coords.longitude;
    }

  }

})();