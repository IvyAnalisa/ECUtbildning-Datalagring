using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using static System.Console;
using ProducgADONet.Models;
using System.Threading;
using System.Data;

namespace ProducgADONet
{

    class Program
    {
        static string cs = @"Data Source=DESKTOP-U3ME4QU;Initial Catalog=ProductManager;
                            Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                ApplicationIntent=ReadWrite;MultiSubnetFailover=False";



        static void Main(string[] args)
        {
            string username, password;
            do
            {
                WriteLine("  Enter username:");
                username = Convert.ToString(ReadLine());
                WriteLine("  Enter password:");
                password = Convert.ToString(ReadLine());

                if ((username != "Admin") || (password != "13579"))
                {
                    WriteLine("Login failed.Try again...");
                }
            } while ((username != "Admin") || (password != "13579"));
            {
                WriteLine("Login succesfully");
                bool appicationRunning = false;
                WriteLine("*******************************************************");
                WriteLine("*********    Project Arbete -Datalagring      *********");
                WriteLine("***** Namn: IVY ANALISA LA - Webutvecklare.Net 2021****");
                WriteLine("*******************************************************");

                do
                {
                    WriteLine("********************************");
                    WriteLine("***  1. Add product          ***");
                    WriteLine("***  2. Search product       ***");
                    WriteLine("***  3. Display Product List ***");
                    WriteLine("***  4. Add Category           ***");
                    WriteLine("***  5. Add Product to Category***");
                    WriteLine("***  6. Display List Category  ***");
                    WriteLine("***  7. Delete product ***");
                    WriteLine("***  8. Display Category ***");
                    WriteLine("***  9. Exit                   ***");
                    WriteLine("********************************");
                    ConsoleKeyInfo input = ReadKey(true);
                    switch (input.Key)
                    {
                        case ConsoleKey.D1:
                            AddProduct();
                            break;

                        case ConsoleKey.D2:
                            SearchProduct();
                            break;

                        case ConsoleKey.D3:
                            DisplayProduct();
                            break;

                        case ConsoleKey.D4:
                            AddCategory();
                            break;



                        case ConsoleKey.D5:
                            AddProductToCategory();
                            break;

                        case ConsoleKey.D6:
                            DisplayListCategory();
                            break;

                        case ConsoleKey.D7:
                            DeleteProduct();
                            break;

                        case ConsoleKey.D8:
                            DisplayCategory();
                            break;

                        case ConsoleKey.D9:
                            {
                                appicationRunning = true;
                            }
                            return;
                        default:
                            WriteLine("Wrong input");
                            break;
                    }
                } while (!appicationRunning);
            }

        }
        private static void AddProduct()
        {
            try
            {

                SqlConnection conn = new SqlConnection(cs);
                conn.Open();
                Product products = new Product();


                do
                {

                    //WriteLine("Article number:");
                    // string articleNumber = ReadLine();

                    WriteLine("Product Name:");

                    products.Name = ReadLine();


                    WriteLine("Description");
                    products.Description = ReadLine();

                    WriteLine("Price");
                    products.Price = decimal.Parse(ReadLine());

                    WriteLine("Image URl");
                    products.ImageUrl = ReadLine();

                    WriteLine("  Is this correct Y(es) N(o)" + "\n");


                } while (ReadKey().Key == ConsoleKey.N);

                if (products.Equals(products.Id) && ReadKey().Key == ConsoleKey.Y)
                {

                    throw new ArgumentException(String.Format("Product exist"));
                }
                else
                {

                    string sql = @"INSERT INTO Product(
                                Name,
                                Description,
                                Price,
                                ImageUrl)
                                VALUES(
                                @Name,@Description,@Price,@ImageUrl)";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@Name", products.Name);
                    cmd.Parameters.AddWithValue("@Description", products.Description);
                    cmd.Parameters.AddWithValue("@Price", products.Price);
                    cmd.Parameters.AddWithValue("@ImageUrl", products.ImageUrl);
                    Console.WriteLine("A new product has been added." + Environment.NewLine);
                    cmd.ExecuteNonQuery();

                }
                Thread.Sleep(2000);


