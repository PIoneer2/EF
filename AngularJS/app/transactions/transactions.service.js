'use strict';

angular
   .module('transactionsMainApp')
   .service('dataTransferService', dataTransferService);


function dataTransferService ($http, URL, Id){
this.$inject = ['$http'];

    var BASE_URL = URL; 

    //service
    this.hello = function() {
        return "Hello World";
    };

    //factory
    return { 
        all: function() { 
            return $http.get(BASE_URL); 
        }, 
        create: function(client) { 
            return $http.post(BASE_URL, client); 
        }, 
        update: function(client) { 
            return $http.put(BASE_URL + '/' + client.id, client); 
        }, 
        delete: function(id) { 
            return $http.delete(BASE_URL + '/' + id); 
        } 
    }; 




};