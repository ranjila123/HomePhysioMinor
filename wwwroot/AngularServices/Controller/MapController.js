
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

                   

        }]);

})();
