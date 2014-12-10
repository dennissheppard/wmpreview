(function(){

  angular.module('foodApp.login', [/*'services.account', 'models.account'*/])
  .controller('Login', Login);

  Login.$inject = ['AccountService', 'AccountModel']

  function Login(AccountService, AccountModel){
    var vm = this;
    vm.accountInfo = AccountModel;


    vm.submitLogin = SubmitLogin;


    ///////


    function SubmitLogin(){
      AccountService.Login(vm.username, vm.password).then(function(response){
        alert(response);
      });
    }

  }




})();