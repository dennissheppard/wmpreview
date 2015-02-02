(function () {
    angular
        .module('foodApp.search')
        .controller('Filters', Filters);

    Filters.$inject = ['SearchManager'];

    /* @ngInject */
    function Filters(SearchManager) {
            /* jshint validthis: true */
        var vm = this;

        vm.sumbitFilter = submitFilter;

        vm.activate = activate;
        vm.title = 'Filters';

        activate();

        ////////////////

        function activate() {
            submitFilter();
        }

        function submitFilter(){
            var filters = "";
            var grabAndGo = vm.grabAndGo;

            SearchManager.getSearchResultsFiltered(filters).then(function(response){

                //where im going to call the search service and i'll pass it service.
                //have to create service
                //and that service will hit the service manager that actually calls the api
                //this happens in other places in the app;
            });
        }
    }
})();