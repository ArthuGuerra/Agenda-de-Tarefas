using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LIstaDeTarefas.Entities.Enums;
using LIstaDeTarefas.Services;

namespace LIstaDeTarefas.Entities
{
    internal class Tasks //: ITask //: IComparable
    {
        public string NomeTarefa { get; set; }
        public string Descricao { get; set; } = default!;
        public DateTime Vencimento { get; set; }
        public Prioridade Priority { get; set; }
        public Status Status { get; set; }
        public string Categoria { get; set; }
        



        public Tasks(string nome, string descricao, DateTime vencimento, Prioridade priority, Status status, string categoria )
        {
            NomeTarefa = nome;
            Descricao = descricao;
            Vencimento = vencimento;
            Priority = priority;
            Status = status;
            Categoria = categoria;
        }
        public Tasks(){}

        public List<Task> GetTasks(Task tarefa)
        {
            var tasks = new List<Task>();
            tasks.Add(tarefa);
            return tasks;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Lista de Tarefas\n");
            sb.AppendLine("Tarefa: " + NomeTarefa + "\n");
            sb.AppendLine("Descrição: " + Descricao + "\n");
            sb.AppendLine("Prioridade: " + Priority + "\n");
            sb.AppendLine("Status: " + Status + "\n");
            sb.AppendLine("Categoria: " + Categoria + "\n");
            sb.AppendLine("Tarefa Criada Em: " + DateTime.UtcNow.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss") + "\n");
            sb.AppendLine("Vencimento: " + Vencimento.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss") + "\n");
            
            return sb.ToString();
               
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is Tasks))
            {
                throw new ArgumentException("Obj nao é do tipo Tasks");
            }
            else
            {
                Tasks other = obj as Tasks;
                return NomeTarefa.Equals(other.NomeTarefa);
            }
        }

        public override int GetHashCode()
        {
            return NomeTarefa.GetHashCode();
        }

        public int CompareTo(object? obj)
        {
            if (!(obj is Tasks))
            {
                throw new Exception("Objeto inválido");
            }

            Tasks other = obj as Tasks;
            return this.NomeTarefa.CompareTo(other.NomeTarefa);

        }
    }
}
