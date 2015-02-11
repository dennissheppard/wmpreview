(function(){
  angular.module('foodApp.widgets.loader')
    .directive('loader', function() {
      return {
        restrict: 'EA',
        template: '<div ng-show="show"><i class="fa fa-4x fa-refresh fa-spin"></i></div>',
        controller: function ($scope, LoaderService) {
          $scope.loader = LoaderService;
          $scope.$watch('loader.isVisible', toggleDisplay);

          function toggleDisplay() {
            $scope.show = !!($scope.loader.isVisible);
          }
        }
      };
    });
})();