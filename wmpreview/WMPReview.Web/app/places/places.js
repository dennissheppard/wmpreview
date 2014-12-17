(function(){

  angular.module('foodApp.places')
  .controller('Places', Places);

  Places.$inject = ['PlacesService']

  function Places(PlacesService){
    /*jshint validthis: true */
    var vm = this;

    //DOM events
    vm.addPlace = AddPlace;

    activate();


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