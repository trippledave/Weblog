﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Weblog.Core.Helpers
{
    public static class InputFilterHelper
    {

        public const string FilterSpecialCharsRegex = "([A-Za-z0-9ÄäÖöÜüß]+)";
        public const string FilterHtmlTagsRegex = "([^<>]+)";


        public static String sanitizeInput(String input){
            Regex rgx = new Regex("(<[^>]+>)");
            input = rgx.Replace(input, " ");

            input = input.Replace("\r\n", "<br/>");

            return input;

        }
    }
}
