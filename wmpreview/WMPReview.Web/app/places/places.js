(function(){

  angular.module('foodApp.places')
  .controller('Places', Places);

  Places.$inject = ['PlacesService']

  function Places(PlacesService){
    /*jshint validthis: true */
    var vm = this;

    activate();

    //DOM events
    vm.addPlace = AddPlace;

    ///////

    function activate(){
      /*
       * any startup code goes here
       */
    }

    function AddPlace(){

      PlacesService.Add(vm).then(function(response){
        if(response === 'success'){
          //show toastr
        }
      });
    }

  }




})();