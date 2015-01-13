(function(){

  angular.module('foodApp.serviceManager')
    .factory('ServiceManager', ServiceManager);
  //TODO: Remove AccountManager once login exists
  ServiceManager.$inject = ['$http', 'apiConstants', 'LoaderService'];

  function ServiceManager($http, apiConstants, LoaderService){
    return{
      Get: get,
      Post: post,
      Delete: remove,
      Initialize: initialize
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

    function initialize(){
      this.token = 'bearer Mn8-bYp_pPWetq7pPVfREy6lltLsOE_di4tNL9bynQe3FagSsKOLCbBadwvmJ4OIRrpOs7XHelV0xGAg55HWp8zmqTjV1fzCXiu21y9Io65afa8VRZITWWKrX0tOyrp3rtlcvMOuyTGSeJNA4HdkwrdkT6fS2-tpSbrybfhUDOOPJW1IzlKudoBaHOMG4b1DUgwv_eKvs56jCsC1kirHBZ001B2YrlU1j_5-S4DX_sTwbsBiztx4VFBAw4qqARCnI1eGJBnEn-SNSeZ_fuP1D1VhrnysBjINGvYJyeb4gCs4r1qg9Nk_MTHeakmRYe8cDtdlAuH4TERBrIgbRKRiuUk2Y2IYMHppICdcuEM3QWzadbiRhcJqE6gU6Okv9QQC6k4AwcCrJxdtnRyt-WRIMeAEhbKW79z1eyaeofcWAGycFF7-mHXchTU3BdbOMkohjL542_I8cy6_z469gSUofn8vbNEPRDj1-C4dRgEETQimaitpjzaGXiokj1nmSH3nKe3DkSMjbu8bFCZzMD9O4LaNAqskwmJl7ImRCQnYUlr9nM6maCDmOTawmY0sWRQu';
      $http.defaults.headers.common['Authorization']= this.token;
      $http.defaults.headers.common['X-Tenant']= apiConstants.xtenant;
    }
  }




})();