using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Weblog.Core.DataAccess.Weblog
{
    public partial class WeblogDataContext
    {

        #region Request-wide Singleton

        private const string CONTEXTKEY_DATACONTEXT = "{CD01E2F2-3893-4580-8D19-9C948B6F33BD}";

        public static WeblogDataContext Current
        {
            get
            {
                WeblogDataContext result =
                        HttpContext.Current.Items[CONTEXTKEY_DATACONTEXT] as WeblogDataContext;
                if (result == null)
                {
                    result = new WeblogDataContext();
                    HttpContext.Current.Items.Add(CONTEXTKEY_DATACONTEXT, result);
                }
                return result;
            }
        }

        #endregion

    }
}
