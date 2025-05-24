namespace BuecherVerwaltungEmpty.Data;

public class Buch
{
    public int Id { get; set; }
    public string Titel { get; set; }
    public string Autor { get; set; }
    public string? Verlag { get; set; }
    public int? Jahr { get; set; }
    
    public bool IstGelesen {get;set;} = false;
    public int Bewertung {get;set;}
    
}