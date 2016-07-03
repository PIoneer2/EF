'use strict';

angular
.module('transactionsMainApp')
.service('transactionHttpService', transactionHttpService);

transactionHttpService.$inject = ['$http', '$location', '$timeout', '$rootScope', '$routeParams'];
function transactionHttpService ($http, $location, $timeout, $rootScope, $routeParams){
  var header = {'Authorization': 'Bearer ' + sessionStorage.token};
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

//---------start makeTransactionReqest
this.makeTransactionReqest = function(typeOfService, data, redirectPath, $scope) {
  $scope.$ctrl.showServerMessage = true;
//----------- start repo check
if (($rootScope.storedTransaction.Id == $scope.$ctrl.Id) && (typeOfService == ('getOne'))){
  $scope.$ctrl.transaction = angular.copy($rootScope.storedTransaction);
  $scope.$ctrl.showServerMessage = false;
}
//----------- end repo check
else { //make a request
//---------- start setting parameters for transactions request
var URL = 'http://localhost/efapi/api/Transactions/';
var selectedMethod = 'GET';
var sendData = {};
if (typeOfService == 'getMany'){

}
if (typeOfService == 'getOne'){
  URL += $scope.$ctrl.Id;
}
if (typeOfService == 'update'){
  selectedMethod = 'PUT';
  sendData = data;
}
if (typeOfService == 'create'){
  selectedMethod = 'POST';
  sendData = data;
}
if (typeOfService == 'delete'){
  selectedMethod = 'DELETE';
  URL += $scope.$ctrl.Id;
}
//---------- end setting parameters for transactions request
//----- start request
return $http({method: selectedMethod, url: URL, data: sendData, headers: header})
.then(
  function(response) {
    if (typeOfService == 'getMany'){
      $scope.$ctrl.transactions = response.data;
      var tmpDateTime;
      for (var t in $scope.$ctrl.transactions) {
        tmpDateTime = $scope.$ctrl.transactions[t].Date.split('T', 2);
        $scope.$ctrl.transactions[t].Date = tmpDateTime[0]+' '+tmpDateTime[1];
      }
      $scope.$ctrl.showServerMessage = false;
      if ($scope.$ctrl.transactions.length == 0) {
        $scope.$ctrl.showServerMessage = true;
        $scope.$ctrl.serverMessage = 'Sorry, no data to display!';
        $timeout(function() {
         $scope.$ctrl.showServerMessage = false
       }, 5000);          
      }
    }

    if (typeOfService == 'getOne'){
      $scope.$ctrl.transaction = response.data;
      var tmpDateTime = response.data.Date.split('T', 2);
      $scope.$ctrl.transaction.Date = tmpDateTime[0]+' '+tmpDateTime[1];
      $rootScope.storedTransaction = angular.copy($scope.$ctrl.transaction);
      $scope.$ctrl.showServerMessage = false;
    }

    if (typeOfService == 'update'){
      $rootScope.storedTransaction = angular.copy($scope.$ctrl.transaction);
      $scope.$ctrl.serverMessage = "Transaction updated. Make new one?..";
      $timeout(function() {
        $scope.$ctrl.showServerMessage = false;
        $location.path(redirectPath);
      }, 2000);
    }

    if (typeOfService == 'create'){
      $scope.$ctrl.serverMessage = "New transaction saved. Make new one?..";
      $timeout(function() {
        $scope.$ctrl.showServerMessage = false;
        $location.path(redirectPath);
      }, 2000);
    }

    if (typeOfService == 'delete'){
      $scope.$ctrl.transaction = response.data;
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
      $scope.$ctrl.serverMessage = 'Successful delete, redirecting...';
      $scope.$ctrl.showServerMessage = false;
      $location.path(redirectPath);
    }
  },
  function (response) {
    try {
      $scope.$ctrl.serverMessage = response.data.Message;
      $scope.$ctrl.modelErrors = new Array();
      for (var key in response.data.ModelState) {
        for (var i = 0; i < response.data.ModelState[key].length; i++) {
          $scope.$ctrl.modelErrors.push(response.data.ModelState[key][i]);
        }
      }
    }
    catch (error) {
      $scope.$ctrl.serverMessage = error.data;
    }
    finally {
      $timeout(function() {
        $scope.$ctrl.showServerMessage = false;
      }, 5000);
    }
  });
//----- end request

}
}
//---------end makeTransactionReqest
//---------start makeOptionsListReqest
this.makeOptionsListReqest = function(listName, $scope) {
  var 
  URLUser = 'http://localhost/efapi/api/User',
  URLTranactionType = 'http://localhost/efapi/api/TranactionType';

  $scope.$ctrl.showServerMessage = true;

//---------start User list
if (listName == 'User'){

  return $http({method: 'GET', url: URLUser, headers: header})
  .then(
      //success
      function(response) {
        $scope.$ctrl.UserId = response.data;
        $scope.$ctrl.userIdListLoaded = true;
        if ($rootScope.Id != null) {
          $scope.$ctrl.selectedUserId = $rootScope.Id;
        }
        else {
          for (var k in $scope.$ctrl.UserId) {
            if ($scope.$ctrl.UserId[k].UserName == $rootScope.email) {
              sessionStorage.Id = $scope.$ctrl.UserId[k].Id;
              $rootScope.Id = $scope.$ctrl.UserId[k].Id;
              $scope.$ctrl.selectedUserId = $scope.$ctrl.UserId[k].Id;
              break;
            }
          }
        }
        $scope.$ctrl.selectedUser = {
          "Email": $rootScope.email,
          "UserName": $rootScope.email,
          "Id": $scope.$ctrl.selectedUserId
        };
        if ($scope.$ctrl.userIdListLoaded == true && $scope.$ctrl.tranactionTypeIdListLoaded == true) {$scope.$ctrl.showServerMessage = false;}
      },
    //fail
    function (response) {
      $scope.$ctrl.serverMessage = 'Error in loading User list';
    }
    );
}
//---------end User list
//---------start TranactionType list
        else { //listName == 'TranactionType'
          if ($scope.$ctrl.TranactionTypeId === undefined) {
            return $http({method: 'GET', url: URLTranactionType, headers: header})
            .then(
              function(response) {
                $scope.$ctrl.TranactionTypeId = response.data;
                $scope.$ctrl.tranactionTypeIdListLoaded = true;
                $scope.$ctrl.selectedTransType = {
                  "Id": 1,
                  "Name": "Supply"
                };
                if ($scope.$ctrl.userIdListLoaded == true && $scope.$ctrl.tranactionTypeIdListLoaded == true) {$scope.$ctrl.showServerMessage = false;}
              },
              function (response) {
                $scope.$ctrl.serverMessage = 'Error in loading Transaction Type list';
              }
              );
          }
          else {
            if ($scope.$ctrl.userIdListLoaded == true && $scope.$ctrl.tranactionTypeIdListLoaded == true) {$scope.$ctrl.showServerMessage = false;}
          }
        }
//---------end TranactionType list
}
    //---------end makeOptionsListReqest
    return this;
  };