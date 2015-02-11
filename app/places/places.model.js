(function(){
  angular.module('foodApp.places')
    .service('PlacesModel', Places);

  function Places(){
      var that = this;

      this.places = [];


      this.place = function(businesses){
          _.each(businesses, function(business){
              that.places.push(business);
          })

      }



  }
})();