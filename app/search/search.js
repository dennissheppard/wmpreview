(function () {
  angular
    .module('foodApp.search')
    .controller('Search', Search);

  Search.$inject = ['$state', 'SearchManager'];

  /* @ngInject */
  function Search($state, SearchManager) {
    /* jshint validthis: true */
    var vm = this;

    vm.activate = activate;
    vm.title = 'Find a place';
    vm.search = search;
    vm.filter = filter;

    activate();

    ////////////////

    function activate() {
      if(SearchManager.resultData){
        vm.searchResults = SearchManager.resultData.businesses;
      }
      else{
        vm.searchTerm = '';
      }
      // send coordinates
      //SearchManager.getNearbyPlaces().then(function(response){
      //  vm.nearbyPlaces = response.data.places;
      //});
    }

    function search(){
      SearchManager.getPlacesBySearchTerm(vm.searchTerm).success(function(results){
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