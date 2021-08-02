using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParcelService
{
    /// <summary>
    /// ParcelService is an interface into the Regrid api to retrieve information about a piece of land.
    /// Copyright 2021 - E. Scott McFadden
    /// </summary>
    public class ParcelService
    {
        #region Public Properties
        /// <summary>
        /// ServiceURL is the url we use to communicate with the regrid api
        /// </summary>
        public string ServiceURL { get; set; }  = "https://app.regrid.com//";
        /// <summary>
        /// SecurityToken is the API key that is used by Regrid to authenicate the request
        /// </summary>
        public string SecurityToken { get; set; } = "xxxxx";
        /// <summary>
        /// Limit is the number of results that can be returned.   Max is 500 (no enforced by this service)
        /// </summary>
        public int Limit { get; set; } = 20;
        /// <summary>
        /// The logger service that is injected into this service.
        /// </summary>
        public ILogger log { get;  }
        #endregion

        #region constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public ParcelService(ILogger logger)
        {
            log = logger;
            log.Debug("constructing ParcelService");


        }

        /// <summary>
        /// Constructor - include SecurityToken
        /// </summary>
        /// <param name="logger">Logging Service Object</param>
        /// <param name="Token">Security Token</param>
        public ParcelService(ILogger logger, string Token)
        {
            log = logger;
            log.Debug("constructing ParcelService");
            SecurityToken = Token; 
        }
        #endregion

        #region API Methods
        /// <summary>
        /// GetParcelByCoordinates 
        /// 
        /// Async method to retrieve GeoJson object that describes the property
        /// </summary>
        /// <param name="lat">lattitude of property</param>
        /// <param name="lon">longitude of property</param>
        /// <returns>GeoJson string</returns>
        public async Task<string> GetParcelByCoordinatesAsync(float lat, float lon)
        {
            HttpClient client = new HttpClient();
            var target = ServiceURL + GetParcelByCoordinates(ref lat,ref lon);
            HttpResponseMessage response = await client.GetAsync(target);
            
            return await HandleResponseAsync(response, target);
        }

        /// <summary>
        /// GetParcelByNumberAsync
        /// 
        /// Async method to retrieve GeoJson object that describes 
        /// the property based on parcel number
        /// </summary>
        /// <param name="parceNumb">Parcel Number </param>
        /// <returns>GeoJson string</returns>
        public async Task<string> GetParcelByNumberAsync(string parceNumb)
        {
            HttpClient client = new HttpClient();
            var target = ServiceURL + GetParcelByNumber(ref parceNumb );
            HttpResponseMessage response = await client.GetAsync(target);

            return await HandleResponseAsync(response, target);
        }

        /// <summary>
        /// GetParcelByAddressAsync
        /// 
        /// Async method to retrieve GeoJson object that describes 
        /// the property based on address
        /// </summary>
        /// <param name="Address">UUEncoded address</param>
        /// <param name="context">uuencoded context which limits the scope of the search.  
        /// For example, /us/oh/hamilton for Hamilton County, OH.For example, 
        /// /us/oh/hamilton for Hamilton County, OH. </param>
        /// <returns>GeoJson string</returns>
        public async Task<string> GetParcelByAddressAsync(string Address, string context)
        {
            HttpClient client = new HttpClient();
            var target = ServiceURL + GetParcelByAddress(ref Address, ref context);
            HttpResponseMessage response = await client.GetAsync(target);

            return await HandleResponseAsync(response, target);
        }
        /// <summary>
        /// GetParcelByPath
        /// 
        /// retrieves path of parcel from context path. 
        /// </summary>
        /// <param name="path">Parcel paths are similar to place paths and include
        /// an integer ID at the end. For example, /us/mi/wayne/detroit/555.</param>
        /// <returns>GeoJson string</returns>
        public async Task<string> GetParcelByPath(string path)
        {
            HttpClient client = new HttpClient();
            var target = ServiceURL + path;
            HttpResponseMessage response = await client.GetAsync(target);

            return await HandleResponseAsync(response, target);
        }

        #endregion

        #region Utility Methods
        private async Task<string> HandleResponseAsync(HttpResponseMessage  response, string target)
        {
            var ret = "";
            if (response.IsSuccessStatusCode)
            {
                ret = "success : " + await response.Content.ReadAsAsync<string>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                ret = $"not found : target: {target}  ";
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                ret = $"forbidden - Security Key is probably invalid : target: {target}  ";
            else
                ret = $"response code: {response.StatusCode}, target: {target} ";

            return ret;
        }
        #endregion

        #region API Parameter strings
        private string GetParcelByNumber(ref string parceNumb)
        {
            return $"api//v1//search.json?parcelnumb={parceNumb}&token={SecurityToken}&limit={Limit}";
        }
        private string GetParcelByCoordinates(ref float lat, ref float lon)
        {
            return $"api//v1//search.json?lat={lat}&lon={lon}&token={SecurityToken}&limit={Limit}";
        }
        private string GetParcelByAddress(ref string address, ref string context)
        {
            return $"api//v1//search.json?query={address}&token={SecurityToken}&limit={Limit}" + ( String.IsNullOrEmpty(context) ? "" : $"&context={context}");
        }
        #endregion
    }
}
