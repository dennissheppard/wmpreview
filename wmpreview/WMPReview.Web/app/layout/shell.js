(function () {
  'use strict';

  angular
    .module('foodApp.layout')
    .controller('Shell', Shell);

  Shell.$inject = ['$scope'];

  function Shell($scope) {
    /*jshint validthis: true */
    var shellVM = this;

    activate();
    setBindings();

    ///////////

    function activate() {
      //any startup code for the controller goes here
    }

    function setBindings(){
      // listen for any child controller wanting to hide the navbar
      $scope.$on('hideNav', hideNav);
    }

    // event bindings
    function hideNav(){
      shellVM.hideNav = true;
    }
  }
})();