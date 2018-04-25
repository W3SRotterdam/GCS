angular.module("umbraco").controller("W3S.CheckboxPlusInput", function ($rootScope, $scope, $routeParams, $injector, notificationsService, iconHelper, dialogService, $http, assetsService, $timeout) {
    $scope.values = $scope.model.config.items;
    $scope.saveValues = [];

    $scope.$watch("values", function (newValue, oldValue) {
        if (oldValue !== newValue) {
            $scope.saveValues = [];

            for (var i = 0; i < newValue.length; i++) {
                if (newValue[i].ischecked && newValue[i].text != "") {

                    //var exists = false;
                    //var index = 0;

                    //if ($scope.saveValues != []) {
                    //    for (var j = 0; j < $scope.saveValues.length; j++) {

                    //        console.log("$scope.saveValues[j]", $scope.saveValues[j].alias);
                    //        console.log("newValue[i].label", newValue[i].label);

                    //        console.log("$scope.saveValues[j].alias == newValue[i].label", $scope.saveValues[j].alias == newValue[i].label)

                    //        if ($scope.saveValues[j].alias == newValue[i].label) {
                    //            exists = true;
                    //            index = j;
                    //            break;
                    //        } else {
                    //            exist = false;
                    //            index = 0;
                    //        }
                    //    }
                    //}

                    //if (!exists) {
                    $scope.saveValues.push({
                        alias: newValue[i].label,
                        text: newValue[i].text

                    });
                    //} else {
                    //    $scope.saveValues[index] = {
                    //        alias: newValue[i].label,
                    //        text: newValue[i].text
                    //    }
                    //}
                }
            }

            $scope.model.value = JSON.stringify($scope.saveValues);
        }
    }, true);
});