using GastroLink.DTO;
using GastroLink.Facade.Interface;
using GastroLink.Mapper;
using GastroLink.Models;
using GastroLink.Service.Interfaces;
using GastroLink.Settings;
using GastroLink.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GastroLink.Controllers {
    public class PedidoController : Controller {
        private readonly IFacadeCardapio _facadeCardapio;
        private readonly IRascunhoPedidoService _rascunhoPedidoService;
        private readonly IFacadePrato _facadePrato;
        private readonly ApiGastroLinkSettings _apiSettings;
        private readonly IFacadePedido _facadePedido;

        public PedidoController(IFacadeCardapio facadeCardapio, IRascunhoPedidoService rascunhoPedidoService, IFacadePrato facadePrato, IOptions<ApiGastroLinkSettings> apiSettings, IFacadePedido facadePedido) {
            _facadeCardapio = facadeCardapio;
            _rascunhoPedidoService = rascunhoPedidoService;
            _facadePrato = facadePrato;
            _facadePedido = facadePedido;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> CriarPedido(int idMesa) {
            var cardapio = await _facadeCardapio.SelecionarCardapio();
           
            foreach (var categoria in cardapio) {
                DefinirCaminhoImagem(categoria.Pratos);
            }

            var criarPedidoViewModel = new CriarPedidoViewModel {
                idMesa = idMesa,
                listCategoriaPrato = cardapio
            };
            return View(criarPedidoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarItemRascunho([FromBody]AdicionarItemRascunhoDTO dto) {
            var pedidoRascunho = await _rascunhoPedidoService.ObterRascunhoPedido(dto.mesaId);

            try {
                if (pedidoRascunho == null) {
                    pedidoRascunho = new RascunhoPedido { MesaId = dto.mesaId, Itens = new List<RascunhoItemPedido>() };
                }

                var prato = await _facadePrato.BuscarPratoPorId(dto.RascunhoItemPedido.Prato.Id);
                dto.RascunhoItemPedido.Preco = prato.Preco;

                pedidoRascunho.Itens.Add(dto.RascunhoItemPedido);
                await _rascunhoPedidoService.SalvarRascunho(pedidoRascunho);

                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> ObterQuantidadeItensRascunhoPedido(int idMesa) {
            var pedidoRascunho = await _rascunhoPedidoService.ObterRascunhoPedido(idMesa);
            if (pedidoRascunho == null) {
                return Ok(0);
            }
            return Ok(_rascunhoPedidoService.ObterQuantidadePratos(pedidoRascunho.Itens));
        }

        [HttpGet] 
        public async Task<IActionResult> ResumoPedido(int idMesa) {
            var pedidoCacheRedis = await _rascunhoPedidoService.ObterRascunhoPedido(idMesa);
            if (pedidoCacheRedis == null) {
                return NotFound();
            }

            var PedidoRascunho = new RascunhoPedido {
                MesaId = pedidoCacheRedis.MesaId,
            };

            foreach (var item in pedidoCacheRedis.Itens) {
                var prato = await _facadePrato.BuscarPratoPorId(item.Prato.Id);
                DefinirCaminhoImagem(prato);
                item.Prato = prato;
                PedidoRascunho.Itens.Add(item);
            }

            return View(PedidoRascunho);
        }

        [HttpPost]
        public async Task<IActionResult> GerarPedido([FromBody]int idMesa) {
            try {
                var pedidoRascunho = await _rascunhoPedidoService.ObterRascunhoPedido(idMesa);

                var pedido = PedidoMapper.RascunhoToPedidoCreateDTO(pedidoRascunho);

                pedido.IdUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0;

                if(await _facadePedido.CadastrarPedido(pedido)) {
                    await _rascunhoPedidoService.RemoverRascunho(idMesa);
                } else {
                    return BadRequest();
                }

            }catch(Exception ex) {
                return BadRequest();
            }
            return Ok();
        }
        
        private void DefinirCaminhoImagem(List<Prato> listPratos) {
            foreach (var prato in listPratos) {
                prato.UrlImagem = $"{_apiSettings.BaseUrlImagem}{prato.UrlImagem}";
            }
        }

        private void DefinirCaminhoImagem(Prato Prato) {
            Prato.UrlImagem = $"{_apiSettings.BaseUrlImagem}{Prato.UrlImagem}";
        }
    }
}
