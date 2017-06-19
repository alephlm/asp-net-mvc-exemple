(function () {
	angular
		.module('app', ['ngRoute', 'ngCookies', 'ngAnimate', 'toastr'])
		.config(function (toastrConfig, $routeProvider, $locationProvider) {
			angular.extend(toastrConfig, {
				positionClass: 'toast-bottom-right',
			});

			$routeProvider
				.when('/', {
					controller: 'customerController',
					templateUrl: 'views/index.html',
					controllerAs: 'vm'
				})

				.when('/parkinginfo', {
					controller: 'customerController',
					templateUrl: 'views/parkingInfo.html',
					controllerAs: 'vm'
				})

				.when('/invoices', {
					controller: 'customerController',
					templateUrl: 'views/invoice.html',
					controllerAs: 'vm'
				})

				.when('/parked', {
					controller: 'parkedController',
					templateUrl: 'views/parked.html',
					controllerAs: 'vm'
				})

				.otherwise({ redirectTo: '/' });
		})
		.run(function ($rootScope, $cookies) {
			$rootScope.globals = $cookies.getObject('globals') || {};
		});
})();
