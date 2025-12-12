
using ItemsManager;
using Models;


//EntityManager<Person> entityManager = new PeopleManager();
EntityManager<Product> entityManager = new ProductsManager("C:\\Users\\Student\\Desktop\\manager");
//EntityManager<ToDo> entityManager = new ToDoManager();

entityManager.Run();