using Carros.Models;
namespace Carros.Services;
public interface ICarService
{
 List<Carro> GetCarrinhos();
List<Tipo> GetTipos();
Carro GetCarro(int Numero);
CarrosDto GetCarrosDto();
DetailsDto GetDetailedCarro(int Numero);
 Tipo GetTipo(string Nome);
}
