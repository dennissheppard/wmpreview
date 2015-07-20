(function(){

  angular.module('foodApp.places')
  .controller('Places', Places);

  Places.$inject = ['$state', 'PlacesService']

  function Places($state, PlacesService){
    /*jshint validthis: true */
    var vm = this;
    vm.search = search;
    
    activate();

    ///////

      
    vm.businesses = '';

    function activate(){

    }
      
    function search(){
        PlaceService.getYelpData(vm.searchTerm, true).then(function(response){
            $state.go('places.add');
        });
    }
      



  }




})();