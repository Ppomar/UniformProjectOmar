using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.RegularExpressions;
using UniformProjectOmar.Helpers;
using UniformProjectOmar.Models;
using UniformProjectOmar.Repositories;
using UniformProjectOmar.Repositories.Interfaces;

namespace UniformProjectOmar.Components.Pages.Uniforms.Movements
{
    public partial class MovementsPage : ComponentBase
    {
        [Inject]
        private IUniformsRepository _uniformsRepository { get; set; } = default!;

        [Inject]
        private IEmpleadoRepository _empleadoRepository { get; set; } = default!;

        [Inject]
        private IArticuloRepository _articuloRepository { get; set; } = default!;

        [Inject]
        private IJSRuntime jSRuntime { get; set; } = default!;

        private List<MovimientoVw>? Movimientos;

        private CrearMovimiento NuevoMovimiento = new();

        private List<Empleado>? Empleados;

        private List<Articulo>? Articulos;


        protected override async Task OnInitializedAsync()
        {
            Empleados = await _empleadoRepository.GetEmpleados();
            Movimientos = await _uniformsRepository.GetMoves();
            Articulos = await _articuloRepository.GetArticulos();
        }

        private bool IsModalOpen;

        private void OnModalOpenClose()
        {
            IsModalOpen = !IsModalOpen;
            NuevoMovimiento = new();
        }

        private async Task DeleteMovimientoAsync(int id)
        {
            var isConfirmed = await jSRuntime.ShowSweetAlertConfirm("Estas seguro?", "El registro se borrara!");

            if (isConfirmed)
            {
                var response = await _uniformsRepository.DeleteMovimiento(id);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro No encontrado!");
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Eliminado Exitosamente!");

                    Movimientos = await _uniformsRepository.GetMoves();
                    IsModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se elimino el registro, intente mas tarde!");
                }

            }
        }

        private async Task EditMovimientoAsync(int id)
        {
            var movimiento = await _uniformsRepository.GetMovimientoById(id);

            if (movimiento == null)
            {
                await jSRuntime.ToastrMessage("warning", "Registro No Existente!");
            }
            else
            {
                NuevoMovimiento = new CrearMovimiento
                {
                    Id = movimiento.Id,
                    IdArticulo = movimiento.IdArticulo.Value,
                    IdEmpleado = movimiento.IdEmpleado.Value,
                    Talla = movimiento.Talla,
                    Cantidad = movimiento.Cantidad.Value
                };

                IsModalOpen = true;
            }
        }

        private async Task SaveMovimientoAsync(CrearMovimiento movimiento)
        {
            if (movimiento.Id > 0)
            {
                var response = await _uniformsRepository.UpdateMovimiento(movimiento);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("error", "No se actualizo el registro, intente mas tarde!");
                    Movimientos = await _uniformsRepository.GetMoves();
                    IsModalOpen = false;
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Actualizado Exitosamente!");

                    Movimientos = await _uniformsRepository.GetMoves();
                    IsModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se actualizo el registro, intente mas tarde!");
                }
            }
            else 
            {
                var response = await _uniformsRepository.CreateMovimiento(movimiento);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro Existente!");

                    Movimientos = await _uniformsRepository.GetMoves();
                    IsModalOpen = false;
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Creado Exitosamente!");

                    Movimientos = await _uniformsRepository.GetMoves();
                    IsModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se creo el registro, intente mas tarde!");
                }
            }
        }
    }
}
