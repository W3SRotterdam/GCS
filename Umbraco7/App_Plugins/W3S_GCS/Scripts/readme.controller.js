angular.module("umbraco").controller("W3S.GCS.ReadMeController",
    function ($scope, $routeParams, $location, $http, GCSResources, notificationsService, editorState) {
        $scope.loaded = false;

        GCSResources.GetProperties($routeParams.alias).then(function (data) {
            $scope.content = data.data;
            editorState.set($scope.content);
            GCSResources.GetReadMe().then(function (data) {
                var converter = new showdown.Converter();
                converter.setOption('tables', true);
                text = data.data.markdown;
                html = converter.makeHtml(text);
                $scope.readme = html;
                $scope.loaded = true;
            });
        });
    }
);
