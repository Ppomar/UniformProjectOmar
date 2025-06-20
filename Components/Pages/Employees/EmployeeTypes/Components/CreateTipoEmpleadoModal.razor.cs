using Microsoft.AspNetCore.Components;
using UniformProjectOmar.Models;

namespace UniformProjectOmar.Components.Pages.Employees.EmployeeTypes.Components
{
    public partial class CreateTipoEmpleadoModal : ComponentBase
    {
        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        [Parameter]
        public EventCallback<TipoEmpleado> OnSave { get; set; }

        [Parameter]
        public TipoEmpleado TipoEmpleado { get; set; } = new();

        private async Task OnCloseClicked()
        {
            await OnClose.InvokeAsync();
        }

        private async Task OnSaveClicked()
        {
            await OnSave.InvokeAsync(TipoEmpleado);
        }
    }
}
