'use strict';

angular.
module('accountRegister').
component('accountRegister', {
  templateUrl: 'account/account-register/account-register.template.html',
  controller: ['$routeParams', '$http', '$scope', '$location', '$timeout', '$httpParamSerializerJQLike', 'accountHttpService',
  function AccountRegisterController($routeParams, $http, $scope, $location, $timeout, $httpParamSerializerJQLike, accountHttpService) {
    var self = this;
    self.email, self.password, self.serverMessage, self.showServerMessage = false;
    self.minLenghtPassword = 1;
    self.maxLenghtPassword = 255;
    self.maxLenghtEmail = 255;

    self.submit = function(){
      self.data = {
        Email: self.email,
        Password: self.password, 
        ConfirmPassword: self.password
      };
      accountHttpService.makePOST('reg', self.data, '/Account/Login/', $scope);
    };
  }]
});