'use strict';

angular
.module('transactionsMainApp')
.service('accountHttpService', accountHttpService);

accountHttpService.$inject = ['$http', '$httpParamSerializerJQLike', '$location', '$timeout', '$rootScope'];
function accountHttpService ($http, $httpParamSerializerJQLike, $location, $timeout, $rootScope){ //typeOfService, data, redirectPath, 
this.makePOST = function(typeOfService, data, redirectPath, $scope) { //, $http, $httpParamSerializerJQLike, $location, $timeout
$scope.$ctrl.showServerMessage = true;
var 
URLReg = 'http://localhost/efapi/api/Account/Register',
URLLogin = 'http://localhost/efapi/token',
URL, sendData, header, redirect = redirectPath;

if (typeOfService == 'reg'){
    URL = URLReg;
    sendData = data;
    header = {'Content-Type': 'application/json; charset=utf-8'};
}
else { //typeOfService == 'login'
    URL = URLLogin;
    sendData = $httpParamSerializerJQLike(data);
    header = { 'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8' };
}

return $http({method: 'POST', url: URL, data: sendData, headers: header})
.then(
    function(response) {
      $scope.$ctrl.serverMessage = response.Message;
      if (typeOfService == 'reg'){
        sessionStorage.userName = $scope.$ctrl.email;
      }
      else {
        sessionStorage.userName = response.data.userName;
        sessionStorage.token = response.data.access_token;
        sessionStorage.Id = response.data.Id;
        $rootScope.email = sessionStorage.userName;
        $rootScope.token = sessionStorage.token;
        $rootScope.logged = true;
      }  
      $timeout(function() {
        $scope.$ctrl.showServerMessage = false;
        $location.path(redirect);
    }, 2000);
  },
  function (response) {
    try {
        $scope.$ctrl.serverMessage = response.data.Message;
        $scope.$ctrl.modelErrors = new Array();
        for (var key in response.data.ModelState) {
            for (var i = 0; i < response.data.ModelState[key].length; i++) {
                $scope.$ctrl.modelErrors.push(response.data.ModelState[key][i]);
            }
        }
    }
    catch (error) {
        $scope.$ctrl.serverMessage = error.data;
    }
    finally {
        $timeout(function() {
            $scope.$ctrl.showServerMessage = false;
        }, 5000);
    }
});
    }
    return this;
};