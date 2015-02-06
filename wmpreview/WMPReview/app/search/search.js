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
    vm.title = 'search';
    vm.search = search;
    vm.filter = filter;

    activate();

    ////////////////

    function activate() {
      vm.searchTerm = '';
      // send coordinates
      SearchManager.getNearbyPlaces().then(function(response){
        vm.nearbyPlaces = response.data.places;
      });
    }

    function search(){
      SearchManager.getPlacesBySearchTerm(vm.searchTerm).then(function(){
        $state.go('search.results');
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