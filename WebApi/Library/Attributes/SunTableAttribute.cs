using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Library.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SunTableAttribute : Attribute
    {
        private string _name = null;
        public SunTableAttribute(string name)
        {

            this._name = name;
        }

        public string GetName()
        {
            return this._name;
        }
    }
}