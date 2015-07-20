(function(){
  angular.module('foodApp.places')
    .factory('PlacesService', PlacesService);

    PlacesService.$inject = ['$http', 'apiConstants', 'PlacesModel'];

  function PlacesService($http, apiConstants, PlacesModel){
    var places = {
        getYelpData: getYelpData,
        GetYelpResults: getYelpResults
    };

    return places;


    function add(placeInfo){
      var data = {
        address: placeInfo.placeAddress,
        phone: placeInfo.placePhone,
        hours: placeInfo.placeHours,
        tags: placeInfo.placeTags.split(','),
        url: placeInfo.placeUrl,
        name: placeInfo.placeName
      };

      return $http.post(apiConstants.apiUrl, data).then(
        function(response){
          //get a positive response from api
        },
        function(error){
          console.log(error);

        }
      );
    }
      
    function getYelpData(query, isSearch){

        var method = 'GET';
        var searchUrl = 'http://api.yelp.com/v2/search';
        var businessUrl = 'http://api.yelp.com/v2/business/'+query;
        var url = (isSearch ? searchUrl : businessUrl) + '?callback=JSON_CALLBACK';
        var params = {
                callback: 'JSON_CALLBACK',
                
                oauth_consumer_key: 'YzvVFNtk6g0SrGKWYvJHlA', //Consumer Key
                oauth_token: 'HOXHS1WtDABQcLpGjVc0rfX1EcAsam0M', //Token
                oauth_signature_method: "HMAC-SHA1",
                //Authorization: "Token: Z9UcGd9vGjLxozt0hxDmZA3OW_50EH4N",
                oauth_timestamp: new Date().getTime(),
                oauth_nonce: randomString(32, '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'),
                
            };
        if(isSearch){
            params.location = 'Chicago';
            params.term = query;
        }
            
        
        var consumerSecret = 'FW9dld_EbBgaI5VL0FnymJWXcIc'; //Consumer Secret
        var tokenSecret = 'U9oAjOSQfVgFE-iWc76tOt8wedg'; //Token Secret
        var signature = oauthSignature.generate(method, url, params, consumerSecret, tokenSecret, { encodeSignature: false});
        params['oauth_signature'] = signature;
        return $http.jsonp(url, {params: params}).success(function(data, status, headers, config){
            return data;
        }).error(function(data, status, headers, config){
            return status;
        });
       
    }

    function randomString(length, chars) {
        var result = '';
          for (var i = length; i > 0; --i) result += chars[Math.round(Math.random() * (chars.length - 1))];
          return result;
      }

    function getYelpResults(){
        return PlacesModel.places;
    }
  }
})();