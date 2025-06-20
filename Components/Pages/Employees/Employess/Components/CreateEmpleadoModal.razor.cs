using Microsoft.AspNetCore.Components;
using UniformProjectOmar.Models;

namespace UniformProjectOmar.Components.Pages.Employees.Employess.Components
{
    public partial class CreateEmpleadoModal : ComponentBase
    {
        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        [Parameter]
        public EventCallback<Empleado> OnSave { get; set; }
      
        [Parameter]
        public Empleado Empleado { get; set; } = new();

        [Parameter]
        public List<Grupo>? Grupos { get; set; } = new();

        private async Task OnCloseClicked()
        {
            await OnClose.InvokeAsync();
        }

        private async Task OnSaveClicked()
        {
            await OnSave.InvokeAsync(Empleado);
        }
    }
}
