﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandREng.Utilities.Logging
{
#if NETCORE
    public static class DateTimeExtensions
    {
        public static string ToShortDateString(this DateTime dateTime) => dateTime.ToString("d");
        public static string ToShortTimeString(this DateTime dateTime) => dateTime.ToString("t");
        public static string ToLongDateString(this DateTime dateTime) => dateTime.ToString("D");
        public static string ToLongTimeString(this DateTime dateTime) => dateTime.ToString("T");
    }
#endif
}
