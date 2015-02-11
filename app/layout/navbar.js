(function () {
  'use strict';

  angular
    .module('foodApp.layout')
    .controller('Navbar', Navbar);

  Navbar.$inject = ['$scope'];

  function Navbar($scope) {
    /*jshint validthis: true */
    var vm = this;

    vm.navigateBack = navigateBack;


    activate();
    setBindings();

    function activate() {
      //any start up code goes in here

    }

    function setBindings(){
      //set binding listeners in here i.e: $scope.$on('someevent', functionName);

    }

    function navigateBack(){

    }


  }
})();