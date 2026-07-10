using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Linx.LinqExtensions
{
    public interface IContext
    {
        DbContext Context { get; }
    }
}
