angular.module("umbraco").controller("W3S.GCS.EditController",
    function ($scope, $routeParams, $location, $http, GCSResources, notificationsService, editorState) {
        $scope.loaded = false;

        GCSResources.GetProperties($routeParams.alias).then(function (data) {
            $scope.content = data.data;
            editorState.set($scope.content);
            $scope.loaded = true;
        });

        $scope.publish = function (e) {
            GCSResources.SaveSettings(populatePayload()).then(function (data) {
                if (data.data.success) {
                    notificationsService.success("Succesfully saved settings.");
                } else {
                    notificationsService.error("Something went wrong. Please check if all mandatory fields are set.");
                }
            });
        };

        function populatePayload() {
            var payload = {};

            for (var i = 0; i < $scope.content.tabs.length; i++) {
                for (var j = 0; j < $scope.content.tabs[i].properties.length; j++) {
                    payload[$scope.content.tabs[i].properties[j].alias] = $scope.content.tabs[i].properties[j].value;
                }
            }

            return JSON.stringify(payload);
        };
    }
);
