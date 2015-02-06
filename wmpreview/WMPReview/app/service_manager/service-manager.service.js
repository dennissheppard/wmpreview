(function(){

  angular.module('foodApp.serviceManager')
    .factory('ServiceManager', ServiceManager);
  //TODO: Remove AccountManager once login exists
  ServiceManager.$inject = ['$http', 'apiConstants', 'LoaderService'];

  function ServiceManager($http, apiConstants, LoaderService){
    return{
      Get: get,
      Post: post,
      Delete: remove
    };


    ////////////////

    function get(serviceUrl){
      LoaderService.show();
      var promise = $http.get(apiConstants.stageApi + serviceUrl);
      promise.then(function(){
        LoaderService.hide();
      }, function(){
        LoaderService.hide();
      })
      return promise;
    }

    function post(serviceUrl, data){
      LoaderService.show();
      var promise = $http.post(apiConstants.stageApi + serviceUrl, data);
      promise.then(function(){
        LoaderService.hide();
      }, function(){
        LoaderService.hide();
      });
      return promise;
    }

    function remove(serviceUrl){
      LoaderService.show();
      var promise = $http.delete(apiConstants.stageApi + serviceUrl);
      promise.then(function(){
        LoaderService.hide();
      }, function(){
        LoaderService.hide();
      });
      return promise;
    }

  }




})();