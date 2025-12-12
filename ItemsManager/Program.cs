
using ItemsManager;
using Models;


//EntityManager<Person> entityManager = new PeopleManager();
EntityManager<Product> entityManager = new ProductsManager();
//EntityManager<ToDo> entityManager = new ToDoManager();

entityManager.Run();