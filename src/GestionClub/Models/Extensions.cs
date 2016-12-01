using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionClub.Models
{
    static public class Extensions
    {
        static public bool IsAjaxRequest(this HttpRequest request)
        {
            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            else
                return false;
        }
    }
}
