using Microsoft.AspNetCore.Components;
using UniformProjectOmar.Models;

namespace UniformProjectOmar.Components.Pages.Employees.Groups.Components
{
    public partial class CreateGrupoModal : ComponentBase
    {
        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        [Parameter]
        public EventCallback<Grupo> OnSave { get; set; }

        [Parameter]
        public Grupo Grupo { get; set; } = new();

        [Parameter]
        public List<TipoEmpleado>? TipoEmpleados { get; set; }

        private async Task OnCloseClicked()
        {
            await OnClose.InvokeAsync();
        }

        private async Task OnSaveClicked()
        {
            await OnSave.InvokeAsync(Grupo);
        }
    }
}
