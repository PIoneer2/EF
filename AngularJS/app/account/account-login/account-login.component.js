'use strict';

angular.
module('accountLogin').
component('accountLogin', {
	templateUrl: 'account/account-login/account-login.template.html',
	controller: ['$routeParams', '$http', '$scope', '$location', '$rootScope', '$timeout', '$httpParamSerializerJQLike', 'accountHttpService',
	function AccountLoginController($routeParams, $http, $scope, $location, $rootScope, $timeout, $httpParamSerializerJQLike, accountHttpService) {
		var self = this;
		self.showServerMessage = false;
		if ($routeParams.returnUrl != null){
			self.returnPath = $routeParams.returnUrl;
		} else {
			self.returnPath = '/home';
		}

		if ($routeParams.returnAction != null){
			self.returnAction = '/' + $routeParams.returnAction;
		} else {
			self.returnAction = '';
		}

		if ($routeParams.returnId != null){
			self.returnId = '/' + $routeParams.returnId;
		} else {
			self.returnId = '';
		}
		if (sessionStorage.userName != null){
			self.email = sessionStorage.userName;
		}
		self.minLenghtPassword = 1;
		self.maxLenghtPassword = 255;
		self.maxLenghtEmail = 255;
		self.password;
		self.submit = function(){
			self.data = {
				grant_type: 'password',
				username: self.email,
				password: self.password
			};
		accountHttpService.makePOST('login', self.data, self.returnPath + self.returnAction + self.returnId, $scope);
/*
			$http({
				method: 'POST', url: 'http://localhost/efapi/token', 
				data: $httpParamSerializerJQLike(self.data), 
				headers: {
					'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'
				}
			})
			.then(function(response) {
				if (response.status == 200) {
					self.regMessage = "User sucsessfuly logged! Redirecting ...";
					sessionStorage.userName = response.data.userName;
					sessionStorage.token = response.data.access_token;
					sessionStorage.Id = response.data.Id;
					$rootScope.email = sessionStorage.userName;
					$rootScope.token = sessionStorage.token;
					$rootScope.logged = true;
					$location.path(self.returnPath + self.returnAction + self.returnId);
				} 
				else {
					self.regMessage = response.message;
				}
				$timeout(function() {
					self.showServerMessage = false
				}, 5000);
			},
			function (response) {
				self.modelErrors = new Array();
				for (var key in response.data) {
					self.modelErrors.push(response.data[key]);
				}
				$timeout(function() {
					self.showServerMessage = false
				}, 5000);
			});*/


		};
	}]    
});
