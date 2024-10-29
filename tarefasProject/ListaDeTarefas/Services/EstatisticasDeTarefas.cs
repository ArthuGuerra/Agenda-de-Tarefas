
namespace LIstaDeTarefas.Services
{
    internal class EstatisticasDeTarefas : IEstatisticas
    {
        public double Calcular(double tarefasParaEstatistica, double totalTarefas)
        {
            double result = (tarefasParaEstatistica / totalTarefas) * 100;
            return result;
        }
    }
}