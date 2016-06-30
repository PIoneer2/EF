'use strict';

// Register `transactionsList` component, along with its associated controller and template
angular.
module('transactionsList').
component('transactionsList', {
  templateUrl: 'transactions/transactions-list/transactions-list.template.html',
  controller: ['$http', '$location', '$rootScope', '$timeout', 
  function TransactionsListController($http, $location, $rootScope, $timeout) {
    var self = this;
    self.orderProp = 'Id';
    self.returnPath = $location.path();
    self.showServerMessage = false;
    if ($rootScope.logged == true) {
      self.showServerMessage = true;
    	$http({method: 'GET', url: 'http://localhost/efapi/api/Transactions', headers: {
    		"Authorization": "Bearer " + sessionStorage.token
    	}})
    	.then(
    //succeess
    function(response) {
        self.transactions = response.data;
        var tmpDateTime;
      for (var t in self.transactions) {
        tmpDateTime = self.transactions[t].Date.split('T', 2);
        self.transactions[t].Date = tmpDateTime[0]+' '+tmpDateTime[1];
      }
        self.showServerMessage = false
         if (self.transactions.length == 0) {
          //self.showServerMessage = true;
          self.serverMessage = 'Sorry, no data to display!';
          $timeout(function() {
           self.showServerMessage = false
         }, 5000);        	
        }
      },
    //fail
    function (response) {
    	//self.showServerMessage = true;
    	self.serverMessage = 'Sorry, there are some errors:';
    	self.modelErrors = new Array();
      for (var key in response.data) {
        self.modelErrors.push(response.data[key]);
      }
      $timeout(function() {
        self.showServerMessage = false
      }, 5000);
    }
    );
    } else {
     $location.path('/Account/Login/' + self.returnPath);
   }
 }]
});
