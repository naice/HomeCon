using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCon.Model.Data
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class PrimaryKeyAttribute : Attribute
    {
        public PrimaryKeyAttribute()
        {
        }
    }
}
