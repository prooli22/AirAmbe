using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AirAmbe.ViewModel
{
    public class Commande : ICommand
    {
        readonly Action<object> actionAExecute;
        public Commande(Action<object> execute):this(execute, null) { }
        public Commande(Action<object> execute, Predicate<object> CanExecute)
        {
            actionAExecute = execute;
        }
        public bool CanExecute(object param)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object param)
        {
            actionAExecute(param);
        }
    }
}
