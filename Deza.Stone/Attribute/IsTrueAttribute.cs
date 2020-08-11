using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Deza.Stone.Attribute
{
    public class IsTrueAttribute : ValidationAttribute
    {
        #region Overrides of ValidationAttribute

        public override bool IsValid(object value)
        {
            if (value == null) return false;
            if (value.GetType() != typeof(bool)) throw new InvalidOperationException("can only be used on boolean properties.");

            return (bool)value;
        }

        #endregion
    }
}
