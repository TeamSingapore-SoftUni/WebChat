// /* Serrvice for handling error  */
// webchatApp.factory('errorsService', function errorsService($rootScope, ajaxErrorText, baseUrl, imageSize) {
//     function handleError(error) {
//         if (error.message.indexOf('The image size should be less than ') > -1) {
//             $rootScope.$broadcast('alertMessage', 'Image size too big! The image size should be less than' + imageSize + '!');
//         } else if (error.message.indexOf('Authorization has been denied') > -1) {
//             $rootScope.$broadcast('alertMessage', 'You are not authorized to perform this type of request!');
//         } else if (error.message.indexOf('You Can not delete category with advertisements') > -1) {
//             $rootScope.$broadcast('alertMessage', error.message);
//         } else if (error.message.indexOf('You can not delete a town that is used by users!') > -1) {
//             $rootScope.$broadcast('alertMessage', error.message);
//         } else {
//             $rootScope.$broadcast('alertMessage', ajaxErrorText);
//         }
//     };

//     function handleLogingError(error) {
//         if (error.error_description) {
//             $rootScope.$broadcast('alertMessage', error.error_description);
//         } else {
//             $rootScope.$broadcast('alertMessage', ajaxErrorText);
//         }
//     };

//     function handleRegisterError(errorMessage) {
//         if (errorMessage['']) {
//             $rootScope.$broadcast('alertMessage', errorMessage[''][0]);
//         } else if (errorMessage['model.ConfirmPassword']) {
//             $rootScope.$broadcast('alertMessage', errorMessage['model.ConfirmPassword'][0]);
//         } else {
//             $rootScope.$broadcast('alertMessage', ajaxErrorText);
//         }
//     };

//     function handleProfileEditError(errorMessage) {
//         if (errorMessage['']) {
//             $rootScope.$broadcast('alertMessage', errorMessage[''][0]);
//         } else if (errorMessage['model.ConfirmPassword']) {
//             $rootScope.$broadcast('alertMessage', errorMessage['model.ConfirmPassword'][0]);
//         } else {
//             $rootScope.$broadcast('alertMessage', ajaxErrorText);
//         }
//     };

//     return {
//         handleError: handleError,
//         handleLogingError: handleLogingError,
//         handleRegisterError: handleRegisterError,
//         handleProfileEditError: handleProfileEditError
//     };
// });