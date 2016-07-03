'use strict';

// Register `transactionsDelete` component, along with its associated controller and template
angular.
module('transactionsDelete').
component('transactionsDelete', {
  templateUrl: 'transactions/transactions-delete/transactions-delete.template.html',
  controller: ['$routeParams', '$http', '$location', '$rootScope', '$scope', 'transactionHttpService',
  function TransactionsDeleteController($routeParams, $http, $location, $rootScope, $scope, transactionHttpService) {
    var self = this;
    self.Id = $routeParams.Id;
    self.returnPath = $location.path();
    if ($rootScope.logged == true) {
      transactionHttpService.makeTransactionReqest('getOne', {}, '', $scope);
      self.toDelete = function () {
        transactionHttpService.makeTransactionReqest('delete', {}, '/transactions', $scope);
      }
    } 
    else {
      $location.path('/Account/Login/' + self.returnPath);
    }
  }]
});
