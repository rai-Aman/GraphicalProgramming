﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPL_Application
{
    public interface iterator
    {
        bool HasNext();
        Object next();
    }
}
