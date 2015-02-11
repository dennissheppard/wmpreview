(function(){

  angular.module('foodApp.places')
  .controller('Places', Places);

  Places.$inject = ['$state', 'PlacesManager']

  function Places($state, PlacesManager){
    /*jshint validthis: true */
    var vm = this;
    vm.search = search;
    
    activate();

    ///////

      
    vm.businesses = '';

    function activate(){

    }
      
    function search(){
        PlacesManager.getYelpEntries(vm.searchTerm).then(function(response){
            $state.go('places.add');
        });
    }
      



  }




})();