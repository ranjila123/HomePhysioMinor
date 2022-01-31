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

            $scope.Physioappoint = [];
            $scope.PhysioAppointmentList = function (id) {
                $http({ method: 'post', url: `${document.location.origin}/Home/PhysioAppointmentList`, params: { physiotherapistId: id } }).
                    then(function (response) {
                        $scope.Physioappoint = response.data.ap;
                    }, function (response) {

                    });
            };


            $scope.ConfirmAppointment = function (id, physiotherapistId) {
                $scope.Physioappoint = [];

                //$window.location.reload();
                $http({ method: 'post', url: `${document.location.origin}/Appointment/ConfirmAppointment`, params: { appointmentId: id } }).
                    then(function (response) {
                        $scope.PhysioAppointmentList(physiotherapistId);
                        
                    }, function (response) {

                    });
            };


           

            $scope.products = 'aa';
        }]);

})();
