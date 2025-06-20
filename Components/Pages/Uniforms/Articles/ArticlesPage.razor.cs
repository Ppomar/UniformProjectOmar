using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using UniformProjectOmar.Helpers;
using UniformProjectOmar.Models;
using UniformProjectOmar.Repositories.Interfaces;

namespace UniformProjectOmar.Components.Pages.Uniforms.Articles
{
    public partial class ArticlesPage : ComponentBase
    {
        [Inject]
        private IArticuloRepository _articuloRepository { get; set; } = default!;

        [Inject]
        private ITipoArticuloRepository _tipoArticuloRepository { get; set; } = default!;

        [Inject]
        private IJSRuntime jSRuntime { get; set; } = default!;
        public Articulo Articulo { get; set; } = new();

        private List<Articulo>? Articulos;

        public TipoArticulo TipoArticulo { get; set; } = new();


        private List<TipoArticulo>? TipoArticulos;

        private bool IsArticuloModalOpen;

        private bool IsTipoArticuloModalOpen;

        protected override async Task OnInitializedAsync()
        {
            TipoArticulos = await _tipoArticuloRepository.GetTipoArticulos();
            Articulos = await _articuloRepository.GetArticulos();
        }

        private async Task SaveArticuloAsync(Articulo articulo)
        {
            if (articulo.Id > 0)
            {
                var response = await _articuloRepository.UpdateArticulo(articulo);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro Existente!");                                        
                    Articulos = await _articuloRepository.GetArticulos();
                    IsArticuloModalOpen = false;
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Actualizado Exitosamente!");

                    Articulos = await _articuloRepository.GetArticulos();
                    IsArticuloModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se actualizo el registro, intente mas tarde!");
                }                
            }
            else 
            {
                var response = await _articuloRepository.CreateArticulo(articulo);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro Existente!");

                    Articulos = await _articuloRepository.GetArticulos();
                    IsArticuloModalOpen = false;
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Creado Exitosamente!");

                    Articulos = await _articuloRepository.GetArticulos();
                    IsArticuloModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se creo el registro, intente mas tarde!");
                }
            }            
        }

        private async Task SaveTipoArticuloAsync(TipoArticulo tipoArticulo)
        {
            var response = await _tipoArticuloRepository.CreateTipoArticulo(tipoArticulo);

            if (response == null)
            {
                await jSRuntime.ToastrMessage("warning", "Registro Existente!");
            }
            else if (response == true)
            {
                await jSRuntime.ToastrMessage("success", "Creado Exitosamente!");

                Articulos = await _articuloRepository.GetArticulos();
                IsTipoArticuloModalOpen = false;
            }
            else 
            {
                await jSRuntime.ToastrMessage("error", "No se creo el registro, intente mas tarde!");
            }

        }

        private async Task EditArticuloAsync(int id) 
        {
            var articulo = await _articuloRepository.GetArticuloById(id);

            if (articulo == null)
            {
                await jSRuntime.ToastrMessage("warning", "Registro No Existente!");
            }
            else 
            {
                Articulo = new Articulo
                {
                    Id = articulo.Id,
                    Descripcion = articulo.Descripcion,
                    IdTipoArticulo = articulo.IdTipoArticulo,
                    UnidadTalla = articulo.UnidadTalla
                };

                IsArticuloModalOpen = true;
            }
        } 

        private async Task DeleteArticulo(int id)
        {
            var isConfirmed = await jSRuntime.ShowSweetAlertConfirm("Estas seguro?", "El registro se borrara!");

            if (isConfirmed)
            {
                var response = await _articuloRepository.DeleteArticulo(id);

                if (response == null)
                {
                    await jSRuntime.ToastrMessage("warning", "Registro No encontrado!");
                }
                else if (response == true)
                {
                    await jSRuntime.ToastrMessage("success", "Eliminado Exitosamente!");

                    Articulos = await _articuloRepository.GetArticulos();
                    IsTipoArticuloModalOpen = false;
                }
                else
                {
                    await jSRuntime.ToastrMessage("error", "No se elimino el registro, intente mas tarde!");
                }

            }
        }

        private void OnArticuloModalOpenClose()
        {
            IsArticuloModalOpen = !IsArticuloModalOpen;
            Articulo = new();
        }

        private void OnTipoArticuloModalOpenClose()
        {
            IsTipoArticuloModalOpen = !IsTipoArticuloModalOpen;
            TipoArticulo = new();
        }
    }
}
