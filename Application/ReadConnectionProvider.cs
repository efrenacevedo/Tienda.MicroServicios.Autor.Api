namespace Tienda.MicroServicios.Autor.Api.Application
{
    public class ReadConnectionProvider : IReadConnectionProvider
    {
        private readonly string[] _conns;
        private readonly Random _rnd = new();

        public ReadConnectionProvider(string[] conns)
        {
            if (conns == null || conns.Length == 0) throw new ArgumentException("Se requieren connection strings de lectura.");
            _conns = conns;
        }

        public string GetRandom() => _conns[_rnd.Next(_conns.Length)];
    }
}
