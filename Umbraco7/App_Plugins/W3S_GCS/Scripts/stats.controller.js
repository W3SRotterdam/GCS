var app = angular.module("umbraco");
    //app.requires.push('chart.js');s
    app.controller("W3S.GCS.StatsController",
    function ($scope, $routeParams, $location, $http, GCSResources, notificationsService, editorState) {
        $scope.loaded = false;

        GCSResources.GetProperties($routeParams.alias).then(function (data) {
            $scope.content = data.data;
            editorState.set($scope.content);

            $scope.chosenyear = {};
            $scope.chosenmonth = {};

            $scope.years = [];
            for (var i = 2017; i < 2050; i++) {
                $scope.years.push(
                    {
                        name: i,
                        value: i
                    }
                );
            }

            $scope.months = [];
            for (var j = 1; j < 13; j++) {
                $scope.months.push(
                    {
                        name: j,
                        value: j
                    }
                );
            }

            $scope.datechange = function (year, month) {
                GCSResources.GetStats(year, month).then(function (data) {
                    appendData(data);
                });
            }

            $scope.loaded = true;
        });

        //GCSResources.GetStats(-1, -1).then(function (data) {
        //    appendData(data);
        //});

        function appendData(data) {
            $scope.stats = data.data;

            $scope.topqueries = [];
            angular.forEach(data.data.topqueries, function (value, key) {
                $scope.topqueries.push({
                    query: key,
                    occurence: value
                });
            });

            $scope.topspelling = [];
            angular.forEach(data.data.topspelling, function (value, key) {
                $scope.topspelling.push({
                    query: key,
                    occurence: value
                });
            });

            $scope.topclicks = [];
            angular.forEach(data.data.topclicks, function (value, key) {
                $scope.topclicks.push({
                    query: key,
                    occurence: value
                });
            });
        }
    }
);
