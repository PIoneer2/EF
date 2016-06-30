'use strict';

// Register `transactionsCreateedit` component, along with its associated controller and template
angular.
module('transactionsCreateedit').
component('transactionsCreateedit', {
  templateUrl: 'transactions/transactions-createedit/transactions-createedit.template.html',
  controller: ['$routeParams', '$http', '$location', '$rootScope', '$timeout',
  function TransactionsCreateeditController($routeParams, $http, $location, $rootScope, $timeout) {
    var self = this;
    self.Id = $routeParams.Id;
    self.serverMessage ='';
    self.returnPath = $location.path();
    if ($rootScope.logged == true) {
      self.showServerMessage = false;
      self.maxLenghtDescription = 255;
      self.dateTimePattern="\\d\\d\\d\\d[-./]?\\d\\d[-./]?\\d\\d \\d\\d[-.:/]\\d\\d[-.:/]\\d\\d";
      self.selectedUserId;

      self.showServerMessage = true;
      $http({method: 'GET', url: 'http://localhost/efapi/api/User', headers: {
        "Authorization": "Bearer " + sessionStorage.token
      }})
      .then(
      //success
      function(response) {
        self.UserId = response.data;
        self.userIdListLoaded = true;
        for (var k in self.UserId) {
          if (self.UserId[k].UserName == $rootScope.email) {
            //$rootScope.Id = self.UserId[k].Id;
            self.selectedUserId = self.UserId[k].Id;
            break;
          }
        }
        self.selectedUser = {
          "Email": $rootScope.email,
          "UserName": $rootScope.email,
          "Id": self.selectedUserId
};
if (self.userIdListLoaded == true && self.tranactionTypeIdListLoaded == true) {self.showServerMessage = false;}
},
    //fail
    function (response) {
      self.serverMessage = 'Error in loading User list';
    }
    );

      $http({method: 'GET', url: 'http://localhost/efapi/api/TranactionType', headers: {
        "Authorization": "Bearer " + sessionStorage.token
      }})
      .then(
      //success
      function(response) {
        self.TranactionTypeId = response.data;
        self.tranactionTypeIdListLoaded = true;
        self.selectedTransType = {
          "Id": 1,
          "Name": "Supply"
        };
        if (self.userIdListLoaded == true && self.tranactionTypeIdListLoaded == true) {self.showServerMessage = false;}
      },
    //fail
    function (response) {
      self.serverMessage = 'Error in loading Transaction Type list';
    }
    );

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
self.showServerMessage = true;
$http({method: 'GET', url: 'http://localhost/efapi/api/Transactions/'+self.Id, headers: {
  "Authorization": "Bearer " + sessionStorage.token
}})
.then(
      //success
      function(response) {
        self.transaction = response.data;
        var tmpDateTime = response.data.Date.split('T', 2);
        self.transaction.Date = tmpDateTime[0] + ' ' + tmpDateTime[1];
        self.selectedUser = response.data.Users;
        self.selectedTransType = response.data.TranactionType;
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
}

self.sendToServer = function () {
  self.data = {
    "Description": self.transaction.Description, 
    "TranactionTypeId": self.selectedTransType.Id, 
    "UserId": self.selectedUser.Id, 
    "Date": self.transaction.Date, 
    "Id": self.Id
  };
//POST
self.showServerMessage = true;
if (self.Id == 0) {
  $http({method: 'POST', url: 'http://localhost/efapi/api/Transactions/', data: self.data, 
    headers: {
      "Authorization": "Bearer " + sessionStorage.token
    }
  })
  .then(function(response) {
    self.serverMessage = "New transaction saved. Make new one?..";
    $timeout(function() {
      self.showServerMessage = false;
      $location.path('/transactions');
    }, 2000);
  },
  function (response) {
    try {
      self.serverMessage = response.data.Message;
      self.modelErrors = new Array();
      for (var key in response.data.ModelState) {
        for (var i = 0; i < response.data.ModelState[key].length; i++) {
          self.modelErrors.push(response.data.ModelState[key][i]);
        }
        $timeout(function() {
          self.showServerMessage = false
        }, 5000);
      }
    }
    catch (err) {
      self.serverMessage = response.statusText;
    }
    finally {
      $timeout(function() {
        self.showServerMessage = false
      }, 5000);
    }
  });
}

//PUT+Id
else {
  $http({method: 'PUT', url: 'http://localhost/efapi/api/Transactions/', data: self.data, 
    headers: {
      "Authorization": "Bearer " + sessionStorage.token
    }
  })
  .then(function(response) {
    self.serverMessage = "Transaction updated. Make new one?..";
    $timeout(function() {
      self.showServerMessage = false;
      $location.path('/transactions');
    }, 2000);
  },
  function (response) {
    try {
      self.serverMessage = response.data.Message;
      self.modelErrors = new Array();
      for (var key in response.data.ModelState) {
        for (var i = 0; i < response.data.ModelState[key].length; i++) {
          self.modelErrors.push(response.data.ModelState[key][i]);
        }
        $timeout(function() {
          self.showServerMessage = false;
        }, 5000);
      }
    }
    catch (err) {
      self.serverMessage = 'Browser is not able to update transaction';
      $timeout(function() {
        self.showServerMessage = false;
      }, 5000);
    }
  });
}
}
} else {
  $location.path('/Account/Login/' + self.returnPath);
}
}]    
});
