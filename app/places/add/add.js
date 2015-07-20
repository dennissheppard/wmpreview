(function () {
    angular.module('foodApp.places.add')
        .controller('Add', Add);

    Add.$inject = ['PlacesService'];

    function Add(PlacesManager){

        var vm = this;

        activate();

        vm.yelpResults = '';

        function activate(){

            vm.yelpResults = PlacesManager.GetYelpResults();
        }



    }
})();