(function () {
  'use strict';

  angular
    .module('foodApp.layout')
    .controller('Shell', Shell);

  Shell.$inject = ['config', 'logger'];

  function Shell(config, logger) {
    /*jshint validthis: true */
    var vm = this;

    vm.title = config.appTitle;

    activate();

    function activate() {
      logger.success(config.appTitle + ' loaded!', null);
    }
  }
})();