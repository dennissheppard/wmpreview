(function () {
  angular
    .module('foodApp.search')
    .controller('Search', Search);

  Search.$inject = ['SearchManager'];

  function Search(SearchManager) {
    /* jshint validthis: true */
    var vm = this;

    vm.title = 'search';
    vm.places = [];

    activate();

    ////////////////

    function activate() {
      SearchManager.GetClosestPlaces().then(function(response){
        vm.places = response.data.places;
      }, function(fail){
        Console.log("Failed to get closest places!");
      })
    }


  }
})();