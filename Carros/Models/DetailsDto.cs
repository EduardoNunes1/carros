namespace Carros.Models;
public class DetailsDto
{
    public Carro Prior { get; set; }
    public Carro Current { get; set; }
    public Carro Next { get; set; }
    public List<Tipo> Tipos { get; set; }
}
