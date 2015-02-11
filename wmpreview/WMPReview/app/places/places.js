(function(){

  angular.module('foodApp.places')
  .controller('Places', Places);

  Places.$inject = ['PlacesService', '$state']

  function Places(PlacesService, $state){
    /*jshint validthis: true */
    var vm = this;
    vm.search = search;
    
    activate();
    vm.businesses = businesses;
    //DOM events
    

    ///////

      
    vm.businesses = '';
    function activate(){
       /*
        * any startup code goes here
        */
        
    }
      
    function search(){
       searchToAddPlace(vm.searchTerm);
    }
      

    function searchToAddPlace(term){
        vm.businesses = PlacesService.getYelpEntries(term);
        

    }

  }




})();