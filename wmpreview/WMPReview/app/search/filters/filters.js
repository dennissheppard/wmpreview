(function () {
    angular
        .module('foodApp.search')
        .controller('Filters', Filters);

    Filters.$inject = ['SearchManager'];

    /* @ngInject */
    function Filters(SearchManager) {
            /* jshint validthis: true */
        var vm = this;

        vm.submitFilter = submitFilter;
        vm.testingClick = testingClick;

        vm.activate = activate;
        vm.title = 'Filters';

        activate();

        ////////////////

        function activate() {
        }

        function submitFilter(){
            var filters = new {
                distanceFromUser: vm.distanceFromUser,
                greatForClients: vm.greatForClients,
                awesomeDrinks: vm.awesomeDrinks,
                openLate: vm.openLate,
                greatService: vm.greatService,
                grabAndGo: vm.grabAndGo
            };
            SearchManager.getSearchResultsFiltered(filters).then(function(response){

                //where im going to call the search service and i'll pass it service.
                //have to create service
                //and that service will hit the service manager that actually calls the api
                //this happens in other places in the app;
            });
        }

        function testingClick(){
            var filters = {
                distanceFromUser: vm.distanceFromUser,
                greatForClients: vm.greatForClients,
                awesomeDrinks: vm.awesomeDrinks,
                openLate: vm.openLate,
                greatService: vm.greatService,
                grabAndGo: vm.grabAndGo
            };
            SearchManager.getSearchResultsFiltered(filters).then(function(response){
                var filteredPlaces = response.data.places;
            });
        }
    }
})();