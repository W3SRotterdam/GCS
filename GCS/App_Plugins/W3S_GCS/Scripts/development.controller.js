angular.module("umbraco").controller("W3S.GCS.DevelopmentController",
    function ($scope, $routeParams, $location, $http, GCSResources, notificationsService, editorState) {
        $scope.loaded = false;

        GCSResources.GetProperties($routeParams.alias).then(function (data) {
            $scope.content = data.data;
            editorState.set($scope.content);
            $scope.loaded = true;
        });

        $scope.getResults = function (query, index, fileType) {
            GCSResources.GetResults(query, index, fileType).then(function (data) {
                $scope.results = data.data;
            })
        }
    }
);
