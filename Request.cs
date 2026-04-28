using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MyBrowser
{
    /// <summary>
    /// This class defines the functionality for sending HTTP requests to a url and fetching the raw html, handling status codes, etc.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// This class is used to compact the result returned by a request as an object
        /// </summary>
        public class WebPageResult
        {
            public string StatusCode { get; set; }
            public string Title { get; set; }
            public string HtmlContent { get; set; }
            public bool Success { get; set; }
        }

        // fetch the requested web page without blocking
        public static async Task<WebPageResult> GetWebPage(string url)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // send GET request to url
                    HttpResponseMessage response = await httpClient.GetAsync(url).ConfigureAwait(false);

                    string statusCode;
                    // http status codes to display
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK: statusCode = "200 OK - Success";
                            break;
                        case System.Net.HttpStatusCode.BadRequest:
                            statusCode = "400 Bad Request";
                            break;
                        case System.Net.HttpStatusCode.Forbidden:
                            statusCode = "403 Forbidden";
                            break;
                        case System.Net.HttpStatusCode.NotFound:
                            statusCode = "404 Not Found";
                            break;
                        default:
                            statusCode = $"Status Code : {(int)response.StatusCode} {response.ReasonPhrase}";
                            break;
                    }

                    // OK, then get html
                    if (response.IsSuccessStatusCode)
                    {
                        // can just read as string since we only have to display raw html
                        string rawHtml = await response.Content.ReadAsStringAsync();
                        string title = ExtractTitle(rawHtml);

                        return new WebPageResult
                        {
                            StatusCode = statusCode,
                            Title = title ?? "(none)",
                            HtmlContent = rawHtml,
                            Success = true
                        };
                    }
                    // else return status code error and set flag to false
                    else
                    {
                        return new WebPageResult
                        {
                            StatusCode = statusCode,
                            Title = null,
                            HtmlContent = null,
                            Success = false
                        };
                    }
                }
            }
            // exception for any networking issuess
            catch (HttpRequestException ex)
            {
                return new WebPageResult
                {
                    StatusCode = $"Error making request: {ex.Message}",
                    Success = false
                };
            }
            // handle invalid url 
            catch (UriFormatException)
            {
                return new WebPageResult
                {
                    StatusCode = $"Invalid URL format",
                    Success = false
                };
            }
            // general for unexpected errors
            catch (Exception ex)
            {
                return new WebPageResult
                {
                    StatusCode = $"Connection failed, could not fetch page",
                    Success = false
                };
            }
        }

        /// <summary>
        /// Uses the HTML Agility Pack Html Document parser to get title tag and returns inner html
        /// </summary>
        /// <param name="html"></param>
        /// <returns>titel of web page as string</returns>
        private static string ExtractTitle(string html)
        {
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);

            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

            if (htmlBody == null)
            {
                return "none";
            }
            else
            {
                return htmlBody.InnerHtml;
            }
        }
    }
}
