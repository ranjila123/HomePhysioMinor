////const { get } = require("jquery");

(function () {
    'use strict';

    angular
        .module('startapp')
        .controller('AppointmentController', ['$scope', '$http', function AppointmentController($scope, $http) {
            $scope.url = `${document.location.origin}/Appointment/GetPhysiotherapistList`;
            $scope.physiotherapits = [];
            $scope.CallHttpPost = function () {
                $http({ method: 'post', url: $scope.url, params: { categoryId: $scope.searchType } }).
                    then(function (response) {
                        $scope.physiotherapits = response.data.a;
                    }, function (response) {

                    });
            };
            $scope.products = 'aa';
        }]);
})();