                conn.Close();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
        }
        private static void SearchProduct()
        {
            try
            {
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();
                Product products = new Product();
                WriteLine("*** 2.Search Product :***");
                WriteLine("*************************");

                WriteLine(" Write product Name you want to search:");
                products.Name = ReadLine();

                string sql = @"SELECT * FROM Product WHERE Name =@Name ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", products.Name);
                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    WriteLine("result");


                    WriteLine(r[0].ToString() + "             " + r[1].ToString() + "             " + r[2].ToString() + "             " + r[3] + "             " + r[4]);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
        }
        private static void DisplayProduct()
        {

            try
            {
                Product products = new Product();
                List<Product> productsList = new List<Product>();
                WriteLine("***Product List***");
                WriteLine("{0,-20}{1,-20}{2,-20}{3,-20}{4,-10} {5,-10}", "Product Id", "Name", "Description", "Price", "Image URL", "Category ID");
                WriteLine("----------------------------------------------------------------------------------------------");
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();
                string sql = @"SELECT *
                            FROM Product";
                SqlCommand command = new SqlCommand(sql, conn);

                SqlDataReader r = command.ExecuteReader();

                while (r.Read())
                {
                    WriteLine(r[0].ToString() + "              " + r[1] + "               " + r[2] + "              " + r[3] + "             " + r[4] + "          " + r[5]);
                }

                /* while (r.Read() == true)
                 {


                     var product = new Product(Id: (int)r["Id"],
                                                  Name: (string)r["Name"],
                                                  Description: (string)r["Description"],
                                                  Price: (decimal)r["Price"],
                                                  ImageUrl: (string)r["ImageUrl"]);
                     productsList.Add(product);
                     WriteLine($"{product.Id}           {product.Name}               {product.Description}            {product.Price}             {product.ImageUrl}");

                 }*/

                conn.Close();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }


        }
        private static void AddCategory()
        {
            try
            {

                SqlConnection conn = new SqlConnection(cs);
                conn.Open();
                Category category = new Category();


                do
                {

                    //WriteLine("Article number:");
                    // string articleNumber = ReadLine();

                    WriteLine("Category Name:");

                    category.CategoryName = ReadLine();




                    WriteLine("  Is this correct Y(es) N(o)" + "\n");


                } while (ReadKey().Key == ConsoleKey.N);

                if (category.Equals(category.CategoryId) && ReadKey().Key == ConsoleKey.Y)
                {

                    throw new ArgumentException(String.Format("Category exist"));
                }
                else
                {

                    string sql = @"INSERT INTO Category(
                                CategoryName
                                )
                                VALUES(
                                @CategoryName)";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                    Console.WriteLine("A new category has been added." + Environment.NewLine);
                    cmd.ExecuteNonQuery();

                }
                Thread.Sleep(2000);


                conn.Close();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
        }

        private static void DisplayCategory()
        {

            try
            {
                Product products = new Product();
                List<Product> productsList = new List<Product>();
                WriteLine("***Category***");
                WriteLine("{0,-20}{1,-20}", "Category Id", " Category Name");
                WriteLine("---------------------------------------------------");
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();
                string sql = @"SELECT *
                            FROM Category";
                SqlCommand command = new SqlCommand(sql, conn);

                SqlDataReader r = command.ExecuteReader();

                while (r.Read())
                {
                    WriteLine(r[0].ToString() + "             " + r[1] + "             ");
                }

                /* while (r.Read() == true)
                 {


                     var product = new Product(Id: (int)r["Id"],
                                                  Name: (string)r["Name"],
                                                  Description: (string)r["Description"],
                                                  Price: (decimal)r["Price"],
                                                  ImageUrl: (string)r["ImageUrl"]);
                     productsList.Add(product);
                     WriteLine($"{product.Id}           {product.Name}               {product.Description}            {product.Price}             {product.ImageUrl}");

                 }*/

                conn.Close();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }


        }

