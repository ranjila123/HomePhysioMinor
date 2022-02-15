(function () {
    'use strict';

    angular
        .module('startapp')
        .controller('FileUploadController', ['$scope', 'Upload',function FileUploadController($scope, Upload) {
            $scope.PatientImgSubmit = function () {
                if ($scope.form.file.$valid && $scope.file) {
                    $scope.PatientImgUpload($scope.file,$scope.imageType);
                }
                else {
                    messageBox('error', 'File is not valid.');
                }
            };
            $scope.PatientImgUpload = function (file,imageType) {
                Upload.upload({                    
                    url: '/Home/UploadPatientImage',
                    data: { file: file,imageType:imageType }
                }).then(function (resp) {
                    $scope.file = undefined;
                    $('#previewimage').attr("src","/img/Profile pic/PP.png");
                }, function (resp) {
                    console.log('Error status: ' + resp.status);
                }, function (evt) {
                   
                });
            };
            $scope.PhysioImgSubmit = function () {
                if ($scope.form.file.$valid && $scope.file) {
                    $scope.PhysioImgUpload($scope.file, $scope.imageType);
                }
                else {
                    messageBox('error', 'File is not valid.');
                }
            };
            $scope.PhysioImgUpload = function (file, imageType) {
                Upload.upload({                    
                    url: '/Home/UploadPhysioImage',
                    data: { file: file,imageType:imageType }
                }).then(function (resp) {
                    $scope.file = undefined;
                    $('#previewimage').attr("src","/img/Profile pic/PP.png");
                }, function (resp) {
                    console.log('Error status: ' + resp.status);
                }, function (evt) {
                   
                });
            };
        }]);
})();
