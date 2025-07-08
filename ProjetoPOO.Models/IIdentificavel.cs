namespace ProjetoPOO.Models
{
    public interface IIdentificavel
    {
        int Id { get; set; }    // antes era só get :contentReference[oaicite:0]{index=0}
        string? Nome { get; set; }
    }
}