        private static void AddProductToCategory()

        {
            try
            {
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();

                Product products = new Product();
                Category category = new Category();
                /*
              WriteLine("Write product name");
              products.Name = ReadLine();
              WriteLine("write category name");
              category.CategoryName = ReadLine();

              string sql = @"SELECT * FROM Category
                          INNER JOIN Product
                          ON Category.CategoryId =Product.CategoryId ";

                SqlCommand cmd = new SqlCommand(sql, conn);

                // cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int col = 0; col < reader.FieldCount; col++)
                    {
                        // WriteLine(reader.GetName(col).ToString());
                        WriteLine(reader[col].ToString());
                    }



                }*/

                WriteLine("write product ");
                string selectProductByName = ReadLine();
                FindProductByName(selectProductByName);




                //Find product to add to Category

                WriteLine("Write Category Id:");
                int selectCategoryById = Convert.ToInt32(ReadLine());
                FindCategoryById(selectCategoryById);
                if (selectCategoryById == null)
                {
                    Notify("Category is not found");
                    return;
                }

                // Product p = GetProduct(selectProductByName);

                SaveProduct(selectCategoryById, selectProductByName);
                WriteLine("Product has been added");

                conn.Close();


            }

            catch (Exception ex)
            { Console.WriteLine(ex.Message); }

        }



        private static void Notify(string message)
        {
            Clear();
            WriteLine(message);
            Thread.Sleep(2000);
        }

