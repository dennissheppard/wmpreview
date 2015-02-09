(function(){

  angular.module('foodApp.places')
  .controller('Places', Places);

  Places.$inject = ['PlacesService', '$state']

  function Places(PlacesService, $state){
    /*jshint validthis: true */
    var vm = this;
    vm.title = "Add a place";
    vm.search = search;
    
    activate();
    vm.businesses = businesses;
    //DOM events
    

    ///////

      
      var businesses = "";
    function activate(){
       /*
        * any startup code goes here
        */
        
    }
      
    function search(){
       searchToAddPlace(vm.searchTerm);
    }
      

    function searchToAddPlace(term){
        businesses = PlacesService.getYelpEntries(term);
        $state.go('places.add.results');
        
//      PlacesService.Add(vm).then(function(response){
//        if(response === 'success'){
//          //show toastr
//        }
//      });
    }

  }




})();