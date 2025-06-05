namespace Model
{
    public class User
    {
        public int Id { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