        private static void FindCategoryById(int selectCategoryById)
        {
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            Category category = new Category();

            //  WriteLine("Write Category Id:");
            // category.CategoryId = Convert.ToInt32(ReadLine());
            string sql = @"SELECT * FROM Category WHERE CategoryId =@CategoryId ";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@CategoryId", selectCategoryById);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {

                WriteLine("result");


                WriteLine(r[0].ToString() + "             " + r[1].ToString());
            }


        }

        static void FindProductByName(string selectProductByName)
        {
            Product products = new Product();
            SqlConnection conn = new SqlConnection(cs);

            conn.Open();
            string sql = @"SELECT Id,                      
                       Name,
                       Description,
                       ImageUrl,
                       Price
                        FROM Product WHERE Name =@Name ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Name", selectProductByName);
            SqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                WriteLine("Result");


                WriteLine(r[0].ToString() + "             " + r[1].ToString() + "             " + r[2].ToString() + "             " + r[3] + "             " + r[4]);
            }


        }
        static void SaveProduct(int selectCategoryById, string selectProductByName)
        {
            Product products = new Product();
            Category category = new Category();
            SqlConnection conn = new SqlConnection(cs);

            conn.Open();
            /* string sql = @" SELECT * FROM Category
                             INNER JOIN Product
                             ON Product.CategoryId= Category.CategoryId
                             ";*/
            string sql = @"
                    UPDATE Product SET CategoryId = 
                   (SELECT CategoryId FROM Category
                    WHERE CategoryId = @CategoryId)
                     WHERE       Name= @Name
                   ";

            //problem :cast all product to same category
            SqlCommand command = new SqlCommand(sql, conn);

            command.Parameters.AddWithValue("@CategoryId", selectCategoryById);

            // command.Parameters.AddWithValue("@Id", products.Id);
            command.Parameters.AddWithValue("@Name", selectProductByName);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                WriteLine(reader[0].ToString() + "    " + reader[1] + "    " + reader[2] + "   " + reader[3] + "   " + reader[5]);
                /* for (int col = 0; col < reader.FieldCount; col++)
                 {
                     // WriteLine(reader.GetName(col).ToString());
                     WriteLine(reader[col].ToString());

                 }*/



            }



        }
        static Product GetProduct(string selectProductByName)
        {
            string sql = @"
                SELECT Id,                      
                       Name,
                       Description,
                       ImageUrl,
                       Price
                  FROM Product
                 WHERE Name = @Name";
            SqlConnection conn = new SqlConnection(cs);


            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Name", selectProductByName);

            SqlDataReader reader = cmd.ExecuteReader();


            if (reader.Read() == false)

                return null;

            Product products = new Product();


            return products;
        }
        static void DisplayListCategory()
        {



            Category Category = new Category();
            Product products = new Product();
            List<Product> productsList = new List<Product>();
            List<Category> categoryList = new List<Category>();
            try
            {
                WriteLine("*** List Category***");
                WriteLine("{0,-20}{1,-20}", "Category Id", "Category Name");
                WriteLine("----------------------------------------------");
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();
                string sql = @"SELECT *  FROM Category 
                             JOIN Product
                              ON Category.CategoryId = Product.CategoryId
                              ";
                /*string sql = @" SELECT COUNT (CategoryId),Name
                                FROM Product
                                GROUP BY Name 
                                ORDER BY COUNT (CategoryId) DESC
                                    ";*/
                SqlCommand command = new SqlCommand(sql, conn);

                SqlDataReader reader = command.ExecuteReader();
                command.Parameters.AddWithValue("@CategoryId", Category.CategoryId);
                command.Parameters.AddWithValue("@CategoryName", Category.CategoryName);
                //  command.Parameters.AddWithValue("@Id", products.Name);


                while (reader.Read())
                {
                    // WriteLine($"{Category.CategoryId}    {Category.CategoryName}");
                    WriteLine(reader[0].ToString() + "          " + reader[1]);
                    WriteLine("   " + reader[3] + "    " + reader[5]);
                    WriteLine("----------------------------------------------");

                    /*for (int col = 0; col < reader.FieldCount; col++)
                    {
                        WriteLine(reader.GetName(col).ToString());
                        WriteLine(reader[col].ToString());
                    }*/
                    //  PopulateCategoryProducts(Category);


                    /* foreach (Category category in categoryList)
                    {
                        WriteLine($"   {Category.CategoryName}  ( {categoryList.Count}) ");
                        WriteLine(reader[0].ToString() + reader[1]);
                    }

                    foreach (Category category in GetAllCategories())
                    {
                        PopulateCategoryProducts(category);
                        WriteLine(category.CategoryName + category.products.Count);
                        foreach (Product product in category.products)
                        {
                            WriteLine(" " + product.Name);
                        }

                    }*/

                }




                conn.Close();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }

        }
        static void PopulateCategoryProducts(Category category)
        {
            string sql = @"
                SELECT Id,
                       Name,
                       Description,
                       ImageURL,
                       Price,
                       CategoryId
                  FROM Product
                 WHERE CategoryId = @CategoryId";
            Product products = new Product();
            SqlConnection conn = new SqlConnection(cs);

            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataReader reader = command.ExecuteReader();
            command.Parameters.AddWithValue("@CategoryId", category.CategoryId);

            conn.Open();

            while (reader.Read())
            {
                Product p = new Product();
                p.Id = reader.GetInt32(0);
                p.Name = reader.GetString(2);
                p.Price = reader.GetDecimal(5);
                // products from public List<Product> products { get; set; }
                category.products.Add(p);

            }
            return;
        }
        static List<Category> GetAllCategories()
        {

            string sql = @"SELECT CategoryId,CategoryName  FROM Category";

            SqlConnection conn = new SqlConnection(cs);

            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataReader reader = command.ExecuteReader();

            List<Category> categoryList = new List<Category>();



            while (reader.Read())
            {
                var categoryId = (int)reader["CategoryId"];
                var categoryName = (string)reader["CategoryName"];

                Category category = new Category()
                {
                    CategoryId = categoryId
                };
                categoryList.Add(category);

            }
            return categoryList;
        }

        static void DeleteProduct()
        {

            Product products = new Product();
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();



            try
            {

                WriteLine("Enter a product name u wanna delete: ");
                products.Name = Console.ReadLine();



                string sql = @" DELETE FROM Product WHERE Name = @Name";



                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Name", products.Name);
                Console.WriteLine("The product has been deleted.");
                //categories.Add(Category);

                cmd.ExecuteNonQuery();





                conn.Close();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
        }


    }
}

