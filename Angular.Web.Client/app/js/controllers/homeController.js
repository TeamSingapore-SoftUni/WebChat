/* Controllers */
var webchatAppControllers = webchatAppControllers || angular.module('webchatAppControllers', []);

webchatApp.controller('HomeController',
    function homeController($scope, $rootScope, $http,  errorsService) {
        // $scope.loading = true;
        // $scope.noAdsToDisplay = false;

        // /* filter buttons values*/
        // $scope.townFilter = "Town";
        // $scope.categoryFilter = "Category";

        // /* get selected town/category id for further filtering */
        // var currentCategoryId = '',
        //     currentTownId = '',
        //     currentPage = 1;

        // /* pagination */
        // $scope.totalAds = 0;
        // $scope.adsPerPage = parseInt(adsPerPageUser);
        // getResultsPage(1);

        // $scope.pagination = {
        //     current: 1
        // };

        // $scope.pageChanged = function(newPage) {
        //     getResultsPage(newPage);
        // };

        // function getResultsPage(pageNumber) {
        //     adsData.getAllAddsFiltered(pageNumber, currentTownId, currentCategoryId, $scope.adsPerPage).then(function(data) {
        //         $scope.noAdsToDisplay = false;
        //         $scope.loading = true;
        //         $scope.adsData = data;
        //         $scope.totalAds = parseInt(data.numPages) * $scope.adsPerPage;
        //         currentPage = pageNumber;
        //     }, function(error) {
        //         errorsService.handleError(error);
        //     }).finally(function() {
        //         $scope.loading = false;
        //         $('html, body').animate({
        //             scrollTop: 0
        //         }, 1000);
        //     });
        // }

        // /* get all categoreis */
        // categoriesData.getAll().then(function(data) {
        //     $scope.categoriesData = data;
        // }, function(error) {
        //     errorsService.handleError(error);
        // });

        // /* filter ads by category */
        // $scope.filterByCategory = function(categoryId, cateogryName) {
        //     adsData.getAllAddsFiltered(currentPage, currentTownId, categoryId, $scope.adsPerPage).then(function(data) {
        //         $scope.noAdsToDisplay = false;
        //         $scope.loading = true;
        //         $scope.adsData = data;

        //         if (data.ads.length === 0) {
        //             $scope.noAdsToDisplay = true;
        //         }

        //         $scope.totalAds = parseInt(data.numPages) * $scope.adsPerPage;;
        //         $scope.categoryFilter = cateogryName;
        //         currentCategoryId = categoryId;
        //     }, function(error) {
        //         errorsService.handleError(error);
        //     }).finally(function() {
        //         $scope.loading = false;
        //     });
        // };

        // /* get all towns*/
        // townsData.getAll().then(function(data) {
        //     $scope.townsData = data;
        // }, function(error) {
        //     errorsService.handleError(error);
        // });

        // /* filter ads by town*/
        // $scope.filterByTown = function(townId, townName) {
        //     adsData.getAllAddsFiltered(currentPage, townId, currentCategoryId, $scope.adsPerPage).then(function(data) {
        //         $scope.noAdsToDisplay = false;
        //         $scope.loading = true;
        //         $scope.adsData = data;

        //         if (data.ads.length === 0) {
        //             $scope.noAdsToDisplay = true;
        //         }

        //         $scope.totalAds = parseInt(data.numPages) * $scope.adsPerPage;;
        //         $scope.townFilter = townName;
        //         currentTownId = townId;
        //     }, function(error) {
        //         errorsService.handleError(error);
        //     }).finally(function() {
        //         $scope.loading = false;
        //     });
        // };

        // $scope.filterByCount = function(count) {
        //     var adsPerPage = parseInt(count);
        //     adsData.getAllAddsFiltered(currentPage, currentTownId, currentCategoryId, adsPerPage).then(function(data) {
        //         $scope.noAdsToDisplay = false;
        //         $scope.loading = true;
        //         $scope.adsData = data;

        //         if (data.ads.length === 0) {
        //             $scope.noAdsToDisplay = true;
        //         }

        //         $scope.totalAds = parseInt(data.numPages) * adsPerPage;
        //         $scope.adsPerPage = adsPerPage;
        //     }, function(error) {
        //         errorsService.handleError(error);
        //     }).finally(function() {
        //         $scope.loading = false;
        //         $('html, body').animate({
        //             scrollTop: 0
        //         }, 1000);
        //     });
        // };
    });