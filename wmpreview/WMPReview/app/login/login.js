(function(){

  angular.module('foodApp.login')
  .controller('Login', Login);

  Login.$inject = ['$scope'];

  function Login($scope){
    /*jshint validthis: true */
    var vm = this;

    activate();


    // DOM events
    vm.submitLogin = SubmitLogin;

    ///////

    function activate(){
      /*
       * any startup code goes here
       */

      // fire an event up the chain to hide the navbar
      // this should get caught in the shell controller
      $scope.$emit('hideNav');
    }

    function SubmitLogin(){
      AccountService.Login(vm.username, vm.password)
        .then(function(response){
          //response should have the AccountModel with a token saved on it
          //we'll save the token to a cookie or local storage
          //and it should also be available on the AccountModel
          vm.accountModel = response;

        },
        function(error){
          vm.accountModel.error = error;
        });
    }

  }




})();