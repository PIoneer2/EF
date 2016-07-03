'use strict';

angular
.module('transactionsMainApp')
.service('accountHttpService', accountHttpService);

accountHttpService.$inject = ['$http', '$httpParamSerializerJQLike', '$location', '$timeout', '$rootScope'];
function accountHttpService ($http, $httpParamSerializerJQLike, $location, $timeout, $rootScope){

this.makeLogoff = function($scope) {
      return $http({method: 'POST', url: 'http://localhost/efapi/api/Account/Logout', 
        headers: { "Authorization": "Bearer " + sessionStorage.token }})
      .then(
        function(response) {
            sessionStorage.removeItem('userName');
            sessionStorage.removeItem('token');
            sessionStorage.removeItem('Id');
            $rootScope.email = '';
            $rootScope.token = '';
            $rootScope.logged = false;
            $rootScope.Id = '';
            $location.path('/home');
        },
        function (response) {
            $scope.$ctrl.showServerMessage = true;
            $scope.$ctrl.serverMessage = 'There were some errors with login:';
            $scope.$ctrl.modelErrors = new Array();
            for (var key in response.data) {
              $scope.$ctrl.modelErrors.push(response.data[key]);
            }
          $timeout(function() {
              $scope.$ctrl.showServerMessage = false;
          }, 5000);
      }
      );
  }

this.makePOST = function(typeOfService, data, redirectPath, $scope) { 
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
      if (response.data.Message !== undefined){
        $scope.$ctrl.modelErrors = new Array();
        $scope.$ctrl.serverMessage = response.data.Message;
        for (var key in response.data.ModelState) {
          for (var i = 0; i < response.data.ModelState[key].length; i++) {
            $scope.$ctrl.modelErrors.push(response.data.ModelState[key][i]);
          }
        }
      }
      else {
        $scope.$ctrl.serverMessage = response.statusText;
        $scope.$ctrl.modelErrors = new Array();
        for (var key in response.data) {
          $scope.$ctrl.modelErrors.push(response.data[key]);
        }
        $timeout(function() {
          $scope.$ctrl.showServerMessage = false;
        }, 2000);
      }
    }
    catch (error) {
      $scope.$ctrl.serverMessage = error.data
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