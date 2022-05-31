using BackEndConstant;
using System;

namespace BackEndHelpers
{
    public static class RouteHelpers
    {
        
        public static string GetNasaRoute()
        {
            string URLReturl = "";
            string startDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
            URLReturl = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate}&end_date={endDate}&api_key={Constants.APIKEYNASA.NASAKEY}";
            return URLReturl;
        }
    }
}
