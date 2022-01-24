////const { get } = require("jquery");

(function () {
    'use strict';

    angular
        .module('startapp')
        .controller('HomeController', ['$scope', '$http', function HomeController($scope, $http) {
            $scope.url = `${document.location.origin}/Home/AppointmentList`;

            $scope.appoint = [];
            $scope.AppointmentList = function (id) {
                $http({ method: 'post', url: $scope.url, params: { patientId:id} }).
                    then(function (response) {
                        $scope.appoint = response.data.ad;
                    }, function (response) {

                    });
            };

           

            $scope.products = 'aa';
        }]);

})();
