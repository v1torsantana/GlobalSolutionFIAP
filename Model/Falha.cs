namespace Model
{
    public class Falha
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string TipoFalha { get; set; }
        public string Descricao { get; set; }
        public string UsuarioRegistro { get; set; }
    }
}
