using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIstaDeTarefas.Services
{
    internal interface ITask
    {
        public List<Task> GetTasks();
    }
}
