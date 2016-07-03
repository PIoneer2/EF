'use strict';

// Register `transactionsList` component, along with its associated controller and template
angular.
module('transactionsList').
component('transactionsList', {
  templateUrl: 'transactions/transactions-list/transactions-list.template.html',
  controller: ['$http', '$location', '$rootScope', '$timeout', 'transactionHttpService', '$scope',
  function TransactionsListController($http, $location, $rootScope, $timeout, transactionHttpService, $scope) {
    var self = this;
    self.orderProp = 'Id';
    self.returnPath = $location.path();
    self.showServerMessage = false;
    if ($rootScope.logged == true) {
      transactionHttpService.makeTransactionReqest('getMany', {}, '', $scope);
    } 
    else {
     $location.path('/Account/Login/' + self.returnPath);
   }
 }]
});
