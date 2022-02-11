////const { get } = require("jquery");

(function () {
    'use strict';

    angular
        .module('startapp')
        .controller('HomeController', ['$scope', '$http','$ngConfirm', function HomeController($scope, $http,$ngConfirm) {
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
                $ngConfirm({
                    title: 'Appointment',
                    content: '<strong>Are you sure to make this appointment?</strong>',
                    scope: $scope,
                    type: 'green',
                    buttons: {
                        yes: {
                            text: 'Ok',
                            keys: ['enter'],
                            btnClass: 'btn-blue',
                            action: function (scope, button) {
                                $scope.Physioappoint = [];
                                //$window.location.reload();
                                $http({ method: 'post', url: `${document.location.origin}/Appointment/ConfirmAppointment`, params: { appointmentId: id } }).
                                    then(function (response) {
                                        $scope.PhysioAppointmentList(physiotherapistId);

                                    }, function (response) {

                                    });
                            }
                        },
                         cancel: {
                            text: 'Cancel',
                            keys: ['esc'],
                            btnClass: 'btn-red',
                            action: function (scope, button) {
                            }
                        }
                    }
                });

                
            };


           

            $scope.products = 'aa';
        }]);

})();
