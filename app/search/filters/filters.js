(function () {
    angular
        .module('foodApp.search')
        .controller('Filters', Filters);

    Filters.$inject = ['$state','SearchService'];

    /* @ngInject */
    function Filters($state, SearchService) {
            /* jshint validthis: true */
        var vm = this;

        vm.submitFilter = submitFilter;

        vm.activate = activate;
        vm.title = 'Filters';

        activate();

        ////////////////

        function activate() {
        }


        function submitFilter(){
            var filters = {
                distanceFromUser: vm.distanceFromUser,
                greatForClients: vm.greatForClients,
                awesomeDrinks: vm.awesomeDrinks,
                openLate: vm.openLate,
                greatService: vm.greatService,
                grabAndGo: vm.grabAndGo
            };
            SearchService.getSearchResultsFiltered(filters).then(function(response){
                vm.filteredPlaces = response.data.places;
                $state.go('search.filter-results');
            });
        }
    }
})();