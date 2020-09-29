using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorFormSample.Client.SharedComponent
{
    public class InputFlexSelect<TValue> : InputSelect<TValue>
    {
        // from https://www.pragimtech.com/blog/blazor/inputselect-does-not-support-system.int32/
        // and https://chrissainty.com/building-custom-input-components-for-blazor-using-inputbase/
        protected override bool TryParseValueFromString(string value, out TValue result,
            out string validationErrorMessage)
        {
            if (typeof(TValue) == typeof(Guid))
            {
                if (Guid.TryParse(value, out var resultGuid))
                {
                    result = (TValue)(object)resultGuid;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    result = default;
                    validationErrorMessage =
                        $"The selected value {value} is not a valid GUID.";
                    return false;
                }
            }
            else
            {
                return base.TryParseValueFromString(value, out result,
                    out validationErrorMessage);
            }
        }
    }
}
