app.config(function ($routeProvider) {
    var canRoute = function (isRequired) {

        return {
            /** Checks that the user is authenticated, then ensures that are requires assets are loaded */
            isAuthenticatedAndReady: function ($q, userService, $route, assetsService, appState) {
                var deferred = $q.defer();

                //don't need to check if we've redirected to login and we've already checked auth
                if (!$route.current.params.section
                    && ($route.current.params.check === false || $route.current.params.check === "false")) {
                    deferred.resolve(true);
                    return deferred.promise;
                }

                userService.isAuthenticated()
                    .then(function () {

                        assetsService._loadInitAssets().then(function () {

                            //This could be the first time has loaded after the user has logged in, in this case
                            // we need to broadcast the authenticated event - this will be handled by the startup (init)
                            // handler to set/broadcast the ready state
                            if (appState.getGlobalState("isReady") !== true) {
                                userService.getCurrentUser({ broadcastEvent: true }).then(function (user) {
                                    //is auth, check if we allow or reject
                                    if (isRequired) {
                                        //this will resolve successfully so the route will continue
                                        deferred.resolve(true);
                                    }
                                    else {
                                        deferred.reject({ path: "/" });
                                    }
                                });
                            }
                            else {
                                //is auth, check if we allow or reject
                                if (isRequired) {
                                    //this will resolve successfully so the route will continue
                                    deferred.resolve(true);
                                }
                                else {
                                    deferred.reject({ path: "/" });
                                }
                            }

                        });

                    }, function () {
                        //not auth, check if we allow or reject
                        if (isRequired) {
                            //the check=false is checked above so that we don't have to make another http call to check
                            //if they are logged in since we already know they are not.
                            deferred.reject({ path: "/login/false" });
                        }
                        else {
                            //this will resolve successfully so the route will continue
                            deferred.resolve(true);
                        }
                    });
                return deferred.promise;
            }
        };
    };

    $routeProvider
        .when('/GCS/GCS/Edit/:alias/:id', {
            template: "<div ng-include='templateUrl'></div>",
            controller: function ($scope, $route, $routeParams, treeService) {

                $routeParams.method = "Edit";
                $routeParams.section = "GCS";
                $routeParams.tree = "GCSTree";

                $scope.templateUrl = "/app_plugins/" + "W3S_GCS" + "/backoffice/" + $routeParams.tree + "/" + $routeParams.method + ".html";
            },
            resolve: canRoute(true)
        })
        .when('/GCS/GCS/Stats/:alias/:id', {
            template: "<div ng-include='templateUrl'></div>",
            controller: function ($scope, $route, $routeParams, treeService) {

                $routeParams.method = "Stats";
                $routeParams.section = "GCS";
                $routeParams.tree = "GCSTree";

                $scope.templateUrl = "/app_plugins/" + "W3S_GCS" + "/backoffice/" + $routeParams.tree + "/" + $routeParams.method + ".html";
            },
            resolve: canRoute(true)
        })
        .when('/GCS/GCS/Development/:alias/:id', {
            template: "<div ng-include='templateUrl'></div>",
            controller: function ($scope, $route, $routeParams, treeService) {

                $routeParams.method = "Development";
                $routeParams.section = "GCS";
                $routeParams.tree = "GCSTree";

                $scope.templateUrl = "/app_plugins/" + "W3S_GCS" + "/backoffice/" + $routeParams.tree + "/" + $routeParams.method + ".html";
            },
            resolve: canRoute(true)
        })
         .when('/GCS/GCS/ReadMe/:alias/:id', {
             template: "<div ng-include='templateUrl'></div>",
             controller: function ($scope, $route, $routeParams, treeService) {

                 $routeParams.method = "ReadMe";
                 $routeParams.section = "GCS";
                 $routeParams.tree = "GCSTree";

                 $scope.templateUrl = "/app_plugins/" + "W3S_GCS" + "/backoffice/" + $routeParams.tree + "/" + $routeParams.method + ".html";
             },
             resolve: canRoute(true)
         });
}).config(function ($locationProvider) {
});