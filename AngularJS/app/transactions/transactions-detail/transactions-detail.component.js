'use strict';

// Register `transactionsDetail` component, along with its associated controller and template
angular.
module('transactionsDetail').
component('transactionsDetail', {
  templateUrl: 'transactions/transactions-detail/transactions-detail.template.html',
  controller: ['$routeParams', '$http', '$location', '$rootScope', '$timeout', 'transactionHttpService', '$scope',
  function TransactionsDetailController($routeParams, $http, $location, $rootScope, $timeout, transactionHttpService, $scope) {
    var self = this;
    self.Id = $routeParams.Id;
    self.showServerMessage = false;
    self.returnPath = $location.path();
    if ($rootScope.logged == true) {
      transactionHttpService.makeTransactionReqest('getOne', {}, '', $scope);
    } else {
      $location.path('/Account/Login/' + self.returnPath);
    }
  }]    
});