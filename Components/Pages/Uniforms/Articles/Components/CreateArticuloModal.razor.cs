using Microsoft.AspNetCore.Components;
using UniformProjectOmar.Models;

namespace UniformProjectOmar.Components.Pages.Uniforms.Articles.Components
{
    public partial class CreateArticuloModal : ComponentBase
    {
        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        [Parameter]
        public EventCallback<Articulo> OnSave { get; set; }

        [Parameter]
        public Articulo Articulo { get; set; } = new();

        [Parameter]
        public List<TipoArticulo>? TipoArticulos { get; set; }

        private async Task OnCloseClicked()
        {
            await OnClose.InvokeAsync();
        }

        private async Task OnSaveClicked()
        {
            await OnSave.InvokeAsync(Articulo);
        }
    }
}
