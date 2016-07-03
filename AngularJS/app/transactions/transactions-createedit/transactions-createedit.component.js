'use strict';

// Register `transactionsCreateedit` component, along with its associated controller and template
angular.
module('transactionsCreateedit').
component('transactionsCreateedit', {
  templateUrl: 'transactions/transactions-createedit/transactions-createedit.template.html',
  controller: ['$routeParams', '$http', '$location', '$rootScope', '$timeout', '$scope', 'transactionHttpService',
  function TransactionsCreateeditController($routeParams, $http, $location, $rootScope, $timeout, $scope, transactionHttpService) {
    var self = this;
    self.Id = $routeParams.Id;
    self.serverMessage ='';
    self.returnPath = $location.path();
    if ($rootScope.logged == true) {
      self.showServerMessage = false;
      self.maxLenghtDescription = 255;
      self.dateTimePattern="\\d\\d\\d\\d[-./]?\\d\\d[-./]?\\d\\d \\d\\d[-.:/]\\d\\d[-.:/]\\d\\d";
      self.selectedUserId;
      transactionHttpService.makeOptionsListReqest('TranactionType', $scope)
      .then(
        transactionHttpService.makeOptionsListReqest('User', $scope)
        )
      .then( 
        function () {
          if (self.Id == 0) {
//filling blank
self.transaction = new Object();
var tmpDateTime1 = new Date().toISOString();
var tmpDateTime2 = tmpDateTime1.split('.', 2);
var tmpDateTime = tmpDateTime2[0].split('T', 2);
self.transaction.Date = tmpDateTime[0]+' '+tmpDateTime[1];

}
else {
//GET
transactionHttpService.makeTransactionReqest('getOne', {}, '', $scope);
}}
)
      self.sendToServer = function () {
        self.data = {
          "Description": self.transaction.Description, 
          "TranactionTypeId": self.selectedTransType.Id, 
          "UserId": self.selectedUser.Id, 
          "Date": self.transaction.Date, 
          "Id": self.Id
        };
//POST
if (self.Id == 0) {
  transactionHttpService.makeTransactionReqest('create', self.data, '/transactions', $scope);
}
//PUT+Id
else {
  transactionHttpService.makeTransactionReqest('update', self.data, '/transactions', $scope);
}
}
} 
else {
  $location.path('/Account/Login/' + self.returnPath);
}
}]    
});
