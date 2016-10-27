using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using Models;
using System.Collections.Generic;

namespace DrugiZadUnit
{
    [TestClass]
    public class TodoRepositoryTests

    {
        //add
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Add(null);

        }

        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }

        //update
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateNull()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Update(null);
        }
        //get
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetGuid()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("Apple");
            repository.Add(item);
            item.Id = Guid.Empty;
            // repository.Get(g);
            repository.Get(item.Id);

        }
        //remove
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveGuid()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("Apple");
            repository.Add(item);
            item.Id = Guid.Empty;
            repository.Remove(item.Id);

        }

        //MarkAsCompleted
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MarkGuid()
        {
            ITodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("Apple");
            repository.Add(item);
            item.Id = Guid.Empty;
            repository.MarkAsCompleted(item.Id);
        }
        //GetAll
        [TestMethod]
        public void GetAllFromEmptyRepository()
        {
            try
            {
                ITodoRepository repository = new TodoRepository();
                List<TodoItem> lista = new List<TodoItem>();
                lista = repository.GetAll();
            }
            catch (InvalidOperationException)
            {               
                return;
            }
            Assert.Fail("database is empty!");
        }

        //GetActive
        [TestMethod]
        public void GetActiveFromEmptyList()
        {
            try
            {
                ITodoRepository repository = new TodoRepository();
                Func<TodoItem, bool> filter = Completed;
                repository.GetActive(filter);
            }
            catch (InvalidOperationException)
            {
                return;
            }
            Assert.Fail();
        }

        public static bool Completed(TodoItem item)
        {
            return item.IsCompleted;
        }

        [TestMethod]
        public void GetActiveFromList()
        {
            try
            {
                ITodoRepository repository = new TodoRepository();
                TodoItem item = new TodoItem("Apple");
                repository.Add(item);
                Func<TodoItem, bool> filter = Completed;
                repository.GetActive(filter);
            }
            catch(InvalidOperationException)
            {
                return;
            }
        }
        //GetComplited
        [TestMethod]
        public void GetComplitedFromEmpty()
        {
            try
            {
                ITodoRepository repository = new TodoRepository();
                Func<TodoItem, bool> filter = Completed;
                repository.GetCompleted(filter);
            }
            catch (InvalidOperationException)
            {
                return;
            }
            Assert.Fail();           
        }

        [TestMethod]
        public void GetComplitedFromDatabase()
        {
            try
            {
                ITodoRepository repository = new TodoRepository();
                Func<TodoItem, bool> filter = Completed;
                TodoItem item = new TodoItem("Book");
                item.IsCompleted = true;
                repository.Add(item);
                repository.GetCompleted(filter);
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }

        //GetFiltered
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetFilteredFromEmpty()
        {
            ITodoRepository repository = new TodoRepository();
            Func<TodoItem, bool> filterFunction = FilterByText;
            repository.GetFiltered(filterFunction);               
        }

        public static bool FilterByText(TodoItem item)
        {
            return item.Text == "Apple";
        }

        [TestMethod]
        public void GetFilteredFromDatabase()
        {
            try
            {
                ITodoRepository repository = new TodoRepository();
                Func<TodoItem, bool> filterFunction = FilterByText;
                TodoItem item = new TodoItem("Apple");
                repository.Add(item);
                repository.GetFiltered(filterFunction);
            }
            catch (InvalidOperationException)
            {
                return;
            }
            
        }
    }
}
