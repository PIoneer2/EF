'use strict';

// Register `transactionsDelete` component, along with its associated controller and template
angular.
module('transactionsDelete').
component('transactionsDelete', {
  templateUrl: 'transactions/transactions-delete/transactions-delete.template.html',
  controller: ['$routeParams', '$http', '$location', '$rootScope',
  function TransactionsDeleteController($routeParams, $http, $location, $rootScope) {
    var self = this;
    self.Id = $routeParams.Id;
    self.returnPath = $location.path();
    if ($rootScope.logged == true) {
      self.showServerMessage = true;

      //TO DO
      //self.transaction = new Object ();
      //self.transaction.Id = $routeParams.Id;
      //self.transaction = $rootScope.loadTransaction($http, self.transaction);
      //------------ start GET
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
        self.showServerMessage = false;
        $location.path('/transactions');
      }, 5000);
    }
    );
    //------------ end GET
//------------ start toDelete
self.toDelete = function () {
  $http({method: 'DELETE', url: 'http://localhost/efapi/api/Transactions/' + self.Id, headers: {
    "Authorization": "Bearer " + sessionStorage.token
  }})
  .then(
    //success
    function(response) {
      self.transaction = response.data;
      self.showServerMessage = true;
      self.serverMessage = 'Successful delete, redirecting...';
      self.showServerMessage = false;
      $location.path('/transactions');
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
      //------------ end toDelete
    } 
    else {
      $location.path('/Account/Login/' + self.returnPath);
    }

  }]
});
