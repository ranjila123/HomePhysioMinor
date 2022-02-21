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
                            text: 'Yes',
                            keys: ['enter'],
                            btnClass: 'btn-blue',
                            action: function (scope, button) {
                                $scope.Physioappoint = [];
                                //$window.location.reload();
                                $http({ method: 'post', url: `${document.location.origin}/Appointment/ConfirmAppointment`, params: { appointmentId: id } }).
                                    then(function (response) {
                                        $scope.CreatePayment(id, physiotherapistId);
                                       

                                    }, function (response) {

                                    });
                            }
                        },
                         cancel: {
                            text: 'No',
                            keys: ['esc'],
                            btnClass: 'btn-red',
                            action: function (scope, button) {
                            }
                        }
                    }
                });


               
                           
                               
            };

            $scope.CreatePayment = function (id, physiotherapistId) {
                $http({ method: 'post', url: `${document.location.origin}/Payment/Create`, params: { appointmentId: id } }).
                    then(function (response) {
                        $scope.PhysioAppointmentList(physiotherapistId);

                    }, function (response) {

                    });
            };

            $scope.CancelAppointment = function (id, physiotherapistId) {
                $ngConfirm({
                    title: 'Appointment',
                    content: '<strong>Are you sure to cancel this appointment?</strong>',
                    scope: $scope,
                    type: 'red',
                    buttons: {
                        yes: {
                            text: 'Ok',
                            keys: ['enter'],
                            btnClass: 'btn-red',
                            action: function (scope, button) {
                                $scope.Physioappoint = [];
                                //$window.location.reload();
                                $http({ method: 'post', url: `${document.location.origin}/Appointment/CancelAppointment`, params: { appointmentId: id } }).
                                    then(function (response) {
                                        $scope.PhysioAppointmentList(physiotherapistId);

                                    }, function (response) {

                                    });
                            }
                        },
                        cancel: {
                            text: 'Cancel',
                            keys: ['esc'],
                            btnClass: 'btn-blue',
                            action: function (scope, button) {
                            }
                        }
                    }

                });
            };

            $scope.CancelAppointmentByPatient = function (id, patientId) {
                $ngConfirm({
                    title: 'Appointment',
                    content: '<strong>Are you sure to cancel this appointment?</strong>',
                    scope: $scope,
                    type: 'red',
                    buttons: {
                        yes: {
                            text: 'Ok',
                            keys: ['enter'],
                            btnClass: 'btn-red',
                            action: function (scope, button) {
                                $scope.Physioappoint = [];
                                //$window.location.reload();
                                $http({ method: 'post', url: `${document.location.origin}/Appointment/CancelAppointment`, params: { appointmentId: id } }).
                                    then(function (response) {
                                        $scope.AppointmentList(patientId);

                                    }, function (response) {

                                    });
                            }
                        },
                        cancel: {
                            text: 'Cancel',
                            keys: ['esc'],
                            btnClass: 'btn-blue',
                            action: function (scope, button) {
                            }
                        }
                    }

                });
            };

            $scope.url1 = `${document.location.origin}/Home/DropDown1Test`;
            //$scope.physiotherapits = [];
            $scope.CallPlist = function () {
                $http({ method: 'get', url: $scope.url1 }).
                    then(function (response) {
                        $scope.AllPhysioList = response.data.pList;
                        console.log($scope.AllPhysioList);
                    }, function (response) {

                    });
            };
            $scope.PhysioProfile = function (physiotherapistId) {
                window.location = `${document.location.origin}/Home/Physio_info/physiotherapistId=${physiotherapistId}`;
            };
           

            $scope.products = 'aa';
            $scope.PayKhalti = function (appointmentId,amount) {
                //console.log('test');
                //console.log($('#payment-button'));
                console.log(appointmentId);
                $scope.khaltiAppointmentId = appointmentId;
                $scope.khaltiAmount = amount * 100;
                setTimeout(function () {
                    $('#payment-button').click();
                }, 100);
               
               
            };
        }]);

})();
