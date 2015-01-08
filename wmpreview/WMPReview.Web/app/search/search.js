(function () {
  angular
    .module('foodApp.search')
    .controller('Search', Search);

  Search.$inject = [];

  /* @ngInject */
  function Search() {
    /* jshint validthis: true */
    var vm = this;

    vm.activate = activate;
    vm.title = 'search';

    activate();

    ////////////////

    function activate() {
    }


  }
})();