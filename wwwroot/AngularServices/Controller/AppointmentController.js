(function () {
    'use strict';

    angular
        .module('startapp')
        .controller('AppointmentController', ['$scope', function AppointmentController($scope) {
          
            $scope.products = "Test";
            console.log($scope.asd);
        }]);
})();
