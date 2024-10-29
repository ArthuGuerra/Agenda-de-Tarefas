using LIstaDeTarefas.Entities;
using LIstaDeTarefas.Entities.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.SqlTypes;
using System.Runtime.Intrinsics.X86;
using System;
using System.IO;
using System.Globalization;
using System.Threading.Tasks;

namespace LIstaDeTarefas.Services
{
    internal class TaskService 
    {
        private List<Tasks> _tasks = new List<Tasks>();
      
        public TaskService() { }

        public void NewTask(Tasks task)
        {
            if (_tasks.Contains(task))
            {
                Console.WriteLine("Tarefa já Existente");
                Console.WriteLine($"Aperte qualquer botao");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                _tasks.Add(task);
                Console.WriteLine("Tarefa Adicionada com Sucesso");
            }
        }


        public void PrintTasks()
        {
            if(_tasks.Count == 0)
            {
                Console.WriteLine("Nenhuma tarefa " +
                    "adicionada");
                Console.WriteLine($"Aperte qualquer botao");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                foreach (Tasks task in _tasks)
                {
                    Console.WriteLine(task.ToString());
                    Console.WriteLine("aperte qualquer tecla");
                    Console.ReadLine();
                    Console.Clear();
                }
                Console.WriteLine("aperte qualquer tecla");
                Console.ReadLine();
                Console.Clear();
            }
           

        }


        //O usuário pode alterar qualquer informação de uma tarefa(descrição, data de vencimento, prioridade, categoria).
        //Permite marcar uma tarefa como concluída. Tarefas concluídas podem ser arquivadas.
        // Marcar como concluída:


