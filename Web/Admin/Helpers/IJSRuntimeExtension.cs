using Microsoft.JSInterop;

namespace Admin.Helpers;

public static class IJSRuntimeExtension
{
    public static async ValueTask ToastrSuccess(this IJSRuntime jsRuntime, string msg)
    {
        await jsRuntime.InvokeVoidAsync("ShowToastr", "success", msg);
    }
    public static async ValueTask ToastrError(this IJSRuntime jsRuntime, string msg)
    {
        await jsRuntime.InvokeVoidAsync("ShowToastr", "error", msg);
    }
}