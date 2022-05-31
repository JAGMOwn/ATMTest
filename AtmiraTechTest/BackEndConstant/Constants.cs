using System;
using System.Collections.Generic;

namespace BackEndConstant
{
    public static class Constants
    {
        public static class APIKEYNASA
        {
            /// <summary>
            /// Use APIKEY zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb for no restrictions             
            /// </summary>
            public const string NASAKEY = "zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb";
        }
        public static class Params
        {
            /// <summary>
            /// Planets of solar system
            /// </summary>
            public static readonly List<string> ALLOWEDPLANETS = new List<string>() { "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
        }

        public static class EmptyOrNullParamsResponseMessage
        {
            /// <summary>
            /// Message for empty, white space or null param
            /// </summary>
            public static readonly string NWEPARAM = "400 Error parameter can't be empty, white space or null";
        }

        public static class InvalidParamsResponseMessage
        {
            /// <summary>
            /// Message for invalid param
            /// </summary>
            public static readonly string FORBIDENPLANET = "400 Error planet must belong to the solar system";
        }
    }
}
