using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Weblog.Core.Modules
{
    /// <summary>
    /// Custom HTTP Module for Cloaking IIS7 Server Settings to allow anonymity
    /// </summary>
    public class CloakingHttpModule : IHttpModule
    {
        /// <summary>
        /// List of Headers to remove
        /// </summary>
        private readonly List<string> _cloakedHeaders;
        private readonly NameValueCollection _addedHeaders;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloakingHttpModule"/> class.
        /// </summary>
        public CloakingHttpModule()
        {
            this._cloakedHeaders = new List<string>
                                      {
                                              "Server",
                                              "X-AspNet-Version",
                                              "X-AspNetMvc-Version",
                                              "X-Powered-By"
                                      };

            this._addedHeaders = new NameValueCollection
            {
                {"X-Frame-Options", "deny"},
                {"X-XSS-Protection", "1; mode=block"},
                {"X-Content-Type-Options", "nosniff"}
            };
        }

        /// <summary>
        /// Dispose the Custom HttpModule. Nothing to do here, but must be implemented
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Handles the current request.
        /// </summary>
        /// <param name="context">
        /// The HttpApplication context.
        /// </param>
        public void Init( HttpApplication context )
        {
            context.PreSendRequestHeaders += this.OnPreSendRequestHeaders;
        }

        /// <summary>
        /// Removes all headers in the <see cref="_cloakedHeaders"/> list from the HTTP Response.
        /// </summary>
        /// <param name="sender">
        /// The object raising the event
        /// </param>
        /// <param name="e">
        /// The event data.
        /// </param>
        private void OnPreSendRequestHeaders( object sender, EventArgs e )
        {
            this._cloakedHeaders.ForEach( h => HttpContext.Current.Response.Headers.Remove( h ) );
            HttpContext.Current.Response.Headers.Add( this._addedHeaders );
        }
    }
}
