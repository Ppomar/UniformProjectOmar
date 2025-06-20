using Microsoft.JSInterop;

namespace UniformProjectOmar.Helpers
{
    public static class SweetAlertHelper
    {
        public static async Task<bool> ShowSweetAlertConfirm(this IJSRuntime jSRuntime, string title, string content)
        {
            try
            {
                var response = await jSRuntime.InvokeAsync<bool>("SweetAlertHelper.showConfirmation", title, content);
                return response;
            }
            catch (JSException ex)
            {
                Console.WriteLine($"JS Error: {ex.Message}");
                return false;
            }
        }
    }
}
