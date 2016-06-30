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
  function ($location, $window, $rootScope, $http) {
    this.$inject = ['$location', '$window', '$rootScope', '$http'];
    var self = this;
    $rootScope.logged = false;
    $rootScope.storedTransaction = {
      Date: '',
      Description: '',
      GoodsInTransaction: [],
      Id: 0,
      TranactionType: {},
      TranactionTypeId: 0,
      UserId: 0,
      Users: {}
    };
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
      if (($rootScope.email != '') && ($rootScope.token != '') && ($rootScope.logged != false)){
      //API call
      $http({method: 'POST', url: 'http://localhost/efapi/api/Account/Logout', headers: {
        "Authorization": "Bearer " + sessionStorage.token
      }})
      .then(
        function(response) {
          sessionStorage.removeItem('userName');
          sessionStorage.removeItem('token');
          $rootScope.email = '';
          $rootScope.token = '';
          $rootScope.logged = false;
          $location.path('/home');
        },
      //fail
      function (response) {
        self.showServerMessage = true;
        self.serverMessage = 'Sorry, can`t logoff:';
        self.modelErrors = new Array();
        for (var key in response.data) {
          self.modelErrors.push(response.data[key]);
        }
        $timeout(function() {
          self.showServerMessage = false;
        }, 5000);
      }
      );
      }
      else {
        sessionStorage.removeItem('userName');
          sessionStorage.removeItem('token');
          $rootScope.email = '';
          $rootScope.token = '';
          $rootScope.logged = false;
          $location.path('/home');
      }
    }
    //--------------- end logoff
    //--------------- start loadTransaction
    $rootScope.loadTransaction = function ($http, transaction) {
      if ($rootScope.storedTransaction.Id == transaction.Id) {
        transaction = angular.copy($rootScope.storedTransaction);
        return transaction;
      }
      //надо грузить
      else {
        $http({method: 'GET', url: 'http://localhost/efapi/api/Transactions/' + transaction.Id, headers: {
          "Authorization": "Bearer " + sessionStorage.token
        }})
        .then(
      //success
      function(response) {
        transaction = response.data;
        var tmpDateTime = response.data.Date.split('T', 2);
        transaction.Date = tmpDateTime[0] + ' ' + tmpDateTime[1];
        $rootScope.storedTransaction = angular.copy(transaction);
        self.showServerMessage = false
        return transaction;
      },
      //fail
      function (response) {
        self.serverMessage = 'Sorry, there are some errors:';
        self.modelErrors = new Array();
        for (var key in response.data) {
          self.modelErrors.push(response.data[key]);
        }
        $timeout(function() {
          self.showServerMessage = false;
          $location.path('/transactions');
        }, 5000);
      }
      );
      }
    }
    //--------------- end loadTransaction
  });


/*

function TransactionsMainAppController($routeParams) {
    var self = this;
    self.logged = false;
    if (sessionStorage.userName != null){
      self.email = sessionStorage.userName;
    }
    if (sessionStorage.token != null){
      self.token = sessionStorage.token;
      self.logged = true;
    }
    console.log('---------------------------------------');
    console.log('/home: Storage.userName = ', sessionStorage.userName);
    console.log('/home: Storage.token = ', sessionStorage.token);
  }

*/