        public void UpdateTask(string tarefa)
        {
            var tarefaExistente = _tasks.First(p => p.NomeTarefa.Equals(tarefa));
            if (tarefaExistente != null)
            {
                Console.WriteLine("Oque deseja atualizar ?? ");            
                Console.WriteLine("1 - Descrição");
                Console.WriteLine("2 - Prioridade");
                Console.WriteLine("3 - Status");
                Console.WriteLine("4 - Categoria");
                Console.WriteLine("5 - Vencimento");

                int update = int.Parse(Console.ReadLine());


                switch (update)
                {
                    case 1:
                        Console.WriteLine("Edite a Descrição: ");
                        tarefaExistente.Descricao = Console.ReadLine();
                        Console.WriteLine("Descrição atualizada! ");
                    break;

                    case 2:
                        Console.WriteLine("Edite a Prioridade: ");                  
                        Prioridade priority = Enum.Parse<Prioridade>(Console.ReadLine());
                        tarefaExistente.Priority = priority;
                        Console.WriteLine("Prioridade atualizada! ");
                    break;

                    case 3:
                        Console.WriteLine("Edite o Status: ");
                        Status status = Enum.Parse<Status>(Console.ReadLine());
                        tarefaExistente.Status = status;
                        Console.WriteLine("Status atualizado! ");
                    break;

                    case 4:
                        Console.WriteLine("Edite a Categoria: ");
                        tarefaExistente.Categoria = Console.ReadLine();
                        Console.WriteLine("Categoria atualizada! ");
                    break;

                    case 5:
                        Console.WriteLine("Edite a Data de Vencimento: ");
                        DateTime newDate = DateTime.ParseExact("dd/MM/yyyy", Console.ReadLine(),CultureInfo.InvariantCulture);
                        tarefaExistente.Vencimento = newDate;
                        Console.WriteLine("Data de Vencimento atualizada! ");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Tarefa não registrada!");
            }
        }


        // Filtrar por status: Mostrar tarefas concluídas, pendentes e iniciadas separadamente.

        public void PrintTaskStatus(string status)  
        {
            Status stt = Enum.Parse<Status>(status);
          
            List<Tasks> lista = _tasks.FindAll(x => x.Status == stt);

            if (lista.Count == 0)
            {
                Console.WriteLine($"Nenhuma tarefa com Status {stt} Adicionada!");
                Console.WriteLine($"Aperte qualquer botao");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                foreach (Tasks task in lista)
                {
                    Console.WriteLine(task.ToString());
                }
            }
        }

        public void PrintTaskPriority(string prioridade)     
        {
            Prioridade priority = Enum.Parse<Prioridade>(prioridade);
            List<Tasks> list = _tasks.FindAll(x => x.Priority.Equals(priority));

            if (list.Count == 0)
            {
                Console.WriteLine($"Não existe nenhuma tarefa com Prioridade {priority} Adicionada!");
            }
            else
            {           
                foreach(Tasks tt in list)
                {
                    Console.WriteLine(tt.ToString());
                }
                
            }
        }


        public void PrintPerCategories(string categoria)
        {
            List<Tasks> lista = _tasks.FindAll(x => x.Categoria == categoria);

            if(lista.Count == 0)
            {
                Console.WriteLine($"Não existe nenhuma tarefa com Categoria {categoria} adicionada");
            }
            else
            {
                foreach (Tasks tt in lista)
                {
                    Console.WriteLine(tt.ToString());
                }
            }         
        }



        // O sistema pode exibir um relatório com estatísticas, como o percentual de tarefas concluídas

        // o tempo médio para completar uma tarefa.
        public void StatisticsStatus(string status, IEstatisticas estatisticas)
        {
            Status stt = Enum.Parse<Status>(status);

            List<Tasks> statusList = _tasks.FindAll(s => s.Status == stt);

            if (statusList.Count == 0)
            {
                Console.WriteLine($"Nenhuma tarefa com status: {status} foi adicionada");
            }
            else
            {
                int quantidadeDeStatus = statusList.Count;
                int total = _tasks.Count;
                double result = estatisticas.Calcular(quantidadeDeStatus, total);

                Console.WriteLine($"O Percentual de status: {stt}, é de {result.ToString("F2", CultureInfo.InvariantCulture)}%");
            }
        }


        public void StatisticsPrioritys(string priority,IEstatisticas estatisticas)
        {
            Prioridade priorityEnum = Enum.Parse<Prioridade>(priority);

            List<Tasks> priorityList = _tasks.FindAll(p => p.Priority == priorityEnum);

            if(priorityList.Count == 0)
            {
                Console.WriteLine($"Nenhuma tarefa com Prioridade: {priorityEnum} foi adicionada");
            }
            else
            {
                double quantidadeDePriority = priorityList.Count;
                double total = _tasks.Count;

                double result = estatisticas.Calcular(quantidadeDePriority,total);
                Console.WriteLine($"O percentual de Prioridade: {priorityEnum}, é de {result.ToString("F2",CultureInfo.InvariantCulture)}%");
            }
        }

        public void StatisticsCategory(string category,IEstatisticas estatistica)
        {
            List<Tasks> listCategory = _tasks.FindAll(c => c.Categoria == category);

            if (listCategory.Count == 0)
            {
                Console.WriteLine($"Nenhuma Tarefa com categoria: {category} foi adicionada");
            }
            else
            {
                int quantidadeDeCategorys = listCategory.Count;
                int total = _tasks.Count;
                double result = estatistica.Calcular(quantidadeDeCategorys, total);
                Console.WriteLine($"O percentual de categoria: {category} é de {result.ToString("F2", CultureInfo.InvariantCulture)}% ");
            }
        }


        /// <summary>
        ///  verificando os concluidos e retirando 
        /// </summary>


        public void StatusConcluidos()
        {
            Status stt = Enum.Parse<Status>("Concluida");

            List<Tasks> statusList = _tasks.FindAll(s => s.Status == stt);

            if (statusList.Count == 0)
            {
                Console.WriteLine($"Nenhuma tarefa com Status Concluida ");
            }
            else
            {
                int quantidadeDeConcluida = statusList.Count;
                Console.WriteLine($"Quandidade de tarefas Concluídas: {quantidadeDeConcluida}");

                Console.WriteLine($"Deseja remover Tarefas Concluídas ? ");
                Console.WriteLine($"1 - SIM ");
                Console.WriteLine($"2 - NÃO ");
                int resp = int.Parse(Console.ReadLine());
                if (resp == 1)
                {

                }
                {

                }
            }
        }



        // NOTIFICAR ?

        //Notificação de prazo: Exibir uma notificação de alerta quando o prazo de uma tarefa estiver próximo ou for ultrapassado (pode ser via console, ou email, se for web).

        public void Notification()
        {

        }



        // O usuário pode remover tarefas concluídas ou não necessárias, ou até mesmo limpá-las automaticamente após certo período.

        public void ArchiveTask()
        {

        }




        // As tarefas são salvas em um banco de dados local (SQLite, por exemplo) ou arquivo XML/JSON.Isso permite que as tarefas sejam mantidas entre sessões de uso.
        // API ? 














    }
}
