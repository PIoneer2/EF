'use strict';

angular.
module('accountLogin').
component('accountLogin', {
	templateUrl: 'account/account-login/account-login.template.html',
	controller: ['$routeParams', '$scope', 'accountHttpService',
	function AccountLoginController($routeParams, $scope, accountHttpService) {
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
		};
	}]    
});
