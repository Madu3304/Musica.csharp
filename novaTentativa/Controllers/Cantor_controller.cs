using Microsoft.AspNetCore.Mvc;
using novaTentativa.Server;
using Newtonsoft.Json;


namespace novaTentativa.Controllers
{
    public class Cantor_controller : ControllerBase
    {
        private readonly Cantor_Server _cantor_Server; //injeção 

        public Cantor_controller(Cantor_Server cantor_Server)
        {
            _cantor_Server = cantor_Server;
        }


        //##################################
        //cantor - musica
        [HttpGet("GetMusicas")]
        public async Task<IActionResult> GetFiltroCantores(string? musica = null)
        {
            var cantores = await _cantor_Server.GetCantoresAsync();
            var filtros = cantores.Where(m => (m.Song.Contains(musica, StringComparison.OrdinalIgnoreCase)));
            var musicaFiltro = string.Join(",", filtros.Select(m => m.Song));

            var Filtrados = cantores.Where(c => (c.Song == null || c.Song?.ToLower().Contains(c.Song) == true));
            var resultado = filtros.Select(m => $"Artista: {m.Artist}, Música: {m.Song}, Gênero: {m.Genre}").ToList();
            return Ok(resultado);
        }


        [HttpGet("GetTodosGeneros")]
        public async Task<IActionResult> GetGeneros()
        {
            var arti = await _cantor_Server.GetCantoresAsync();
            var gene = arti.Select(c => c.Genre?.ToLower()).Where(gene => !string.IsNullOrEmpty(gene)).Distinct().ToList();

            return Ok(gene);
        }


        //##################################
        //genero
        [HttpGet("GetGenero")]
        public async Task<IActionResult> GetFiltroGenero(string? genero = null)
        {
            //linq == where, select 
            var cantores = await _cantor_Server.GetCantoresAsync();
            var generosFil = cantores.Where(c => (c.Genre.Contains(genero, StringComparison.OrdinalIgnoreCase)));
            var generofiltrado = string.Join(", ", generosFil.Select(c => c.Genre));

            var gene = cantores.Where(c => (c.Genre == null || c.Genre?.ToLower().Contains(c.Genre) == true));
            var resultado = generosFil.Select(g => $"Gêneros: {g.Genre}, Artista: {g.Artist}").ToList();

            return Ok(resultado);
        }



        //##################################
        //cantor ordenado
        [HttpGet("Getartistaordenado")]
        public async Task<IActionResult> GetCantoresOrdenados()
        {
            try
            {
                var cantores = await _cantor_Server.GetCantoresAsync();
                var cantoresOrdenados = cantores.OrderBy(c => c.Artist).ToList();
                var nomesConcatenados = string.Join(", ", cantoresOrdenados.Select(c => c.Artist));

                return Ok(nomesConcatenados);
            }
            catch (JsonException)
            { return StatusCode(500, "Erro ao  ordenar."); }
        }


        //Newtonsoft.Json == C#
        //IActionResult == resposta http



        //##################################
        //cantor
        [HttpGet("Getverificarartista")]
        public async Task<IActionResult> GetFiltroPorNome(string artist)
        {
            try
            {
                var cantores = await _cantor_Server.GetCantoresAsync();
                var cantoresFiltrados = cantores.Where(c => c.Artist.Contains(artist, StringComparison.OrdinalIgnoreCase));
                var nomesConcatenados = string.Join(",", cantoresFiltrados.Select(c => c.Artist));

                //aqui vai mostrar as musicas do cantor
                var cantoresFil = cantores.Where(c => (c.Song == null || c.Song?.ToLower().Contains(c.Song) == true));
                var resultado = cantoresFiltrados.Select(m => $"Música: {m.Song}").ToList();

                return Ok(resultado);
            }
            catch (JsonException)
            { return StatusCode(500, "Artista não localizado no sistema."); }

        }
    }
}
