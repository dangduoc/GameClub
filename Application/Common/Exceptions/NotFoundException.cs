﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException(string? Message = null) : base(Message)
    {

    }
}