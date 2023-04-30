using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDbManager.Application.Utilities.Helpers
{
    public static class MaskingHelper
    {
        /// <summary>
        /// Mask the string.
        /// </summary>
        /// <param name="value">String that need to be masked</param>
        /// <param name="startIndex">zero index indicating mask start position</param>
        /// <param name="mask">mask that need to be applied, eg. ***</param>
        /// <returns>Usage: "123456789".Mask(3, "****") => "123****89"</returns>
        public static string MaskWithStar(this string value, int startIndex, string mask)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            var result = value;
            var starLength = mask.Length;

            if (value.Length < startIndex) return result;

            result = value.Insert(startIndex, mask);

            result = result.Length >= (startIndex + (starLength * 2)) ? result.Remove(startIndex + starLength, starLength) : result.Remove(startIndex + starLength, result.Length - (startIndex + starLength));

            return result;
        }

    }
}
