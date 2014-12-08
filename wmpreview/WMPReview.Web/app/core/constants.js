/* global toastr:false, moment:false */
(function () {
  'use strict';

  angular
    .module('foodApp.core')
    .constant('toastr', toastr)
    .constant('moment', moment);
})();