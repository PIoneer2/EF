'use strict';

// Register `transactionsDetail` component, along with its associated controller and template
angular.
module('transactionsDetail').
component('transactionsDetail', {
  templateUrl: 'transactions/transactions-detail/transactions-detail.template.html',
  controller: ['$routeParams', '$http', '$location', '$rootScope', '$timeout',
  function TransactionsDetailController($routeParams, $http, $location, $rootScope, $timeout) {
    var self = this;
    self.Id = $routeParams.Id;
    self.showServerMessage = false;

    self.returnPath = $location.path();
      if ($rootScope.logged == true) {
    self.showServerMessage = true;
    $http({method: 'GET', url: 'http://localhost/efapi/api/Transactions/'+self.Id, headers: {
        "Authorization": "Bearer " + sessionStorage.token
      }})
    .then(
      //success
    function(response) {
      self.transaction = response.data;
      var tmpDateTime = response.data.Date.split('T', 2);
      self.transaction.Date = tmpDateTime[0]+' '+tmpDateTime[1];
      self.showServerMessage = false;
    },
    //fail
    function (response) {
      self.serverMessage = 'Sorry, there are some errors:';
      self.modelErrors = new Array();
      for (var key in response.data) {
        self.modelErrors.push(response.data[key]);
      }
      $timeout(function() {
        //self.showServerMessage = false;
        //$location.path('/transactions');
      }, 5000);
    }
    );

    $http.get('transactions/UserId.json')
    .then(
    function(response) {
      self.UserId = response.data;
    });

    $http.get('transactions/TranactionTypeId.json')
    .then(
    function(response) {
      self.TranactionTypeId = response.data;
    });

    } else {
        $location.path('/Account/Login/' + self.returnPath);
      }
  }]    
});
/*
    $http.get('transactions/ViewBag.json').then(function(response) {
        self.ViewBag = response.data;
      });
      */
    /*
    $http.get('transactions/transaction.json').then(function(response) {
      self.transaction = response.data;
      });//works
      
      if (self.Id == 10017)    {
        $http.get('transactions/transaction1.json').then(function(response) {
          self.transaction = response.data;
        });
      }
      if (self.Id == 10023)    {
        $http.get('transactions/transaction2.json').then(function(response) {
          self.transaction = response.data;
        });
      }*/

      /* working
      $http.get('transactions/UserId.json').then(function(response) {
        self.UserId = response.data;
      });
      $http.get('transactions/TranactionTypeId.json').then(function(response) {
        self.TranactionTypeId = response.data;
      });*/
