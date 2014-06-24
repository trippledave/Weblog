using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Weblog.Core.Helpers
{
     public static class CaptchaHelper
    {

        private const string SESSIONKEY_CAPTCHAVALUE = "{229B42D4-E964-4D45-AB09-F9CE548F1F0A}";
        private const string BASESTRING_CAPTCHARESULT = "{0} + {1} =";
        private static Random _random = new Random();

        public static string GenerateSimpleCaptcha()
        {
            int firstValue = _random.Next( 1, 20 );
            int secondValue = _random.Next( 1, 20 );
            int captchaValue = firstValue + secondValue;

            HttpContext.Current.Session[SESSIONKEY_CAPTCHAVALUE] = captchaValue;

            string result = String.Format( BASESTRING_CAPTCHARESULT, firstValue, secondValue );
            return result;
        }

        public static bool CheckCaptcha( int enteredValue )
        {
            try
            {
                int captchaValue = (int)HttpContext.Current.Session[SESSIONKEY_CAPTCHAVALUE];
                bool result = captchaValue == enteredValue;
                return result;
            }
            catch ( Exception)
            {
                return false;
            }
            finally
            {
                HttpContext.Current.Session[SESSIONKEY_CAPTCHAVALUE] = null;
            }
        }
    }
}
