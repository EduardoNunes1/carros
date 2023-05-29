using System.Text.Json;
using Carros.Models;
namespace Carros.Services;
public class CarService : ICarService
{
    private readonly IHttpContextAccessor _session;
    private readonly string carroFile = @"Data\carrinhos.json";
    private readonly string tiposFile = @"Data\tipos.json";
    public CarService(IHttpContextAccessor session)
    {
        _session = session;
        PopularSessao();
    }
    public List<Carro> GetCarrinhos()
    {
        PopularSessao();
        var carrinhos = JsonSerializer.Deserialize<List<Carro>>
            (_session.HttpContext.Session.GetString("Carrinhos"));
        return carrinhos;
    }
    public List<Tipo> GetTipos()
    {
        PopularSessao();
        var tipos = JsonSerializer.Deserialize<List<Tipo>>
            (_session.HttpContext.Session.GetString("Tipos"));
        return tipos;
    }
    public Carro GetCarro(int Numero)
    {
        var carrinhos = GetCarrinhos();
        return carrinhos.Where(p => p.Numero == Numero).FirstOrDefault();
    }
    public CarrosDto GetCarrosDto()
    {
        var cars = new CarrosDto()
        {
            Carros = GetCarrinhos(),
            Tipos = GetTipos()
        };
        return cars;
    }
    public DetailsDto GetDetailedCarro(int Numero)
    {
        var carrinhos = GetCarrinhos();
        var car = new DetailsDto()
        {
            Current = carrinhos.Where(p => p.Numero == Numero)
            .FirstOrDefault(),
            Prior = carrinhos.OrderByDescending(p => p.Numero)
            .FirstOrDefault(p => p.Numero < Numero),
            Next = carrinhos.OrderBy(p => p.Numero)
            .FirstOrDefault(p => p.Numero > Numero),
        };
        car.Tipos = GetTipos();
        return car;
    }
    public Tipo GetTipo(string Nome)
    {
        var tipos = GetTipos();
        return tipos.Where(t => t.Nome == Nome).FirstOrDefault();
    }
    private void PopularSessao()
    {
        if (string.IsNullOrEmpty(_session.HttpContext.Session.GetString("Tipos")))
        {
            _session.HttpContext.Session
            .SetString("Carrinhos", LerArquivo(carroFile));
            _session.HttpContext.Session
            .SetString("Tipos", LerArquivo(tiposFile));
        }
    }
    private string LerArquivo(string fileName)
    {
        using (StreamReader leitor = new StreamReader(fileName))
        {
            string dados = leitor.ReadToEnd();
            return dados;
        }
    }
}
