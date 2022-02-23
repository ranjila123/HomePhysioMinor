(function () {
    'use strict';

    angular
        .module('startapp')
        .controller('FileUploadController', ['$scope', 'Upload',function FileUploadController($scope, Upload) {
            $scope.PatientImgSubmit = function (imageType) {
                if ($scope.form.file.$valid && $scope.file) {
                    $scope.PatientImgUpload($scope.file,imageType);
                }
                else {
                    alert('error', 'File is not valid.');
                }
            };
            $scope.PatientImgUpload = function (file,imageType) {
                Upload.upload({                    
                    url: '/Home/UploadPatientImage',
                    data: { file: file,imageType:imageType }
                }).then(function (resp) {
                    $scope.file = undefined;
                    $('#previewimage').attr("src", "/img/Profile pic/PP.png");
                    alert('Upload successful');
                    window.location.reload();
                }, function (resp) {
                    console.log('Error status: ' + resp.status);
                }, function (evt) {
                   
                });
            };
            $scope.PhysioImgSubmit = function (imageType) {
                if ($scope.form.file.$valid && $scope.file) {
                    $scope.PhysioImgUpload($scope.file,imageType);
                }
                else {
                    alert('error', 'File is not valid.');
                }
            };
            $scope.PhysioImgUpload = function (file, imageType) {
                Upload.upload({                    
                    url: '/Home/UploadPhysioImage',
                    data: { file: file,imageType:imageType }
                }).then(function (resp) {
                    $scope.file = undefined;
                    $('#previewimage').attr("src", "/img/Profile pic/PP.png");
                    alert('Upload successful');
                    window.location.reload();
                }, function (resp) {
                    console.log('Error status: ' + resp.status);
                }, function (evt) {
                   
                });
            };
        }]);
})();
