(function () {
  'use strict';

  var core = angular.module('foodApp.core');

  core.config(toastrConfig);

  /* @ngInject */
  function toastrConfig(toastr){
    toastr.options.timeOut = 4000;
    toastr.options.positionClass = 'toast-bottom-right';
  }

  var config = {
    appErrorPrefix: '[Food App Error] ', //Configure the exceptionHandler decorator
    appTitle: 'WMP Review',
    version: '1.0.0'
  };

  core.value('config', config);

  core.config(configure);

  /* @ngInject */
  function configure ($logProvider, $routeProvider, $locationProvider, routehelperConfigProvider, exceptionConfigProvider) {
    // turn debugging off/on (no info or warn)
    if ($logProvider.debugEnabled) {
      $logProvider.debugEnabled(true);
    }

    // Configure the common route provider
    routehelperConfigProvider.config.$routeProvider = $routeProvider;
    routehelperConfigProvider.config.docTitle = 'HotTowel-Angular: ';

    // Configure the common exception handler
    exceptionConfigProvider.config.appErrorPrefix = config.appErrorPrefix;
    $locationProvider.html5Mode(true);
  }

})();