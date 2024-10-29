using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIstaDeTarefas.Services
{
    internal class ListaTarefas : ITask
    {
        private readonly ITask _tasks;

        public ListaTarefas(ITask tasks)
        {
            _tasks = tasks;
        }

        public List<Task> GetTasks()
        {
           return _tasks.GetTasks();
        }
    }
}
