using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using UniformProjectOmar.Helpers;
using UniformProjectOmar.Models;
using UniformProjectOmar.Repositories;
using UniformProjectOmar.Repositories.Interfaces;

namespace UniformProjectOmar.Components.Pages.Uniforms.ArticleTypes
{
    public partial class ArticleTypesPage : ComponentBase
    {        
        [Inject]
        private ITipoArticuloRepository _tipoArticuloRepository { get; set; } = default!;

        [Inject]
        private IJSRuntime jSRuntime { get; set; } = default!;

        public TipoArticulo TipoArticulo { get; set; } = new();

        private List<TipoArticulo>? TipoArticulos;        

        private bool IsTipoArticuloModalOpen;

        protected override async Task OnInitializedAsync()
        {
            TipoArticulos = await _tipoArticuloRepository.GetTipoArticulos();
        }        

        private void OnTipoArticuloModalOpenClose()
        {            
            IsTipoArticuloModalOpen = !IsTipoArticuloModalOpen;
            TipoArticulo = new();
        }

        private async Task EditTipoArticuloAsync(int id)
        {
            var tipoArticulo = await _tipoArticuloRepository.GetTipoArticuloById(id);

            if (tipoArticulo == null)
            {
                await jSRuntime.ToastrMessage("warning", "Registro No Existente!");
            }
            else
            {
                TipoArticulo = new TipoArticulo
                {
                    Id = tipoArticulo.Id,
                    Descripcion = tipoArticulo.Descripcion,
                    Aplicacion = tipoArticulo.Aplicacion
                };

                IsTipoArticuloModalOpen = true;
            }
        }

        private async Task SaveTipoArticuloAsync(TipoArticulo tipoArticulo)
        {
            if (tipoArticulo.Id > 0)
            {
                var response = await _tipoArticuloRepository.UpdateTipoArticulo(tipoArticulo);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro Existente!");
                    TipoArticulos = await _tipoArticuloRepository.GetTipoArticulos();
                    IsTipoArticuloModalOpen = false;
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Actulizado Exitosamente!");

                    TipoArticulos = await _tipoArticuloRepository.GetTipoArticulos();
                    IsTipoArticuloModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se actualizo el registro, intente mas tarde!");
                }
            }
            else 
            {
                var response = await _tipoArticuloRepository.CreateTipoArticulo(tipoArticulo);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro Existente!");
                    TipoArticulos = await _tipoArticuloRepository.GetTipoArticulos();
                    IsTipoArticuloModalOpen = false;
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Creado Exitosamente!");

                    TipoArticulos = await _tipoArticuloRepository.GetTipoArticulos();
                    IsTipoArticuloModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se creo el registro, intente mas tarde!");
                }
            }                
        }

        private async Task DeleteTipoArticulo(int id) 
        {      
            var isConfirmed = await jSRuntime.ShowSweetAlertConfirm("Estas seguro?", "El registro se borrara!");

            if (isConfirmed)
            {
                var response = await _tipoArticuloRepository.DeleteTipoArticulo(id);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro No encontrado!");
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Eliminado Exitosamente!");

                    TipoArticulos = await _tipoArticuloRepository.GetTipoArticulos();
                    IsTipoArticuloModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se elimino el registro, intente mas tarde!");
                }
            
            }
        }
    }
}
