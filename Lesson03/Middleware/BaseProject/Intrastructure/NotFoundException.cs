using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace BaseProject.Intrastructure
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string username): base($"the UserName is '{username ?? "<UnknownUser"));
        {
            
        }
    }
}
