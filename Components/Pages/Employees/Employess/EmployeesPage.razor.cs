using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using UniformProjectOmar.Helpers;
using UniformProjectOmar.Models;
using UniformProjectOmar.Repositories;
using UniformProjectOmar.Repositories.Interfaces;

namespace UniformProjectOmar.Components.Pages.Employees.Employess
{
    public partial class EmployeesPage : ComponentBase
    {
        [Inject]
        private IEmpleadoRepository _empleadoRepository { get; set; } = default!;

        [Inject]
        private IGrupoRepository _grupoRepository { get; set; } = default!;

        [Inject]
        private IJSRuntime jSRuntime { get; set; } = default!;

        private List<Empleado>? Empleados;

        private List<Grupo>? Grupos;

        private Empleado? Empleado;

        private bool IsEmpleadoModalOpen;

        private void OnEmpleadoModalOpenClose() 
        {
            IsEmpleadoModalOpen = !IsEmpleadoModalOpen;
            Empleado = new();
        }

        protected override async Task OnInitializedAsync()
        {
            Empleados = await _empleadoRepository.GetEmpleados();
            Grupos = await _grupoRepository.GetGrupos();
        }

        private async Task EditEmpleadoAsync(int id)
        {
            var empleado = await _empleadoRepository.GetEmpleadoById(id);

            if (empleado == null)
            {
                await jSRuntime.ToastrMessage("warning", "Registro No Existente!");
            }
            else
            {
                Empleado = new Empleado
                {
                    Id = empleado.Id,
                    IdGrupo = empleado.IdGrupo,
                    NombreEmpleado = empleado.NombreEmpleado
                };

                IsEmpleadoModalOpen = true;
            }
        }

        private async Task DeleteEmpleadoAsync(int id)
        {
            var isConfirmed = await jSRuntime.ShowSweetAlertConfirm("Estas seguro?", "El registro se borrara!");

            if (isConfirmed)
            {
                var response = await _empleadoRepository.DeleteEmpleado(id);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro No encontrado!");
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Eliminado Exitosamente!");

                    Empleados = await _empleadoRepository.GetEmpleados();
                    IsEmpleadoModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se elimino el registro, intente mas tarde!");
                }

            }
        }

        private async Task SaveEmpleadoAsync(Empleado empleado)
        {
            if (empleado.Id > 0)
            {
                var response = await _empleadoRepository.UpdateEmpleado(empleado);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro Existente!");
                    Empleados = await _empleadoRepository.GetEmpleados();
                    IsEmpleadoModalOpen = false;
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Actualizado Exitosamente!");

                    Empleados = await _empleadoRepository.GetEmpleados();
                    IsEmpleadoModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se actualizo el registro, intente mas tarde!");
                }
            }
            else
            {
                var response = await _empleadoRepository.CreateEmpleado(empleado);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro Existente!");

                    Empleados = await _empleadoRepository.GetEmpleados();
                    IsEmpleadoModalOpen = false;
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Creado Exitosamente!");

                    Empleados = await _empleadoRepository.GetEmpleados();
                    IsEmpleadoModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se creo el registro, intente mas tarde!");
                }
            }
        }
    }
}
