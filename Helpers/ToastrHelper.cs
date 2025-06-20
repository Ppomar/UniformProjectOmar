using Microsoft.JSInterop;

namespace UniformProjectOmar.Helpers
{
    public static class ToastrHelper
    {        
        public static async ValueTask ToastrMessage(this IJSRuntime JSRuntime, string type, string message)
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", type, message);
        }
    }
}
