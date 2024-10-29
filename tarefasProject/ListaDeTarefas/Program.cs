using LIstaDeTarefas.Entities.Enums;
using LIstaDeTarefas.Services;
using LIstaDeTarefas.Entities;
using System.Globalization;

try
{

    int loop = 20;

    Console.WriteLine("Bem-vindo à sua agenda");
    TaskService service = new TaskService();
    EstatisticasDeTarefas statistica = new EstatisticasDeTarefas();

    while (loop != 0)
    {
        Console.WriteLine($"Escolha uma Ação");
        Console.WriteLine($"1 - Nova Tarefa");
        Console.WriteLine($"2 - Atualizar Tarefa");
        Console.WriteLine($"3 - Filtrar por Status");
        Console.WriteLine($"4 - Filtrar por Prioridade");
        Console.WriteLine($"5 - Filtrar por Categoria");
        Console.WriteLine($"6 - Estatistica dos Status");
        Console.WriteLine($"7 - Estatistica das Prioridades");
        Console.WriteLine($"8 - Estatistica das Categorias");
        Console.WriteLine($"9 - Mostrar Todas as Tarefas");
        Console.WriteLine($"0 - Sair ");

        loop = int.Parse(Console.ReadLine());

        switch (loop)
        {
            case 1:
                Console.WriteLine($"Digite o Nome da Tarefa:\n");
                string nomeTarefa = Console.ReadLine();

                Console.WriteLine($"Descrição da Tarefa:\n");
                string descricaoTarefa = Console.ReadLine();

                Console.WriteLine($"Prioridade: Alta / Media / Baixa \n");
                Prioridade priority = Enum.Parse<Prioridade>(Console.ReadLine());

 
                Console.WriteLine($"Status da Tarefa: Pendente / Iniciada / Concluída\n");
                Status status = Enum.Parse<Status>(Console.ReadLine());

                Console.WriteLine($"Categoria da Tarefa:\n");
                string categoria = Console.ReadLine();

                Console.WriteLine($"Data Vencimento: format(xx/xx/xxxx) ");
                DateTime vencimento = DateTime.ParseExact(Console.ReadLine(),"dd/MM/yyyy", CultureInfo.InvariantCulture);

                Tasks task = new Tasks(nomeTarefa, descricaoTarefa, vencimento, priority, status, categoria);
                service.NewTask(task);

                Console.WriteLine($"\n");
                Console.WriteLine($"Aperte qualquer botao");
                Console.ReadLine();
                Console.Clear();
            break;

            case 2:
                Console.WriteLine($"Digite o Nome da Tarefa que deseja atualizar.");
                string nome = Console.ReadLine();
                service.UpdateTask(nome);
                Console.WriteLine($"\n");
                Console.WriteLine($"Aperte qualquer botao");
                Console.ReadLine();
                Console.Clear();
            break;

            case 3:
                Console.WriteLine("Qual o Status de tarefa quer Monitorar ?");
                string statusTarefa = Console.ReadLine();
                service.PrintTaskStatus(statusTarefa);
                Console.WriteLine($"\n");
                Console.WriteLine($"Aperte qualquer botao");
                Console.ReadLine();
                Console.Clear();
            break;

            case 4:
                Console.WriteLine("Qual a Prioridade de tarefa quer Monitorar ?");
                string prioridadeTarefa = Console.ReadLine();
                service.PrintTaskPriority(prioridadeTarefa);
                Console.WriteLine($"\n");
                Console.WriteLine($"Aperte qualquer botao");
                Console.ReadLine();
                Console.Clear();
            break;

            case 5:
                Console.WriteLine("Qual a Categoria de tarefa quer Monitorar ?");
                string categoriaTarefa = Console.ReadLine();
                service.PrintPerCategories(categoriaTarefa);
                Console.WriteLine($"\n");
                Console.WriteLine($"Aperte qualquer botao");
                Console.ReadLine();
                Console.Clear();
            break;

            case 6:             
                Console.WriteLine($"Qual Status deseja saber o percentual ? ");
                string statusParaPercentual = Console.ReadLine();
                service.StatisticsStatus(statusParaPercentual,statistica);
                Console.WriteLine($"Aperte qualquer botao");
                Console.ReadLine();
                Console.Clear();
            break;

            case 7:
                Console.WriteLine($"Qual Prioridade deseja saber o percentual ?");
                string prioridade = Console.ReadLine();
                service.StatisticsPrioritys(prioridade, statistica);
                Console.WriteLine($"Aperte qualquer botao");
                Console.ReadLine();
                Console.Clear();
            break;

            case 8:
                Console.WriteLine($"Qual Categoria deseja saber o percentual ?");
                string category = Console.ReadLine();
                service.StatisticsCategory(category, statistica);
                Console.WriteLine($"Aperte qualquer botao");
                Console.ReadLine();
                Console.Clear();
            break;

            case 9:
                service.PrintTasks();
                
            break;


        }
    }

} catch(FormatException f)
{
    Console.WriteLine("Erro de Formato " + f.Message);
}
catch (DivideByZeroException f)
{
    Console.WriteLine("Exceção: Divisão por zero é impossivel " + f.Message);
}
catch (IOException f)
{
    Console.WriteLine("Erro de Arquivo inválido ou não encontrado. " + f.Message);
}
catch (ArgumentNullException f)
{
    Console.WriteLine("Variável Nula " + f.Message);
} catch(ArgumentException f)
{
    Console.WriteLine("Erro de Argumento ?? " + f.Message);
}
catch (Exception f)
{
    Console.WriteLine("Erro Genérico " + f.Message);
}






