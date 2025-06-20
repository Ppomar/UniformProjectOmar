using Microsoft.AspNetCore.Components;
using UniformProjectOmar.Models;

namespace UniformProjectOmar.Components.Pages.Uniforms.Movements.Components
{
    public partial class CreateMovementModal : ComponentBase
    {
        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        [Parameter]
        public EventCallback<CrearMovimiento> OnSave { get; set; }

        [Parameter]
        public CrearMovimiento Movimiento { get; set; } = new();

        [Parameter]
        public List<Empleado>? Empleados { get; set; }

        [Parameter]
        public List<Articulo>? Articulos { get; set; }

        private async Task OnCloseClicked()
        {
            await OnClose.InvokeAsync();
        }

        private async Task OnSaveClicked()
        {
            await OnSave.InvokeAsync(Movimiento);
        }
    }
}
