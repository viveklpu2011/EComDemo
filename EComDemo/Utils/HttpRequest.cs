using Plugin.Connectivity;
using EComDemo.ServiceConfigration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EComDemo.Utils
{
    public class HttpRequest
    {
        //protected static string HeaderName { get { return "AuthenticationToken"; } }
        //protected static string HeaderValue { get { return "9DCA8F18-C15F-44A7-8F33-63DBD363578D"; } }
        ServiceConfigrations service = new ServiceConfigrations();
        /// <summary>
        /// Purpose: Http Post Request
        /// </summary>
        /// <typeparam name="T">Resultant Class</typeparam>
        /// <param name="ReqUrl">Reuest Url</param>
        /// <param name="PostData">Post Data</param>
        /// <returns></returns>



        public static async Task<HttpRequestResponseStatus> PostRequest(string BaseReqUrl, string Api, string PostData)
        {
            var responseStatus = new HttpRequestResponseStatus();



            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(3);
                    client.BaseAddress = new Uri(BaseReqUrl);
                    HttpResponseMessage response = new HttpResponseMessage();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HeaderValue);
                    if (PostData != null)
                    {
                        HttpContent Content = new StringContent(PostData, Encoding.UTF8, "application/json");
                        response = await client.PostAsync(Api, Content).ConfigureAwait(false);
                    }
                    else
                    {
                        response = await client.PostAsync(Api, null).ConfigureAwait(false);
                    }



                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();



                        responseStatus = new HttpRequestResponseStatus()
                        {
                            Status = true,
                            Result = result
                        };



                        return responseStatus;
                    }
                    else
                    {
                        ///Handle Service Status
                        ///Status Code: 404 - Servive Not Found
                        ///Status Code: 500 - Internal Server Error



                        responseStatus = new HttpRequestResponseStatus()
                        {
                            Status = false,
                            Message = "There is internal error with services. Please contact administrator."
                        };




                        return responseStatus;//default(HttpRequestResponseStatus<T>);
                    }
                }
            }
            else
            {
                ///Handle Internet Connection: When user has not connected with Internet
                ///Message: Device is not connected with Internet. Please check your network connection.



                responseStatus = new HttpRequestResponseStatus()
                {
                    Status = false,
                    Message = "Device is not connected with Internet. Please check your network connection."
                };



                return responseStatus;//default(HttpRequestResponseStatus<T>);
            }
        }

        public static async Task<HttpRequestResponseStatus> PutRequest(string BaseReqUrl, string Api, string PostData)
        {
            var responseStatus = new HttpRequestResponseStatus();



            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(3);
                    client.BaseAddress = new Uri(BaseReqUrl);
                    HttpResponseMessage response = new HttpResponseMessage();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HeaderValue);
                    if (PostData != null)
                    {
                        HttpContent Content = new StringContent(PostData, Encoding.UTF8, "application/json");
                        response = await client.PutAsync(Api, Content).ConfigureAwait(false);
                    }
                    else
                    {
                        response = await client.PutAsync(Api, null).ConfigureAwait(false);
                    }



                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();



                        responseStatus = new HttpRequestResponseStatus()
                        {
                            Status = true,
                            Result = result
                        };



                        return responseStatus;
                    }
                    else
                    {
                        ///Handle Service Status
                        ///Status Code: 404 - Servive Not Found
                        ///Status Code: 500 - Internal Server Error



                        responseStatus = new HttpRequestResponseStatus()
                        {
                            Status = false,
                            Message = "There is internal error with services. Please contact administrator."
                        };




                        return responseStatus;//default(HttpRequestResponseStatus<T>);
                    }
                }
            }
            else
            {
                ///Handle Internet Connection: When user has not connected with Internet
                ///Message: Device is not connected with Internet. Please check your network connection.



                responseStatus = new HttpRequestResponseStatus()
                {
                    Status = false,
                    Message = "Device is not connected with Internet. Please check your network connection."
                };



                return responseStatus;//default(HttpRequestResponseStatus<T>);
            }
        }
        /// <summary>
        /// Purpose: Http Get Request
        /// </summary>
        /// <typeparam name="T">Resultant Class</typeparam>
        /// <param name="ReqUrl">Reuest Url with Query String</param>
        /// <returns></returns>
        public static async Task<HttpRequestResponseStatus> GetRequest(string ReqUrl)
        {
            var responseStatus = new HttpRequestResponseStatus();

            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ReqUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add(HeaderName, HeaderValue);

                    HttpResponseMessage response = await client.GetAsync(ReqUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();

                        responseStatus = new HttpRequestResponseStatus()
                        {
                            Status = true,
                            Result = result
                        };

                        return responseStatus;
                    }
                    else
                    {
                        ///Handle Service Status
                        ///Status Code: 404 - Servive Not Found
                        ///Status Code: 500 - Internal Server Error

                        if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        {
                            //Message: There is internal error with services. Please contact administrator.

                            responseStatus = new HttpRequestResponseStatus()
                            {
                                Status = false,
                                Message = "There is internal error with services. Please contact administrator."
                            };
                        }
                        else
                        {
                            responseStatus = new HttpRequestResponseStatus()
                            {
                                Status = false,
                                Message = "There is internal error with services. Please contact administrator."
                            };
                        }

                        return responseStatus;//default(HttpRequestResponseStatus<T>);
                    }
                }
            }
            else
            {
                ///Handle Internet Connection: When user has not connected with Internet
                ///Message: Device is not connected with Internet. Please check your network connection.

                responseStatus = new HttpRequestResponseStatus()
                {

                    Status = false,
                    Message = "Device is not connected with Internet. Please check your network connection."
                };

                return responseStatus;
            }
        }




        public static async Task<HttpRequestResponseStatus> DeleteRequest(string ReqUrl)
        {
            var responseStatus = new HttpRequestResponseStatus();

            if (CheckConnection())
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ReqUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add(HeaderName, HeaderValue);

                    HttpResponseMessage response = await client.DeleteAsync(ReqUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();

                        responseStatus = new HttpRequestResponseStatus()
                        {
                            Status = true,
                            Result = result
                        };

                        return responseStatus;
                    }
                    else
                    {
                        ///Handle Service Status
                        ///Status Code: 404 - Servive Not Found
                        ///Status Code: 500 - Internal Server Error

                        if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        {
                            //Message: There is internal error with services. Please contact administrator.

                            responseStatus = new HttpRequestResponseStatus()
                            {
                                Status = false,
                                Message = "There is internal error with services. Please contact administrator."
                            };
                        }
                        else
                        {
                            responseStatus = new HttpRequestResponseStatus()
                            {
                                Status = false,
                                Message = "There is internal error with services. Please contact administrator."
                            };
                        }

                        return responseStatus;//default(HttpRequestResponseStatus<T>);
                    }
                }
            }
            else
            {
                ///Handle Internet Connection: When user has not connected with Internet
                ///Message: Device is not connected with Internet. Please check your network connection.

                responseStatus = new HttpRequestResponseStatus()
                {

                    Status = false,
                    Message = "Device is not connected with Internet. Please check your network connection."
                };

                return responseStatus;
            }
        }











        //check internet connection
        public static bool CheckConnection()
        {
            var con = CrossConnectivity.Current.IsConnected;
            return con == true ? true : false;
        }
    }

    public class HttpRequestResponseStatus
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
    }
}
