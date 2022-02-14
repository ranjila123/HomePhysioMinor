
(function () {
    'use strict';

    angular
        .module('startapp')
        .controller('MapController', ['$scope', '$http', function MapController($scope, $http) {
            $scope.url = `${document.location.origin}/Map/Calculate`;

            //$scope.physiotherapits = [];
            $scope.CallHttpPost = function () {
                $http({ method: 'post', url: $scope.url, params: {  } }).
                    then(function (response) {
                        $scope.physiotherapist = response.data.p;
                    }, function (response) {

                    });
            };

            //$scope.url1 = `${document.location.origin}/Home/Physio_info`;
            //$scope.GetPhysioList = function (id) {
            //    $http({ method: 'post', url: $scope.url1, params: { physiotherapistId: id }}).
            //        then(function (response) {
            //            $scope.physio = response.data.pinfo;

            //           // $scope.PhysioInfo($scope.physio);
            //        }, function (response) {

            //        });
            //};
                   
            //$scope.PhysioInfo = function (physio) {
            //    $http({ method: 'post', url: $scope.url, params: {} }).
            //        then(function (response) {
            //            $scope.physiotherapist = response.data.p;
            //        }, function (response) {

            //        });
            //};

            $scope.physio = [];
            $scope.GetPhysioList = function (id) {
                $http({ method: 'post', url: `${document.location.origin}/Home/Physio_info`, params: { physiotherapistId: id } }).
                    then(function (response) {
                        $scope.physio = response.data.pinfo;
                       
                    }, function (response) {
                        alert("false");
                    });
            };
        }]);

})();
