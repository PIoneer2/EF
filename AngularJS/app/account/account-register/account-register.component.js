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
      //self.showServerMessage = true;
      self.data = {
        Email: self.email,
        Password: self.password, 
        ConfirmPassword: self.password
      };
      accountHttpService.makePOST('reg', self.data, '/Account/Login/', $scope); //, $http, $httpParamSerializerJQLike, $location, $timeout

      //return 
      /*
      $http({method: 'POST', url: 'http://localhost/efapi/api/Account/Register', data: self.data})
      .then(function(response) {
        if (response.status == 200) {
          self.serverMessage = "User is registered, redirecting ...";
          sessionStorage.userName = self.email;
          $timeout(function() {
          $location.path('/Account/Login/');
        }, 2000);
        } else {
          self.serverMessage = response.Message;
        }
        $timeout(function() {
          self.showServerMessage = false
        }, 5000);
      },
      function (response) {
        try {
        self.serverMessage = response.data.Message;
        self.modelErrors = new Array();
        for (var key in response.data.ModelState) {
                for (var i = 0; i < response.data.ModelState[key].length; i++) {
                    self.modelErrors.push(response.data.ModelState[key][i]);
                }
                $timeout(function() {
                self.showServerMessage = false
               }, 5000);
            }
            }
            catch (err) {
              self.serverMessage = 'Browser is not supporting CORS';
              $timeout(function() {
                self.showServerMessage = false
               }, 5000);
            }
        
      });*/
  };
}]
});