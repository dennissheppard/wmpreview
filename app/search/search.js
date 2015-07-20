(function () {
  angular
    .module('foodApp.search')
    .controller('Search', Search);

  Search.$inject = ['$state', 'SearchService'];

  /* @ngInject */
  function Search($state, SearchService) {
    /* jshint validthis: true */
    var vm = this;

    vm.activate = activate;
    vm.title = 'Find a place';
    vm.search = search;
    vm.filter = filter;

    activate();

    ////////////////

    function activate() {
      if(SearchService.resultData){
        vm.searchResults = SearchService.resultData.businesses;
      }
      else{
        vm.searchTerm = '';
      }
      // send coordinates
      //SearchService.getNearbyPlaces().then(function(response){
      //  vm.nearbyPlaces = response.data.places;
      //});
    }

    function search(){
      SearchService.getPlacesBySearchTerm(vm.searchTerm).success(function(results){
          angular.forEach(results.businesses, function (result, key)
          {
            result.id = encodeURI(result.id);
          });
          vm.searchResults = results.businesses;
      }).error(function (error){
          vm.error = error;
      });
    }
   

    function filter(){
        if(vm.showFilterSection){
            vm.showFilterSection = false;
        }else{
            vm.showFilterSection = true;
        }
    }
  }
})();