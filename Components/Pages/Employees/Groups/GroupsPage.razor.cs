using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using UniformProjectOmar.Helpers;
using UniformProjectOmar.Models;
using UniformProjectOmar.Repositories;
using UniformProjectOmar.Repositories.Interfaces;

namespace UniformProjectOmar.Components.Pages.Employees.Groups
{
    public partial class GroupsPage : ComponentBase
    {
        [Inject]
        private IGrupoRepository _grupoRepository { get; set; } = default!;

        [Inject]
        private ITipoEmpleadoRepository _tipoEmpleadoRepository { get; set; } = default!;

        [Inject]
        private IJSRuntime jSRuntime { get; set; } = default!;

        private List<TipoEmpleado>? TipoEmpleados;

        private List<Grupo>? Grupos;

        private Grupo Grupo { get; set; } = new();

        private bool IsGrupoModalOpen;

        protected override async Task OnInitializedAsync()
        {
            Grupos = await _grupoRepository.GetGrupos();
            TipoEmpleados = await _tipoEmpleadoRepository.GetTipoEmpleados();
        }

        private void OnGrupoModalOpenClose()
        {
            IsGrupoModalOpen = !IsGrupoModalOpen;
            Grupo = new();
        }

        private async Task EditArticuloAsync(string id)
        {
            var grupo = await _grupoRepository.GetGrupoById(id);

            if (grupo == null)
            {
                await jSRuntime.ToastrMessage("warning", "Registro No Existente!");
            }
            else
            {
                Grupo = new Grupo
                {
                    Id = grupo.Id,
                    Grupo1 = grupo.Grupo1,
                    IdTipoEmpleado = grupo.IdTipoEmpleado,                    
                };

                IsGrupoModalOpen = true;
            }
        }

        private async Task DeleteGrupoAsync(string id)
        {
            var isConfirmed = await jSRuntime.ShowSweetAlertConfirm("Estas seguro?", "El registro se borrara!");

            if (isConfirmed)
            {
                var response = await _grupoRepository.DeleteGrupo(id);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro No encontrado!");
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Eliminado Exitosamente!");

                    Grupos = await _grupoRepository.GetGrupos();
                    IsGrupoModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se elimino el registro, intente mas tarde!");
                }

            }
        }

        private async Task SaveGrupoAsync(Grupo grupo)
        {
            var existingGrupo = await _grupoRepository.GetGrupoById(grupo.Id);

            if (existingGrupo != null)
            {
                var response = await _grupoRepository.UpdateGrupo(grupo);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro Existente!");
                    Grupos = await _grupoRepository.GetGrupos();
                    IsGrupoModalOpen = false;
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Actualizado Exitosamente!");

                    Grupos = await _grupoRepository.GetGrupos();
                    IsGrupoModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se actualizo el registro, intente mas tarde!");
                }
            }
            else
            {
                var response = await _grupoRepository.CreateGrupo(grupo);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro Existente!");

                    Grupos = await _grupoRepository.GetGrupos();
                    IsGrupoModalOpen = false;
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Creado Exitosamente!");

                    Grupos = await _grupoRepository.GetGrupos();
                    IsGrupoModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se creo el registro, intente mas tarde!");
                }
            }
        }
    }
}
