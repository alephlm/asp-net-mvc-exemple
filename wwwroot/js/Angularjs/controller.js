function customerController($location, $rootScope, $cookies, customerFactory, toastr) {
    var vm = this;
    vm.customers = [];
    vm.customer = {};
    vm.invoice = {};

    vm.getCustomers = function () {
        customerFactory.getCustomers()
            .then(function (customers) {
                vm.customers = customers.data;
                $rootScope.globals.customers = customers.data;
                if(!customers.data.length){
                    $rootScope.globals.customer = "";
                }
                var cookieExp = new Date();
                cookieExp.setDate(cookieExp.getDate() + 7);
                $cookies.putObject('globals', $rootScope.globals, { expires: cookieExp });
            }).catch(function (error) {
                toastr.error('Error getting customers!', 'Error!');
            });
    }

    vm.saveCustomer = function (carro) {
        customerFactory.createCustomer(customer)
            .then(function (customer) {
                vm.customers.push(customer.data);
                toastr.success('Customer' + customer.data.name + ' was inserted!', 'Success!');
            })
            .catch(function (error) {
                toastr.error('Error saving customer!', 'Error!');
                console.log(error)
            });
    }

    vm.getCustomer = function (id) {
        customerFactory.getCustomer(id)
            .then(function (customer) {
                vm.customer = customer.data;
                $rootScope.globals.customer = customer.data;
                $cookies.putObject('globals', $rootScope.globals);
            })
            .catch(function (error) {
                toastr.error('Error getting customer!', 'Error!');
                console.log(error)
            });
    }

    vm.unsetCustomer = function(){
        $rootScope.globals.customer = {};
        $cookies.putObject('globals', $rootScope.globals);
        $location.url('/');
    }

    vm.generateInvoice = function (id) {
        customerFactory.generateInvoice(id)
            .then(function (invoice) {
                if(!invoice.data.parkeds){
                    toastr.warning("You don't have any parked item for invoicing.", 'Info');
                } else {
                    vm.customer.invoices.push(invoice.data);
                }
            })
            .catch(function (error) {
                toastr.error('Error generating invoice!', 'Error!');
                console.log(error)
            });
    }
};

function parkedController($rootScope, $location, $cookies, parkedFactory, parkingFactory, toastr) {
    var vm = this;
    vm.parked = {};
    var today = new Date();
    vm.parked.inTime = new Date(today.getFullYear(), today.getMonth(), today.getDate(), today.getHours(), today.getMinutes());
    vm.parked.outTime = new Date(today.getFullYear(), today.getMonth(), today.getDate(), today.getHours(), today.getMinutes());
    vm.parkings = [];

    vm.getParkings = getParkings;
    vm.createParked = createParked;

    function getParkings() {
        parkingFactory.getParkings()
            .then(function (parkings) {
                vm.parkings = parkings.data;
            })
            .catch(function (error) {
                toastr.error('Error getting parkings!', 'Error!');
                console.log(error)
            });
    }

    function createParked(parked) {
        parkedFactory.createParked(parked)
            .then(function (result) {
                toastr.success('Item inserted!', 'Success!');
                $location.url('/parkinginfo');
            })
            .catch(function (error) {
                toastr.error('Error saving parked info! ' + error.data, 'Error!');
                console.log(error)
            });
    }

    getParkings();

}
angular
    .module('app')
    .controller("parkedController", parkedController)
    .controller("customerController", customerController);