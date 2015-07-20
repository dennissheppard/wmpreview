(function () {
  angular
    .module('foodApp.search')
    .factory('SearchService', SearchService);

  SearchService.$inject = ['ServiceManager', 'PlacesService'];

  /* @ngInject */
  function SearchService(ServiceManager, PlacesService) {
    var search = {
      getNearbyPlaces: GetNearbyPlaces,
      getPlacesBySearchTerm: GetPlacesBySearchTerm,
      getSearchResultsFiltered: GetSearchResultsFiltered, 
      
    };
    return search;


    
    ////////////////
    function GetNearbyPlaces(){
      navigator.geolocation.getCurrentPosition(geoLocationSuccess);
      var url = 'app/data/placesnearby.json';// 'api/placesbylocation/' + that.lat + '/' + that.long;
      return ServiceManager.Get(url);
    }

    function GetPlacesBySearchTerm(term) {
        return PlacesService.getYelpData(term, true).success(function(data){
            search.resultData = data;
            return search.resultData;
        }).error(function(error){
            return error;
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