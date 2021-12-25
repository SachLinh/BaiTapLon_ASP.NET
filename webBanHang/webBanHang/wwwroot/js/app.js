var app = angular.module('myApp', ['ngAnimate']);


app.controller('productCtrl', ['$scope', function ($scope) {

    
    $scope.hiddenView = false;
   
    // Quick view
    $scope.quickView = function () {
        $scope.hiddenView = true;
    }

    $scope.closeQuickView = function () {
        $scope.hiddenView = false;
    }

}]);