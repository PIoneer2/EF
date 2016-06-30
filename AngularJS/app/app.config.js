'use strict';

angular.
  module('transactionsMainApp').
  config(['$locationProvider' ,'$routeProvider',
    function config($locationProvider, $routeProvider) {
      $locationProvider.hashPrefix('!');

      $routeProvider.
        when('/transactions', {
          template: '<transactions-list></transactions-list>'
        }).
        when('/transactions/detail/:Id', {
          template: '<transactions-detail></transactions-detail>'
        }).
        when('/transactions/delete/:Id', {
          template: '<transactions-delete></transactions-delete>'
        }).
        when('/transactions/createedit/:Id', {
          template: '<transactions-createedit></transactions-createedit>'
        }).
        when('/home', {
          template: '<home-index></home-index>'
        }).
        when('/Account/Login', {
          template: '<account-login></account-login>'
        }).
        when('/Account/Login//:returnUrl', {
          template: '<account-login></account-login>'
        }).
        when('/Account/Login//:returnUrl/:returnAction/:returnId', {
          template: '<account-login></account-login>'
        }).
        when('/Account/Register', {
          template: '<account-register></account-register>'
        }).
        otherwise('/home');
    }
  ]);
