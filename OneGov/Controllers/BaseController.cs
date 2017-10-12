using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CBNFormQ.Controllers
{
    public class BaseController : Controller
    {
        protected string HelloSignAPIKey = ConfigurationManager.AppSettings["963e3f89ccda3b5a5c475023f786bdcfa86edd82df8a93effc8da98ec6e7cd75"];
        protected string HelloSignClientID = ConfigurationManager.AppSettings["1c9ebe55dcf8627c563bfa59061b2e54"];
        
    }
}