'use strict';

// Define the `transactionsMainApp` module
angular.module('transactionsMainApp', [
  // ...which depends on the modules
  'ngRoute',
  'transactionsList',
  'transactionsDetail',
  'transactionsDelete',
  'transactionsCreateedit',
  'homeIndex',
  'accountRegister',
  'accountLogin'
  ])
.controller ('TransactionsMainAppController', 
  function ($location, $window, $rootScope, $http, accountHttpService, $scope) {
    this.$inject = ['$location', '$window', '$rootScope', '$http', 'accountHttpService', '$scope'];
    var self = this;
    $rootScope.logged = false;
    if ($rootScope.Id != null){
      $rootScope.Id = sessionStorage.Id;
    }
    if ((sessionStorage.userName != null)&&(sessionStorage.token != null)){
      $rootScope.email = sessionStorage.userName;
      $rootScope.token = sessionStorage.token;
      $rootScope.logged = true;
    }
    else {
      self.logoff;
    }
    //--------------- start logoff
    self.logoff = function () {
      accountHttpService.makeLogoff($scope);
    }
    //--------------- end logoff
  });