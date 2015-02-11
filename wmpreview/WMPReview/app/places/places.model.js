(function(){
  angular.module('foodApp.places')
    .service('PlacesModel', Places);

  function Places(){
      var that = this;

      this.places = [];


      this.place = function(data){
          that.places.push(data);
      }



  }
})();