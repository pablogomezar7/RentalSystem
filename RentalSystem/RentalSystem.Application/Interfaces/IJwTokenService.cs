﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(string username, string role);
    }

}
