using VHBurguer.Exceptions;

namespace VHBurguer.Applications.Regras
{
    public class HorarioAlteracaoProduto
    {
        public static void ValidarHorario()
        {
            var agora = DateTime.Now.TimeOfDay;
            var abertura = new TimeSpan(10, 0, 0);// 16:00 AM
            var fechamento = new TimeSpan(23, 0, 0);// 11:00 PM

            // retorna um true ou false
            var estaAberto = agora >= abertura && agora <= fechamento;

            // se retornar true
            if (estaAberto)
            {
                throw new DomainException("Não é permitido alterar o produto fora do horário de funcionamento. O horário de funcionamento é das 16:00 às 23:00.");
            }
        }
    }
}
