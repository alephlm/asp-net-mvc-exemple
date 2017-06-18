function customerFactory($http) {

	var url = 'api/customer/';

	return {
		getCustomers : function() {
			return $http.get(url);
		},
		getCustomer : function(id) {
			return $http.get(url + id);
		},
		createCustomer : function(customer) {
			return $http.post(url, customer);
		},
		deleteCustomer : function(id) {
			return $http.delete(url + id);
		},
        generateInvoice : function(customerId) {
            return $http.get(url + 'newinvoice/' + customerId);
        }
	}
};

function parkingFactory($http) {

	var url = 'api/parking/';

	return {
		getParkings : function() {
			return $http.get(url);
		},
		getParking : function(id) {
			return $http.get(url + id);
		},
		createParking : function(parking) {
			return $http.post(url, parking);
		},
		deleteParking : function(id) {
			return $http.delete(url + id);
		}
	}
};

function parkedFactory($http) {

	var url = 'api/parked/';

	return {
		getParked : function(id) {
			return $http.get(url + id);
		},
		createParked : function(parked) {
			return $http.post(url, parked);
		}
	}
};

angular
  .module('app')
  .factory('customerFactory', customerFactory)
  .factory('parkingFactory', parkingFactory)
  .factory('parkedFactory', parkedFactory);