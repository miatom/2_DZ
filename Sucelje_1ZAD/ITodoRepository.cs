using IGenericList;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ITodoRepository 
    {
        TodoItem Get(Guid todoId);
        void Add(TodoItem todoItem);
        bool Remove(Guid todoId);
        void Update(TodoItem todoItem);
        bool MarkAsCompleted(Guid todoId);
        List<TodoItem> GetAll();
        List<TodoItem> GetActive(Func<TodoItem, bool> filter);
        List<TodoItem> GetCompleted(Func<TodoItem, bool> filter);
        List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction);

    }

    public class TodoRepository : ITodoRepository
    {
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;
        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _inMemoryTodoDatabase = initialDbState;
            }
            else
            {
                _inMemoryTodoDatabase = new GenericList<TodoItem>();
            }
        }
        /*
        public static bool Completed(TodoItem item)
        {
            return item.IsCompleted;
        }

        Func<TodoItem, bool> filter = Completed;
        */
        //implementacija sučelja
        public TodoItem Get(Guid todoId)
        {
            if (todoId==Guid.Empty || todoId==null)
            {
                throw new ArgumentException();
            }
            IEnumerable<TodoItem> rezultat =
                from item in _inMemoryTodoDatabase
                where item.Id == todoId
                select item;

            return rezultat.SingleOrDefault();

        }

        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException();
            }

            IEnumerable<TodoItem> pretrazi_id =
                 from item in _inMemoryTodoDatabase
                 where item.Id == todoItem.Id
                 select item;
            if (pretrazi_id.Count() == 0)
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }
           
            else
            {
                throw new DuplicateTodoItemException("Item with that ID already exists!");
            }
        }

        public bool Remove(Guid todoId)
        {
            if (todoId==Guid.Empty || todoId==null)
            {
                throw new ArgumentException();
            }
            IEnumerable<TodoItem> pronadi_element =
                from item in _inMemoryTodoDatabase
                where item.Id == todoId
                select item;
            if (pronadi_element.Count() > 0)
            {
                return _inMemoryTodoDatabase.Remove(pronadi_element.First());
            }
            return false;
        }

        public void Update(TodoItem todoItem)
        {
            if (todoItem==null)
            {
                throw new ArgumentNullException();
            }
            var pronadi = _inMemoryTodoDatabase.Select(w => todoItem).FirstOrDefault();
            if (pronadi==null)
            {
                _inMemoryTodoDatabase.Add(new TodoItem("novi"));
            }
            else
            {
                todoItem.DateCreated=DateTime.Now;
                todoItem.DateCompleted = DateTime.Now;
                todoItem.Id = Guid.NewGuid();
                todoItem.IsCompleted = true;
                todoItem.Text = "update complete";
            }
           
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            if (todoId==Guid.Empty || todoId==null)
            {
                throw new ArgumentException();
            }
            IEnumerable<TodoItem> pronadi =
                from item in _inMemoryTodoDatabase
                where item.Id == todoId
                select item;
            if (pronadi.Count()>0)
            {
                pronadi.First().MarkAsCompleted();
                return pronadi.First().IsCompleted;
            }
            return false;
        }

        public List<TodoItem> GetAll()
        {
            if (_inMemoryTodoDatabase.Count == 0)
            {
                throw new InvalidOperationException();
            }
            IEnumerable<TodoItem> lista =
                from item in _inMemoryTodoDatabase
                orderby item.DateCreated
                select item;
            

            return lista.ToList();
        }

        public List<TodoItem> GetActive(Func<TodoItem, bool> filter)
        {
            if (_inMemoryTodoDatabase.Count==0)
            {
                throw new InvalidOperationException();
            }
            IEnumerable<TodoItem> lista =
                from item in _inMemoryTodoDatabase
                where filter(item) == false
                select item;

            return lista.ToList();
               

        }

        public List<TodoItem> GetCompleted(Func<TodoItem, bool> filter)
        {
            if (_inMemoryTodoDatabase.Count==0)
            {
                throw new InvalidOperationException();
            }
            IEnumerable<TodoItem> lista =
                from item in _inMemoryTodoDatabase
                where filter(item) == true
                select item;

            return lista.ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            if (_inMemoryTodoDatabase.Count==0)
            {
                throw new InvalidOperationException();
            }
            IEnumerable<TodoItem> lista =
                from item in _inMemoryTodoDatabase
                where filterFunction(item) == true
                select item;

            return lista.ToList();
            
        }
    }

    public class DuplicateTodoItemException: Exception
    {
        public DuplicateTodoItemException(string message):base(message)
        {
        }
    }
}
