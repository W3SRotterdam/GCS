angular.module("umbraco.resources")
.factory("GCSResources", function ($http) {
    return {
        GetResults: function(q, index, fileType) {
            return $http.get("/umbraco/surface/GCSearch/Get?query=" + q + "&startIndex=" + index + "&fileType=" + fileType + "&section=" + section);
        },
        GetSettings: function () {
            return $http.get("/umbraco/surface/GCSSettings/Get");
        },
        SaveSettings: function (obj) {
            return $http.post("/umbraco/surface/GCSSettings/Set", { model: obj });
        },
        GetProperties: function (alias) {
            return $http.get("/umbraco/surface/GCSProperties/Get?alias=" + alias);
        },
        GetStats: function (year, month) {
            return $http.get("/umbraco/surface/GCSStatistics/Get?year=" + year + "&month=" + month);
        },
        GetReadMe: function () {
            return $http.get("/umbraco/surface/GCSearch/ReadMe");
        },
        CheckDb: function () {
            return $http.get("/umbraco/surface/GCSearch/CheckDb");
        },
        InitDb: function () {
            return $http.get("/umbraco/surface/GCSearch/InitDb");
        }
    };
});