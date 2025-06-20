using Microsoft.AspNetCore.Components;
using UniformProjectOmar.Models;
using UniformProjectOmar.Repositories.Interfaces;

namespace UniformProjectOmar.Components.Pages.Uniforms.Movements
{
    public partial class MovementsPage : ComponentBase
    {
        [Inject]
        private IUniformsRepository _uniformsRepository { get; set; } = default!;

        private List<Movimiento>? Movimientos;

        private CrearMovimiento NuevoMovimiento = new();

        protected override async Task OnInitializedAsync()
        {
            Movimientos = await _uniformsRepository.GetMovesAsync();
        }

        private bool IsModalOpen;

        private void OnModalOpenClose()
        {
            IsModalOpen = !IsModalOpen;
        }

        private async Task SaveMovimientoAsync(CrearMovimiento movimiento)
        {
            await _uniformsRepository.CreateMovement(movimiento);

            Movimientos = await _uniformsRepository.GetMovesAsync(); 

            IsModalOpen = false;
        }
    }
}
