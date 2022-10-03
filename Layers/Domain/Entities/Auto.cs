using AVIS.CoreBase.Clases;
using AVIS.CoreBase.Interfaces;

namespace Avis.Catalogo.Domain;

public class Auto
{
    [IdentityField] 
    public virtual int AutoId { get; set; }
    public virtual string AutoPkey { get; set; }
    public virtual string Marca { get; set; }
    public virtual string Modelo { get; set; }
    public virtual string Color { get; set; }
    public virtual string Tipo { get; set; }

    public void UpdateInfo(Auto info)
    {
        Marca = info.Marca;
        Modelo = info.Modelo;
        Color = info.Color;
        Tipo = info.Tipo;
    }
}