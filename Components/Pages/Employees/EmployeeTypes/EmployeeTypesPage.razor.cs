using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using UniformProjectOmar.Helpers;
using UniformProjectOmar.Models;
using UniformProjectOmar.Repositories;
using UniformProjectOmar.Repositories.Interfaces;

namespace UniformProjectOmar.Components.Pages.Employees.EmployeeTypes
{
    public partial class EmployeeTypesPage : ComponentBase
    {
        [Inject]
        private ITipoEmpleadoRepository _tipoEmpleadoRepository { get; set; } = default!;

        [Inject]
        private IJSRuntime jSRuntime { get; set; } = default!;

        private List<TipoEmpleado>? TipoEmpleados;

        private TipoEmpleado TipoEmpleado { get; set; } = new();

        private bool IsTipoEmpleadoModalOpen;

        protected override async Task OnInitializedAsync()
        {
            TipoEmpleados = await _tipoEmpleadoRepository.GetTipoEmpleados();
        }

        private void OnTipoEmpleadoOpenClose() 
        {
            IsTipoEmpleadoModalOpen = !IsTipoEmpleadoModalOpen;
            TipoEmpleado = new();
        }

        private async Task EditTipoEmpleadoAsync(int id)
        {
            var tipoEmpleado = await _tipoEmpleadoRepository.GetTipoEmpleadoById(id);

            if (tipoEmpleado == null)
            {
                await jSRuntime.ToastrMessage("warning", "Registro No Existente!");
            }
            else
            {
                TipoEmpleado = new TipoEmpleado
                {
                    Id = tipoEmpleado.Id,
                    Tipo = tipoEmpleado.Tipo,
                };

                IsTipoEmpleadoModalOpen = true;
            }
        }

        private async Task DeleteTipoEmpleadoAsync(int id)
        {
            var isConfirmed = await jSRuntime.ShowSweetAlertConfirm("Estas seguro?", "El registro se borrara!");

            if (isConfirmed)
            {
                var response = await _tipoEmpleadoRepository.DeleteTipoEmpleado(id);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro No encontrado!");
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Eliminado Exitosamente!");

                    TipoEmpleados = await _tipoEmpleadoRepository.GetTipoEmpleados();
                    IsTipoEmpleadoModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se elimino el registro, intente mas tarde!");
                }

            }
        }

        private async Task SaveTipoEmpleadoAsync(TipoEmpleado tipoEmpleado)
        {
            if (tipoEmpleado.Id > 0)
            {
                var response = await _tipoEmpleadoRepository.UpdateTipoEmpleado(tipoEmpleado);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro Existente!");
                    TipoEmpleados = await _tipoEmpleadoRepository.GetTipoEmpleados();
                    IsTipoEmpleadoModalOpen = false;
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Actualizado Exitosamente!");

                    TipoEmpleados = await _tipoEmpleadoRepository.GetTipoEmpleados();
                    IsTipoEmpleadoModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se actualizo el registro, intente mas tarde!");
                }
            }
            else
            {
                var response = await _tipoEmpleadoRepository.CreateTipoEmpleado(tipoEmpleado);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro Existente!");

                    TipoEmpleados = await _tipoEmpleadoRepository.GetTipoEmpleados();
                    IsTipoEmpleadoModalOpen = false;
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Creado Exitosamente!");

                    TipoEmpleados = await _tipoEmpleadoRepository.GetTipoEmpleados();
                    IsTipoEmpleadoModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se creo el registro, intente mas tarde!");
                }
            }
        }
    }
}
