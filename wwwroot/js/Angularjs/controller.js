function customerController($window, $rootScope, $cookies, customerFactory, SweetAlert, toastr) {
    var vm = this;
    vm.customers = [];
    vm.customer = {};
    vm.invoice = {};

    vm.getCustomers = function () {
        customerFactory.getCustomers()
            .then(function (customers) {
                vm.customers = customers.data;
                $rootScope.globals.customers = customers.data;
                var cookieExp = new Date();
                cookieExp.setDate(cookieExp.getDate() + 7);
                $cookies.putObject('globals', $rootScope.globals, { expires: cookieExp });
            }).catch(function (error) {
                SweetAlert.swal("Error while getting customers.")
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

    vm.generateInvoice = function (id) {
        customerFactory.generateInvoice(id)
            .then(function (invoice) {
                vm.customer.invoices.push(invoice.data);
            })
            .catch(function (error) {
                toastr.error('Error generating invoice!', 'Error!');
                console.log(error)
            });
    }
};

function invoiceController($rootScope, $cookies, customerFactory, SweetAlert, toastr) {
    var vm = this;
}
function parkedController($rootScope, $cookies, parkedFactory, parkingFactory, SweetAlert, toastr) {
    var vm = this;
    vm.parked = {};
    vm.parked.inTime = new Date();
    vm.parked.outTime = new Date();
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
            })
            .catch(function (error) {
                toastr.error('Error saving parked info!', 'Error!');
                console.log(error)
            });
    }

    getParkings();

}
angular
    .module('app')
    .controller("invoiceController", invoiceController)
    .controller("parkedController", parkedController)
    .controller("customerController", customerController);