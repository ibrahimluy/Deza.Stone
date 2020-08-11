using System;
using System.ComponentModel.DataAnnotations;

namespace Deza.Stone
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
