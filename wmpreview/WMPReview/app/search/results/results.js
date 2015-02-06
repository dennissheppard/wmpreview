(function () {
    angular
        .module('foodApp.search.results')
        .controller('Results', Results);

    Results.$inject = [];

    /* @ngInject */
    function Results() {
        /* jshint validthis: true */
        var vm = this;

        vm.activate = activate;
        vm.title = 'Results';

        activate();

        ////////////////

        function activate() {
        }


    }
})();