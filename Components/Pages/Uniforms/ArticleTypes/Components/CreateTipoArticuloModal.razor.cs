using Microsoft.AspNetCore.Components;
using UniformProjectOmar.Models;

namespace UniformProjectOmar.Components.Pages.Uniforms.ArticleTypes.Components
{
    public partial class CreateTipoArticuloModal : ComponentBase
    {
        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        [Parameter]
        public EventCallback<TipoArticulo> OnSave { get; set; }

        [Parameter]
        public TipoArticulo TipoArticulo { get; set; } = new();       

        private async Task OnCloseClicked()
        {
            await OnClose.InvokeAsync();
            TipoArticulo = new();
        }

        private async Task OnSaveClicked()
        {
            await OnSave.InvokeAsync(TipoArticulo);
        }
    }
}
