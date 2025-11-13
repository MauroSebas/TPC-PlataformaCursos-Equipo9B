using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace Vistas
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition(

                 "jquery",
                 new ScriptResourceDefinition
                 {
                     Path = "~/Scripts/jquery-3.6.0.min.js",
                     DebugPath = "~/Scripts/jquery-3.6.0.js",
                     CdnPath = "https://code.jquery.com/jquery-3.6.0.min.js",
                     CdnDebugPath = "https://code.jquery.com/jquery-3.6.0.min.js",
                     LoadSuccessExpression = "window.jQuery"
                 }
            );
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;

            FormsAuthenticationTicket ticket = null;
            try { ticket = FormsAuthentication.Decrypt(authCookie.Value); }
            catch { return; }
            if (ticket == null) return;

            string rolesData = ticket.UserData ?? "";
            string[] roles = string.IsNullOrEmpty(rolesData) ? new string[0] : rolesData.Split(',');
            Context.User = new System.Security.Principal.GenericPrincipal(
                new System.Security.Principal.GenericIdentity(ticket.Name),
                roles
            );
        }
    }
}